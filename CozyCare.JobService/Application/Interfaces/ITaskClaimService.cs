using CozyCare.JobService.Domain.Entities;
using CozyCare.SharedKernel.Base;
using CozyCare.ViewModels.DTOs;
using System.Linq.Expressions;

namespace CozyCare.JobService.Application.Interfaces
{
    public interface ITaskClaimService
    {
        Task<BaseResponse<IEnumerable<TaskClaimDto>>> SearchAsync(Expression<Func<TaskClaim, bool>> filter);
        Task<BaseResponse<IEnumerable<TaskClaimDto>>> GetAllAsync();
        Task<BaseResponse<TaskClaimDto>> GetByIdAsync(int id);
        Task<BaseResponse<TaskClaimDto>> CreateAsync(CreateTaskClaimDto dto);
        Task<BaseResponse<string>> UpdateAsync(int id, UpdateTaskClaimDto dto);
        Task<BaseResponse<string>> DeleteAsync(int id);
        Task<BaseResponse<bool>> ChangeStatusTaskClaim(int id);


    }
}
