using CozyCare.SharedKernel.Base;
using CozyCare.ViewModels.DTOs;

namespace CozyCare.BookingService.Applications.Externals
{
	public interface IPaymentApiClient
	{
		Task<BaseResponse<PaymentDto>> GetPaymentByIdAsync(int paymentId, CancellationToken ct = default);
		Task<BaseResponse<PromotionDto>> GetPromotionByCode(string code, CancellationToken ct = default);
	}
}
