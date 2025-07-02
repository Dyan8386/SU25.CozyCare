using CozyCare.CatalogService.Domain.Entities;
using CozyCare.SharedKernel.Base;
using CozyCare.ViewModels.DTOs;
using System.Linq.Expressions;

namespace CozyCare.CatalogService.Application.Interfaces
{
    public interface IServiceService
    {
        Task<BaseResponse<IEnumerable<ServiceDto>>> SearchAsync(Expression<Func<Service, bool>> filter);
        Task<BaseResponse<IEnumerable<ServiceDto>>> GetAllAsync();
        Task<BaseResponse<ServiceDto>> GetByIdAsync(int id);
        Task<BaseResponse<ServiceDto>> CreateAsync(CreateServiceDto dto);
        Task<BaseResponse<string>> UpdateAsync(int id, UpdateServiceDto dto);
        Task<BaseResponse<string>> DeleteAsync(int id);
        Task<BaseResponse<string>> SetServiceStatusAsync(int id, LockServiceDto dto);
    }
}
