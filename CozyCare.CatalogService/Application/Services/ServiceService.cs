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
    public class ServiceService : IServiceService
    {
        private readonly ICatalogUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ServiceService(ICatalogUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<IEnumerable<ServiceDto>>> GetAllAsync()
        {
            var services = await _unitOfWork.Services.GetAllAsync("ServiceDetails");
            return BaseResponse<IEnumerable<ServiceDto>>.OkResponse(_mapper.Map<IEnumerable<ServiceDto>>(services));
        }

        public async Task<BaseResponse<ServiceDto>> GetByIdAsync(int id)
        {
            var service = await _unitOfWork.Services.GetByIdAsync(id);
            if (service == null)
                return BaseResponse<ServiceDto>.NotFoundResponse("Service not found");
            return BaseResponse<ServiceDto>.OkResponse(_mapper.Map<ServiceDto>(service));
        }

        public async Task<BaseResponse<ServiceDto>> CreateAsync(CreateServiceDto dto)
        {
            var entity = _mapper.Map<Service>(dto);
            entity.createdDate = CoreHelper.SystemTimeNow.UtcDateTime;

            await _unitOfWork.Services.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();

            return BaseResponse<ServiceDto>.OkResponse(_mapper.Map<ServiceDto>(entity));
        }

        public async Task<BaseResponse<string>> UpdateAsync(int id, UpdateServiceDto dto)
        {
            var entity = await _unitOfWork.Services.GetByIdAsync(id);
            if (entity == null)
                throw new BaseException.BadRequestException("service_not_found", "Service not found");

            _mapper.Map(dto, entity);
            entity.updatedDate = CoreHelper.SystemTimeNow.UtcDateTime;

            _unitOfWork.Services.Update(entity);
            await _unitOfWork.SaveChangesAsync();

            return BaseResponse<string>.OkResponse("Updated successfully");
        }

        public async Task<BaseResponse<string>> DeleteAsync(int id)
        {
            var entity = await _unitOfWork.Services.GetByIdAsync(id);
            if (entity == null)
                throw new BaseException.BadRequestException("service_not_found", "Service not found");

            _unitOfWork.Services.Delete(entity);
            await _unitOfWork.SaveChangesAsync();

            return BaseResponse<string>.OkResponse("Deleted successfully");
        }

        public async Task<BaseResponse<IEnumerable<ServiceDto>>> SearchAsync(Expression<Func<Service, bool>> filter)
        {
            var results = await _unitOfWork.Services.SearchAsync(filter, includeProperties: "ServiceDetail,Category");
            var dtos = _mapper.Map<IEnumerable<ServiceDto>>(results);
            return BaseResponse<IEnumerable<ServiceDto>>.OkResponse(dtos);
        }
        public async Task<BaseResponse<string>> SetServiceStatusAsync(int id, LockServiceDto dto)
        {
            var service = await _unitOfWork.Services.GetByIdAsync(id);
            if (service == null)
                return BaseResponse<string>.NotFoundResponse("Service not found");

            service.isActive = dto.IsActive;
            service.updatedDate = DateTime.UtcNow;

            _unitOfWork.Services.Update(service);
            await _unitOfWork.SaveChangesAsync();

            return BaseResponse<string>.OkResponse("Service status updated");
        }
        public async Task<BaseResponse<IEnumerable<ServiceDto>>> GetByCategoryIdAsync(int categoryId)
        {
            // Lấy danh sách services theo categoryId
            var results = await _unitOfWork.Services
                .SearchAsync(s => s.categoryId == categoryId, includeProperties: "ServiceDetails,category");
            var dtos = _mapper.Map<IEnumerable<ServiceDto>>(results);
            return BaseResponse<IEnumerable<ServiceDto>>.OkResponse(dtos);
        }
    }
}
