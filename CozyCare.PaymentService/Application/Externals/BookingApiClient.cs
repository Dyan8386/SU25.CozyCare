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
            _http.DefaultRequestHeaders.Remove("Authorization");
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            // Gọi qua Gateway: /booking/api/bookings/{id}
            //var resp = await _http.GetAsync($"/booking/api/bookings/{bookingId}", ct);
            //var json = await resp.Content.ReadAsStringAsync(ct);

            var resp = await _http.GetAsync($"bookings/{bookingId}", ct);
            var json = await resp.Content.ReadAsStringAsync(ct);
            if (resp.StatusCode == HttpStatusCode.TemporaryRedirect ||
            resp.StatusCode == HttpStatusCode.MovedPermanently ||
            resp.StatusCode == HttpStatusCode.PermanentRedirect)
            {
                var redirectUri = resp.Headers.Location!;
                // chắc chắn header vẫn còn
                _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                resp = await _http.GetAsync(redirectUri, ct);
            }

            if (!resp.IsSuccessStatusCode)
            {
                return BaseResponse<BookingDto>.ErrorResponse($"HTTP {(int)resp.StatusCode} - {resp.ReasonPhrase}");
            }

            var wrapper = JsonSerializer.Deserialize<BaseResponse<BookingDto>>(json, _jsonOptions);
            return wrapper ?? BaseResponse<BookingDto>.ErrorResponse("Không thể deserialize BookingDto");
        }
    }
}
