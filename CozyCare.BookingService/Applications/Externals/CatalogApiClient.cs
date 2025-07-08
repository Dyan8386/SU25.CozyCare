using CozyCare.SharedKernel.Base;
using CozyCare.SharedKernel.Store;
using CozyCare.ViewModels.DTOs;
using System;
using System.Collections.Generic;
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
        private static readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };
        private readonly ITokenAccessor _tokenAccessor;
        public CatalogApiClient(HttpClient httpClient, ITokenAccessor tokenAccessor)
        {
            _http = httpClient;
            _tokenAccessor = tokenAccessor;
            // BaseAddress đã được set bởi AddHttpClient("CatalogService") là https://localhost:5158/catalog
        }

        public async Task<BaseResponse<IEnumerable<CategoryDto>>> GetAllCategoriesAsync(CancellationToken ct = default)
        {
            // Móc nối Token
            var token = _tokenAccessor.GetAccessToken();
            _http.DefaultRequestHeaders.Remove("Authorization");
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);


            // Gọi lên Ocelot: GET https://localhost:5158/catalog/categories
            var resp = await _http.GetAsync("/catalog/categories", ct);
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
            // Gọi lên Ocelot: GET https://localhost:5158/catalog/services/{serviceId}
            var resp = await _http.GetAsync($"/catalog/services/{serviceId}", ct);
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
