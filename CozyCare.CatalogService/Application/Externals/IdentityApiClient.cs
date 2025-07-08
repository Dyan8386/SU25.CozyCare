using CozyCare.SharedKernel.Base;
using CozyCare.ViewModels.DTOs;
using System.Net.Http.Headers;
using System.Text.Json;

namespace CozyCare.CatalogService.Application.Externals
{
    public class IdentityApiClient : IIdentityApiClient
    {
        private readonly HttpClient _http;

        public IdentityApiClient(HttpClient http)
        {
            _http = http;
        }

        public async Task<BaseResponse<CurrentAccountDto?>> GetCurrentAccountAsync(string accessToken)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "/api/account/current");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await _http.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                return BaseResponse<CurrentAccountDto?>.UnauthorizeResponse("Invalid access token");
            }

            var content = await response.Content.ReadAsStringAsync();
            var user = JsonSerializer.Deserialize<CurrentAccountDto>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return BaseResponse<CurrentAccountDto?>.OkResponse(user);
        }

    }

}
