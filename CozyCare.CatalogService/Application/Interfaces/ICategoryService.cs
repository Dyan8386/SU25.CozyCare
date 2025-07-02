using CozyCare.CatalogService.Domain.Entities;
using CozyCare.SharedKernel.Base;
using CozyCare.ViewModels.DTOs;
using System.Linq.Expressions;

namespace CozyCare.CatalogService.Application.Interfaces
{
    public interface ICategoryService
    {
        Task<BaseResponse<IEnumerable<CategoryDto>>> SearchAsync(Expression<Func<Category, bool>> filter);
        Task<BaseResponse<IEnumerable<CategoryDto>>> GetAllAsync();
        Task<BaseResponse<CategoryDto>> GetByIdAsync(int id);
        Task<BaseResponse<CategoryDto>> CreateAsync(CreateCategoryDto dto);
        Task<BaseResponse<string>> UpdateAsync(int id, UpdateCategoryDto dto);
        Task<BaseResponse<string>> DeleteAsync(int id);
        Task<BaseResponse<string>> SetCategoryStatusAsync(int id, LockCategoryDto dto);
    }
}
