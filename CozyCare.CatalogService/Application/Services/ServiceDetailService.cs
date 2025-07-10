using AutoMapper;
using CozyCare.CatalogService.Application.Interfaces;
using CozyCare.CatalogService.Domain.Entities;
using CozyCare.CatalogService.Infrastructure;
using CozyCare.SharedKernel.Base;
using CozyCare.ViewModels.DTOs;
using System.Linq.Expressions;

namespace CozyCare.CatalogService.Application.Services
{
    public class ServiceDetailService : IServiceDetailService
    {
        private readonly ICatalogUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ServiceDetailService(ICatalogUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<IEnumerable<ServiceDetailDto>>> GetAllAsync()
        {
            var details = await _unitOfWork.ServiceDetails.GetAllAsync();
            return BaseResponse<IEnumerable<ServiceDetailDto>>.OkResponse(_mapper.Map<IEnumerable<ServiceDetailDto>>(details));
        }

        public async Task<BaseResponse<ServiceDetailDto>> GetByIdAsync(int id)
        {
            var detail = await _unitOfWork.ServiceDetails.GetByIdAsync(id);
            if (detail == null)
                return BaseResponse<ServiceDetailDto>.NotFoundResponse("ServiceDetail not found");

            return BaseResponse<ServiceDetailDto>.OkResponse(_mapper.Map<ServiceDetailDto>(detail));
        }

        public async Task<BaseResponse<ServiceDetailDto>> CreateAsync(CreateServiceDetailDto dto)
        {
            var entity = _mapper.Map<ServiceDetail>(dto);

            await _unitOfWork.ServiceDetails.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();

            return BaseResponse<ServiceDetailDto>.OkResponse(_mapper.Map<ServiceDetailDto>(entity));
        }

        public async Task<BaseResponse<string>> UpdateAsync(int id, UpdateServiceDetailDto dto)
        {
            var entity = await _unitOfWork.ServiceDetails.GetByIdAsync(id);
            if (entity == null)
                throw new BaseException.BadRequestException("service_detail_not_found", "ServiceDetail not found");

            _mapper.Map(dto, entity);
            _unitOfWork.ServiceDetails.Update(entity);
            await _unitOfWork.SaveChangesAsync();

            return BaseResponse<string>.OkResponse("Updated successfully");
        }

        public async Task<BaseResponse<string>> DeleteAsync(int id)
        {
            var entity = await _unitOfWork.ServiceDetails.GetByIdAsync(id);
            if (entity == null)
                throw new BaseException.BadRequestException("service_detail_not_found", "ServiceDetail not found");

            _unitOfWork.ServiceDetails.Delete(entity);
            await _unitOfWork.SaveChangesAsync();

            return BaseResponse<string>.OkResponse("Deleted successfully");
        }

        public async Task<BaseResponse<IEnumerable<ServiceDetailDto>>> SearchAsync(Expression<Func<ServiceDetail, bool>> filter)
        {
            var results = await _unitOfWork.ServiceDetails.SearchAsync(filter, includeProperties: "Service");
            var dtos = _mapper.Map<IEnumerable<ServiceDetailDto>>(results);
            return BaseResponse<IEnumerable<ServiceDetailDto>>.OkResponse(dtos);
        }
        public async Task<BaseResponse<string>> SetServiceDetailStatusAsync(int id, LockServiceDetailDto dto)
        {
            var detail = await _unitOfWork.ServiceDetails.GetByIdAsync(id);
            if (detail == null)
                return BaseResponse<string>.NotFoundResponse("Service detail not found");

            detail.isActive = dto.IsActive;

            _unitOfWork.ServiceDetails.Update(detail);
            await _unitOfWork.SaveChangesAsync();

            return BaseResponse<string>.OkResponse("Service detail status updated");
        }

        public async Task<BaseResponse<IEnumerable<ServiceDetailDto>>> GetByServiceIdAsync(int serviceId)
        {
            var results = await _unitOfWork.ServiceDetails
                .SearchAsync(d => d.serviceId == serviceId, includeProperties: "service");
            var dtos = _mapper.Map<IEnumerable<ServiceDetailDto>>(results);
            return BaseResponse<IEnumerable<ServiceDetailDto>>.OkResponse(dtos);
        }
    }
}
