using AutoMapper;
using CozyCare.JobService.Application.Interfaces;
using CozyCare.JobService.Domain.Entities;
using CozyCare.JobService.Infrastructure;
using CozyCare.SharedKernel.Base;
using CozyCare.SharedKernel.Utils;
using CozyCare.ViewModels.DTOs;
using System.Linq.Expressions;

namespace CozyCare.JobService.Application.Services
{
    public class TaskClaimStatusService : ITaskClaimStatusService
    {
        private readonly IJobUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public TaskClaimStatusService(IJobUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<TaskClaimStatusDto>> CreateAsync(CreateAndUpdateTaskClaimStatusDto dto)
        {
            var entity = _mapper.Map<TaskClaimStatus>(dto);
            await _unitOfWork.TaskClaimStatus.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();

            var resultDto = _mapper.Map<TaskClaimStatusDto>(entity);
            return BaseResponse<TaskClaimStatusDto>.OkResponse(resultDto);
        }

        public async Task<BaseResponse<string>> DeleteAsync(int id)
        {
            var entity = await _unitOfWork.TaskClaimStatus.GetByIdAsync(id);
            if (entity == null)
                throw new BaseException.BadRequestException("TaskClaimStatus_not_found", "Category not found");
            _unitOfWork.TaskClaimStatus.Delete(entity);
            await _unitOfWork.SaveChangesAsync();
            return BaseResponse<string>.OkResponse("Deleted successfully");
        }

        public async Task<BaseResponse<IEnumerable<TaskClaimStatusDto>>> GetAllAsync()
        {
            var statuses = await _unitOfWork.TaskClaimStatus.GetAllAsync();
            var dtos = _mapper.Map<IEnumerable<TaskClaimStatusDto>>(statuses);
            return BaseResponse<IEnumerable<TaskClaimStatusDto>>.OkResponse(dtos);
        }

        public async Task<BaseResponse<TaskClaimStatusDto>> GetByIdAsync(int id)
        {
            var entity = await _unitOfWork.TaskClaimStatus.GetByIdAsync(id);
            if (entity == null)
                return BaseResponse<TaskClaimStatusDto>.NotFoundResponse("TaskClaimStatus not found");
            return BaseResponse<TaskClaimStatusDto>.OkResponse(_mapper.Map<TaskClaimStatusDto>(entity));
        }

       

        public async  Task<BaseResponse<IEnumerable<TaskClaimStatusDto>>> SearchAsync(Expression<Func<TaskClaimStatus, bool>> filter)
        {
            var results = await _unitOfWork.TaskClaimStatus.SearchAsync(filter, includeProperties: "Service");
            var dtos = _mapper.Map<IEnumerable<TaskClaimStatusDto>>(results);
            return BaseResponse<IEnumerable<TaskClaimStatusDto>>.OkResponse(dtos);
        }

        public async Task<BaseResponse<string>> UpdateAsync(int id, CreateAndUpdateTaskClaimStatusDto dto)
        {
            var entity = await _unitOfWork.TaskClaimStatus.GetByIdAsync(id);
            if (entity == null)
                throw new BaseException.BadRequestException("TaskClaimStatus_not_found", "Category not found");

            _mapper.Map(dto, entity);

            _unitOfWork.TaskClaimStatus.Update(entity);
            await _unitOfWork.SaveChangesAsync();

            return BaseResponse<string>.OkResponse("Updated successfully");
        }
    }
}
