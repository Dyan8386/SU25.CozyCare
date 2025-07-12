using CozyCare.SharedKernel.Base;
using CozyCare.SharedKernel.Store;
using CozyCare.ViewModels.DTOs;
using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;

namespace CozyCare.PaymentService.Application.Externals
{
    public class BookingApiClient : IBookingApiClient
    {
        private readonly HttpClient _http;
        private readonly ITokenAccessor _tokenAccessor;

        private static readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public BookingApiClient(HttpClient httpClient, ITokenAccessor tokenAccessor)
        {
            _http = httpClient;
            _tokenAccessor = tokenAccessor;
            // BaseAddress đã được set trong AddHttpClient: https://localhost:5000/booking
        }

        public async Task<BaseResponse<BookingDto>> GetBookingByIdAsync(int bookingId, CancellationToken ct = default)
        {
            var token = _tokenAccessor.GetAccessToken();
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var resp = await _http.GetAsync($"bookings/{bookingId}", ct);
            if (resp.StatusCode == HttpStatusCode.TemporaryRedirect ||
                resp.StatusCode == HttpStatusCode.MovedPermanently ||
                resp.StatusCode == HttpStatusCode.PermanentRedirect)
            {
                var redirectUri = resp.Headers.Location!;
                resp = await _http.GetAsync(redirectUri, ct);
            }

            var json = await resp.Content.ReadAsStringAsync(ct);
            if (!resp.IsSuccessStatusCode)
                return BaseResponse<BookingDto>.ErrorResponse($"HTTP {(int)resp.StatusCode} - {resp.ReasonPhrase}");

            if (string.IsNullOrWhiteSpace(json))
                return BaseResponse<BookingDto>.ErrorResponse("Empty response from booking service");

            var wrapper = JsonSerializer.Deserialize<BaseResponse<BookingDto>>(json, _jsonOptions);
            return wrapper ?? BaseResponse<BookingDto>.ErrorResponse("Cannot deserialize BookingDto");
        }
        public async Task<BaseResponse<string>> UpdatePaymentStatusAsync(int bookingId, int newStatusId, CancellationToken ct = default)
        {
            var token = _tokenAccessor.GetAccessToken();
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            // Step 1: Lấy dữ liệu hiện tại của Booking
            var getResp = await _http.GetAsync($"bookings/{bookingId}", ct);
            var getJson = await getResp.Content.ReadAsStringAsync(ct);

            if (!getResp.IsSuccessStatusCode)
                return BaseResponse<string>.ErrorResponse($"HTTP {(int)getResp.StatusCode} - Cannot get booking to update.");

            var bookingResponse = JsonSerializer.Deserialize<BaseResponse<BookingDto>>(getJson, _jsonOptions);
            if (bookingResponse == null || bookingResponse.Data == null)
                return BaseResponse<string>.ErrorResponse("Cannot deserialize booking");

            var bookingDto = bookingResponse.Data;

            // Step 2: Tạo lại BookingRequest với status mới
            var updatedRequest = new BookingRequest
            {
                customerId = bookingDto.CustomerId,
                promotionCode = bookingDto.PromotionCode,
                bookingDate = bookingDto.BookingDate,
                deadline = bookingDto.Deadline,
                totalAmount = bookingDto.TotalAmount,
                notes = bookingDto.Notes,
                bookingStatusId = bookingDto.BookingStatusId,
                paymentStatusId = newStatusId,
                address = bookingDto.Address
            };

            var content = new StringContent(JsonSerializer.Serialize(updatedRequest), System.Text.Encoding.UTF8, "application/json");

            // Step 3: Gửi PUT để cập nhật
            var putResp = await _http.PutAsync($"bookings/{bookingId}", content, ct);
            var putJson = await putResp.Content.ReadAsStringAsync(ct);

            if (!putResp.IsSuccessStatusCode)
                return BaseResponse<string>.ErrorResponse($"HTTP {(int)putResp.StatusCode} - {putResp.ReasonPhrase}");

            var wrapper = JsonSerializer.Deserialize<BaseResponse<string>>(putJson, _jsonOptions);
            return wrapper ?? BaseResponse<string>.ErrorResponse("Cannot deserialize PUT response");
        }
    }
}
