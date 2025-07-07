using CozyCare.SharedKernel.Base;
using CozyCare.ViewModels.DTOs;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace CozyCare.BookingService.Applications.External
{
    public class CatalogApiClient : ICatalogApiClient
    {
        private readonly HttpClient _http;
        private static readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public CatalogApiClient(HttpClient httpClient)
        {
            _http = httpClient;
            // BaseAddress đã được set bởi AddHttpClient("CatalogService") là https://localhost:5158/catalog
        }

        public async Task<BaseResponse<IEnumerable<CategoryDto>>> GetAllCategoriesAsync(CancellationToken ct = default)
        {
            // Gọi lên Ocelot: GET https://localhost:5158/catalog/categories
            var resp = await _http.GetAsync("/categories", ct);
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
            var resp = await _http.GetAsync($"/services/{serviceId}", ct);
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
