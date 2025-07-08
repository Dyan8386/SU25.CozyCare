using CozyCare.SharedKernel.Base;
using CozyCare.SharedKernel.Store;
using CozyCare.ViewModels.DTOs;
using System.Net.Http.Headers;
using System.Text.Json;

namespace CozyCare.BookingService.Application.Externals
{
    public class IdentityApiClient : IIdentityApiClient
    {
        private readonly HttpClient _http;
        private static readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };
        private readonly ITokenAccessor _tokenAccessor;
        public IdentityApiClient(HttpClient httpClient, ITokenAccessor tokenAccessor)
        {
            _http = httpClient;
            _tokenAccessor = tokenAccessor;
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
