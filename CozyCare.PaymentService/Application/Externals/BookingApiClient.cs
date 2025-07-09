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
    }
}
