using CozyCare.JobService.Domain.Entities;
using CozyCare.SharedKernel.Base;
using CozyCare.ViewModels.DTOs;
using System.Linq.Expressions;

namespace CozyCare.JobService.Application.Interfaces
{
    public interface ITaskClaimService
    {
        Task<BaseResponse<IEnumerable<TaskClaimStatusDto>>> SearchAsync(Expression<Func<Review, bool>> filter);
        Task<BaseResponse<IEnumerable<TaskClaimStatusDto>>> GetAllAsync();
        Task<BaseResponse<TaskClaimStatusDto>> GetByIdAsync(int id);
        Task<BaseResponse<TaskClaimStatusDto>> CreateAsync(CreateReviewDto dto);
        Task<BaseResponse<string>> UpdateAsync(int id, UpdateReviewDto dto);
        Task<BaseResponse<string>> DeleteAsync(int id);
        Task<BaseResponse<string>> SetReviewStatusAsync(int id, LockReviewDto dto);
    }
}
