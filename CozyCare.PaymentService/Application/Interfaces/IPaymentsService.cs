using CozyCare.SharedKernel.Base;
using CozyCare.ViewModels.DTOs;

namespace CozyCare.PaymentService.Application.Interfaces
{
    public interface IPaymentsService
    {
        Task<BaseResponse<PaymentDto>> CreateAsync(CreatePaymentDto request);
        Task<BaseResponse<PaymentDto>> GetByIdAsync(int id);
        Task<BaseResponse<List<PaymentDto>>> GetAllAsync();
        //Task<BaseResponse<bool>> CancelAsync(int id, string? reason = null);
    }
}
