using CozyCare.CatalogService.Domain.Entities;
using CozyCare.SharedKernel.Base;
using CozyCare.ViewModels.DTOs;
using System.Linq.Expressions;

namespace CozyCare.CatalogService.Application.Interfaces
{
    public interface IServiceDetailService
    {
        Task<BaseResponse<IEnumerable<ServiceDetailDto>>> SearchAsync(Expression<Func<ServiceDetail, bool>> filter);
        Task<BaseResponse<IEnumerable<ServiceDetailDto>>> GetAllAsync();
        Task<BaseResponse<ServiceDetailDto>> GetByIdAsync(int id);
        Task<BaseResponse<ServiceDetailDto>> CreateAsync(CreateServiceDetailDto dto);
        Task<BaseResponse<string>> UpdateAsync(int id, UpdateServiceDetailDto dto);
        Task<BaseResponse<string>> DeleteAsync(int id);
        Task<BaseResponse<string>> SetServiceDetailStatusAsync(int id, LockServiceDetailDto dto);
        Task<BaseResponse<IEnumerable<ServiceDetailDto>>> GetByServiceIdAsync(int serviceId);
    }
}
