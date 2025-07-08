using CozyCare.SharedKernel.Base;
using CozyCare.SharedKernel.Store;
using CozyCare.ViewModels.DTOs;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace CozyCare.BookingService.Applications.Externals
{
    public class CatalogApiClient : ICatalogApiClient
    {
        private readonly HttpClient _http;
        private readonly ITokenAccessor _tokenAccessor;
        private static readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public CatalogApiClient(HttpClient httpClient, ITokenAccessor tokenAccessor)
        {
            _http = httpClient;
            _tokenAccessor = tokenAccessor;
            // BaseAddress được set trong AddHttpClient("CatalogService") là: https://localhost:5158/catalog/
        }

        public async Task<BaseResponse<IEnumerable<CategoryDto>>> GetAllCategoriesAsync(CancellationToken ct = default)
        {
            // Gắn token
            var token = _tokenAccessor.GetAccessToken();
            _http.DefaultRequestHeaders.Remove("Authorization");
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            // Gọi Ocelot: GET https://localhost:5158/catalog/categories
            var resp = await _http.GetAsync("categories", ct);
            //var resp = await _http.GetAsync("/catalog/categories", ct);
            var json = await resp.Content.ReadAsStringAsync(ct);

            if (!resp.IsSuccessStatusCode)
            {
                return BaseResponse<IEnumerable<CategoryDto>>.ErrorResponse(
                    $"HTTP {(int)resp.StatusCode} - {resp.ReasonPhrase}"
                );
            }

            var data = JsonSerializer.Deserialize<IEnumerable<CategoryDto>>(json, _jsonOptions)
                       ?? new List<CategoryDto>();
            return BaseResponse<IEnumerable<CategoryDto>>.OkResponse(data);
        }

        public async Task<BaseResponse<ServiceDto>> GetServiceByIdAsync(int serviceId, CancellationToken ct = default)
        {
            // Gắn token
            var token = _tokenAccessor.GetAccessToken();
            _http.DefaultRequestHeaders.Remove("Authorization");
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            // Gọi Ocelot: GET https://localhost:5158/catalog/services/{serviceId}
            var resp = await _http.GetAsync($"services/{serviceId}", ct);
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
                return BaseResponse<ServiceDto>.ErrorResponse(
                    $"HTTP {(int)resp.StatusCode} - {resp.ReasonPhrase}"
                );
            }

            var svc = JsonSerializer.Deserialize<ServiceDto>(json, _jsonOptions);
            if (svc is null)
                return BaseResponse<ServiceDto>.ErrorResponse("Không thể deserialize ServiceDto");

            return BaseResponse<ServiceDto>.OkResponse(svc);
        }
    }
}
