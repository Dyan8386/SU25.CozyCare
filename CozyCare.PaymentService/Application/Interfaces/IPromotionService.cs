using CozyCare.SharedKernel.Base;
using CozyCare.ViewModels.DTOs;

namespace CozyCare.PaymentService.Application.Interfaces
{
    public interface IPromotionService
    {
        Task<BaseResponse<PromotionDto>> CreateAsync(CreatePromotionDto request);
        Task<BaseResponse<PromotionDto>> UpdateAsync(string code, UpdatePromotionDto request);
        Task<BaseResponse<PromotionDto>> GetByCodeAsync(string code);
        Task<BaseResponse<List<PromotionDto>>> GetAllAsync();
    }
}
