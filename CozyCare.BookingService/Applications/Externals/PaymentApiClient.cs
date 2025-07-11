using CozyCare.SharedKernel.Base;
using CozyCare.SharedKernel.Store;
using CozyCare.ViewModels.DTOs;
using System.Net.Http.Headers;
using System.Net;
using System.Text.Json;

namespace CozyCare.BookingService.Applications.Externals
{
	public class PaymentApiClient : IPaymentApiClient
	{
		private readonly HttpClient _http;
		private static readonly JsonSerializerOptions _jsonOptions = new()
		{
			PropertyNameCaseInsensitive = true
		};
		private readonly ITokenAccessor _tokenAccessor;

		public PaymentApiClient(HttpClient httpClient, ITokenAccessor tokenAccessor)
		{
			_http = httpClient;
			_tokenAccessor = tokenAccessor;
			// BaseAddress được set trong AddHttpClient("PaymentService") là: https://localhost:5159/payment/
		}

		public async Task<BaseResponse<PaymentDto>> GetPaymentByIdAsync(int paymentId, CancellationToken ct = default)
		{
			// Gắn token
			var token = _tokenAccessor.GetAccessToken();
			_http.DefaultRequestHeaders.Remove("Authorization");
			_http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

			// (1) Đảm bảo đã set BaseAddress = https://localhost:5158/
			//    nếu chưa: _http.BaseAddress = new Uri("https://localhost:5158/");

			// Gọi Ocelot
			var resp = await _http.GetAsync($"payments/{paymentId}", ct);

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
				return BaseResponse<PaymentDto>.ErrorResponse(
					$"HTTP {(int)resp.StatusCode} - {resp.ReasonPhrase}"
				);
			}

			// (3) Deserialize vào wrapper BaseResponse<AccountDto>
			var wrapper = JsonSerializer.Deserialize<BaseResponse<PaymentDto>>(json, _jsonOptions);
			if (wrapper is null)
				return BaseResponse<PaymentDto>.ErrorResponse("Không thể deserialize thành BaseResponse<AccountDto>");

			// (4) Trả về kết quả
			return BaseResponse<PaymentDto>.OkResponse(wrapper.Data);
		}

		public async Task<BaseResponse<PromotionDto>> GetPromotionByCode(string code, CancellationToken ct = default)
		{
			// Gắn token
			var token = _tokenAccessor.GetAccessToken();
			_http.DefaultRequestHeaders.Remove("Authorization");
			_http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

			// (1) Đảm bảo đã set BaseAddress = https://localhost:5158/
			//    nếu chưa: _http.BaseAddress = new Uri("https://localhost:5158/");

			// Gọi Ocelot
			var resp = await _http.GetAsync($"promotions/{code}", ct);

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
				return BaseResponse<PromotionDto>.ErrorResponse(
					$"HTTP {(int)resp.StatusCode} - {resp.ReasonPhrase}"
				);
			}

			// (3) Deserialize vào wrapper BaseResponse<AccountDto>
			var wrapper = JsonSerializer.Deserialize<BaseResponse<PromotionDto>>(json, _jsonOptions);
			if (wrapper is null)
				return BaseResponse<PromotionDto>.ErrorResponse("Không thể deserialize thành BaseResponse<AccountDto>");

			// (4) Trả về kết quả
			return BaseResponse<PromotionDto>.OkResponse(wrapper.Data);
		}
	}
}
