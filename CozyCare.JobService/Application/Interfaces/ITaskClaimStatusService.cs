using CozyCare.JobService.Domain.Entities;
using CozyCare.SharedKernel.Base;
using CozyCare.ViewModels.DTOs;
using System.Linq.Expressions;

namespace CozyCare.JobService.Application.Interfaces
{
    public interface ITaskClaimStatusService
    {
        Task<BaseResponse<IEnumerable<TaskClaimStatusDto>>> SearchAsync(Expression<Func<TaskClaimStatus, bool>> filter);
        Task<BaseResponse<IEnumerable<TaskClaimStatusDto>>> GetAllAsync();
        Task<BaseResponse<TaskClaimStatusDto>> GetByIdAsync(int id);
        Task<BaseResponse<TaskClaimStatusDto>> CreateAsync(CreateAndUpdateTaskClaimStatusDto dto);
        Task<BaseResponse<string>> UpdateAsync(int id, CreateAndUpdateTaskClaimStatusDto dto);
        Task<BaseResponse<string>> DeleteAsync(int id);
    }
}
