using CozyCare.SharedKernel.Base;
using CozyCare.ViewModels.DTOs;
using static System.Net.WebRequestMethods;
using System.Net.Http.Headers;
using System.Net;
using CozyCare.SharedKernel.Store;
using System.Text.Json;

namespace CozyCare.JobService.Application.Externals
{
    public class JobApiClient : IJobApiClient
    {
        private readonly HttpClient _http;
        private readonly ITokenAccessor _tokenAccessor;
        private static readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public JobApiClient(HttpClient httpClient, ITokenAccessor tokenAccessor)
        {
            _http = httpClient;
            _tokenAccessor = tokenAccessor;
            // BaseAddress được set trong AddHttpClient("CatalogService") là: https://localhost:5158/catalog/
        }

        public async Task<BaseResponse<BookingDetailDto>> GetServiceByIdAsync(int id, CancellationToken ct = default)
        {
            // Gắn token
            var token = _tokenAccessor.GetAccessToken();
            _http.DefaultRequestHeaders.Remove("Authorization");
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            // Gọi Ocelot: GET https://localhost:5158/catalog/services/{serviceId}
            var resp = await _http.GetAsync($"bookingDetails/{id}", ct);
            //var resp = await _http.GetAsync($"/catalog/services/{serviceId}", ct);
            // Nếu gặp redirect (nếu Ocelot forward redirect về URL khác)
            if (resp.StatusCode == HttpStatusCode.TemporaryRedirect ||
                resp.StatusCode == HttpStatusCode.MovedPermanently ||
                resp.StatusCode == HttpStatusCode.PermanentRedirect)
            {
                var redirectUri = resp.Headers.Location!;
                // Giữ nguyên header Authorization
                resp = await _http.GetAsync(redirectUri, ct);
            }

            var json = await resp.Content.ReadAsStringAsync(ct);

            if (!resp.IsSuccessStatusCode)
            {
                return BaseResponse<BookingDetailDto>.ErrorResponse(
                    $"HTTP {(int)resp.StatusCode} - {resp.ReasonPhrase}"
                );
            }

            var svc = JsonSerializer.Deserialize<BookingDetailDto>(json, _jsonOptions);
            if (svc is null)
                return BaseResponse<BookingDetailDto>.ErrorResponse("Không thể deserialize ServiceDto");

            return BaseResponse<BookingDetailDto>.OkResponse(svc);
        }
        public async Task<BaseResponse<AccountDto>> GetAccountByIdAsync(int id, CancellationToken ct = default)
        {
            // Gắn token
            var token = _tokenAccessor.GetAccessToken();
            _http.DefaultRequestHeaders.Remove("Authorization");
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            // Gọi Ocelot: GET https://localhost:5158/catalog/services/{serviceId}
            var resp = await _http.GetAsync($"accounts/{id}", ct);
            //var resp = await _http.GetAsync($"/catalog/services/{serviceId}", ct);
            // Nếu gặp redirect (nếu Ocelot forward redirect về URL khác)
            if (resp.StatusCode == HttpStatusCode.TemporaryRedirect ||
                resp.StatusCode == HttpStatusCode.MovedPermanently ||
                resp.StatusCode == HttpStatusCode.PermanentRedirect)
            {
                var redirectUri = resp.Headers.Location!;
                // Giữ nguyên header Authorization
                resp = await _http.GetAsync(redirectUri, ct);
            }

            var json = await resp.Content.ReadAsStringAsync(ct);

            if (!resp.IsSuccessStatusCode)
            {
                return BaseResponse<AccountDto>.ErrorResponse(
                    $"HTTP {(int)resp.StatusCode} - {resp.ReasonPhrase}"
                );
            }

            var svc = JsonSerializer.Deserialize<AccountDto>(json, _jsonOptions);
            if (svc is null)
                return BaseResponse<AccountDto>.ErrorResponse("Không thể deserialize ServiceDto");

            return BaseResponse<AccountDto>.OkResponse(svc);
        }

    }
}
