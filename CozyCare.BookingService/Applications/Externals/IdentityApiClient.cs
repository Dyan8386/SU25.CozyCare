using CozyCare.SharedKernel.Base;
using CozyCare.SharedKernel.Store;
using CozyCare.ViewModels.DTOs;
using System.Net;
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

		public async Task<BaseResponse<AccountDto>> GetAccountById(int accountId, CancellationToken ct = default)
		{
			// Gắn token
			var token = _tokenAccessor.GetAccessToken();
			_http.DefaultRequestHeaders.Remove("Authorization");
			_http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

			// (1) Đảm bảo đã set BaseAddress = https://localhost:5158/
			//    nếu chưa: _http.BaseAddress = new Uri("https://localhost:5158/");

			// Gọi Ocelot
			var resp = await _http.GetAsync($"account/{accountId}", ct);

			// (2) Xử lý redirect như trước
			if (resp.StatusCode == HttpStatusCode.TemporaryRedirect ||
				resp.StatusCode == HttpStatusCode.MovedPermanently ||
				resp.StatusCode == HttpStatusCode.PermanentRedirect)
			{
				var redirectUri = resp.Headers.Location!;
				resp = await _http.GetAsync(redirectUri, ct);
			}

			var json = await resp.Content.ReadAsStringAsync(ct);

			if (!resp.IsSuccessStatusCode)
			{
				return BaseResponse<AccountDto>.ErrorResponse(
					$"HTTP {(int)resp.StatusCode} - {resp.ReasonPhrase}"
				);
			}

			// (3) Deserialize vào wrapper BaseResponse<AccountDto>
			var wrapper = JsonSerializer.Deserialize<BaseResponse<AccountDto>>(json, _jsonOptions);
			if (wrapper is null)
				return BaseResponse<AccountDto>.ErrorResponse("Không thể deserialize thành BaseResponse<AccountDto>");

			// (4) Trả về kết quả
			return BaseResponse<AccountDto>.OkResponse(wrapper.Data);
		}

	}
}
    
