using CozyCare.SharedKernel.Base;
using CozyCare.ViewModels.DTOs;

namespace CozyCare.PaymentService.Application.Interfaces
{
    public interface IPaymentsService
    {
        Task<BaseResponse<PaymentDto>> GetByIdAsync(int id);
        Task<BaseResponse<List<PaymentDto>>> GetAllAsync();
    }
}
