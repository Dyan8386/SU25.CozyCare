using AutoMapper;
using CozyCare.CatalogService.Application.Interfaces;
using CozyCare.CatalogService.Domain.Entities;
using CozyCare.CatalogService.Infrastructure;
using CozyCare.SharedKernel.Base;
using CozyCare.SharedKernel.Utils;
using CozyCare.ViewModels.DTOs;
using System.Linq.Expressions;

namespace CozyCare.CatalogService.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICatalogUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryService(ICatalogUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<IEnumerable<CategoryDto>>> GetAllAsync()
        {
            var categories = await _unitOfWork.Categories.GetAllAsync();
            var dtos = _mapper.Map<IEnumerable<CategoryDto>>(categories);
            return BaseResponse<IEnumerable<CategoryDto>>.OkResponse(dtos);
        }

        public async Task<BaseResponse<CategoryDto>> GetByIdAsync(int id)
        {
            var entity = await _unitOfWork.Categories.GetByIdAsync(id);
            if (entity == null)
                return BaseResponse<CategoryDto>.NotFoundResponse("Category not found");

            return BaseResponse<CategoryDto>.OkResponse(_mapper.Map<CategoryDto>(entity));
        }

        public async Task<BaseResponse<CategoryDto>> CreateAsync(CreateCategoryDto dto)
        {
            var entity = _mapper.Map<Category>(dto);
            entity.createdDate = CoreHelper.SystemTimeNow.UtcDateTime;

            await _unitOfWork.Categories.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();

            var resultDto = _mapper.Map<CategoryDto>(entity);
            return BaseResponse<CategoryDto>.OkResponse(resultDto);
        }

        public async Task<BaseResponse<string>> UpdateAsync(int id, UpdateCategoryDto dto)
        {
            var entity = await _unitOfWork.Categories.GetByIdAsync(id);
            if (entity == null)
                throw new BaseException.BadRequestException("category_not_found", "Category not found");

            _mapper.Map(dto, entity);
            entity.updatedDate = CoreHelper.SystemTimeNow.UtcDateTime;

            _unitOfWork.Categories.Update(entity);
            await _unitOfWork.SaveChangesAsync();

            return BaseResponse<string>.OkResponse("Updated successfully");
        }

        public async Task<BaseResponse<string>> DeleteAsync(int id)
        {
            var entity = await _unitOfWork.Categories.GetByIdAsync(id);
            if (entity == null)
                throw new BaseException.BadRequestException("category_not_found", "Category not found");

            _unitOfWork.Categories.Delete(entity);
            await _unitOfWork.SaveChangesAsync();

            return BaseResponse<string>.OkResponse("Deleted successfully");
        }

        public async Task<BaseResponse<IEnumerable<CategoryDto>>> SearchAsync(Expression<Func<Category, bool>> filter)
        {
            var results = await _unitOfWork.Categories.SearchAsync(filter, includeProperties: "Service");
            var dtos = _mapper.Map<IEnumerable<CategoryDto>>(results);
            return BaseResponse<IEnumerable<CategoryDto>>.OkResponse(dtos);
        }

        public async Task<BaseResponse<string>> SetCategoryStatusAsync(int id, LockCategoryDto dto)
        {
            var category = await _unitOfWork.Categories.GetByIdAsync(id);
            if (category == null)
                return BaseResponse<string>.NotFoundResponse("Category not found");

            category.isActive = dto.IsActive;
            category.updatedDate = DateTime.UtcNow;

            _unitOfWork.Categories.Update(category);
            await _unitOfWork.SaveChangesAsync();

            return BaseResponse<string>.OkResponse("Category status updated");
        }
    }
}
