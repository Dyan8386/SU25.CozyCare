using CozyCare.JobService.Domain.Entities;
using CozyCare.SharedKernel.Base;
using System.Linq.Expressions;
using CozyCare.ViewModels.DTOs;
namespace CozyCare.JobService.Application.Interfaces
{
    public interface IReviewService
    {
        Task<BaseResponse<IEnumerable<ReviewDto>>> SearchAsync(Expression<Func<Review, bool>> filter);
        Task<BaseResponse<IEnumerable<ReviewDto>>> GetAllAsync();
        Task<BaseResponse<ReviewDto>> GetByIdAsync(int id);
        Task<BaseResponse<ReviewDto>> CreateAsync(CreateReviewDto dto);
        Task<BaseResponse<string>> UpdateAsync(int id, UpdateReviewDto dto);
        Task<BaseResponse<string>> DeleteAsync(int id);

        Task<BaseResponse<IEnumerable<ReviewDto>>> GeReviewByAccountIdAsync(int accountId);
        Task<BaseResponse<IEnumerable<ReviewDto>>> GetReviewByDetailIdAsync(int detailid);
    }
}
