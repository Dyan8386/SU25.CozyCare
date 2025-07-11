using AutoMapper;
using CozyCare.JobService.Application.Externals;
using CozyCare.JobService.Application.Interfaces;
using CozyCare.JobService.Domain.Entities;
using CozyCare.JobService.Infrastructure;
using CozyCare.SharedKernel.Base;
using CozyCare.SharedKernel.Store;
using CozyCare.SharedKernel.Utils;
using CozyCare.ViewModels.DTOs;
using System.Linq.Expressions;

namespace CozyCare.JobService.Application.Services
{
    public class TaskClaimService : ITaskClaimService
    {
        private readonly IJobUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IJobApiClient _jobApiClient;

        public TaskClaimService(IJobUnitOfWork unitOfWork, IMapper mapper, IJobApiClient jobApiClient)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _jobApiClient = jobApiClient;
        }

        public async Task<BaseResponse<TaskClaimDto>> CreateAsync(CreateTaskClaimDto dto)
        {
            var customer = await _jobApiClient.GetAccountByIdAsync(dto.housekeeperId);
            if (customer.StatusCode != StatusCodeHelper.OK || customer.Data == null)
                return customer.StatusCode == StatusCodeHelper.NotFound
                    ? BaseResponse<TaskClaimDto>.NotFoundResponse($"housekeeper with ID {dto.housekeeperId} not found.")
                    : BaseResponse<TaskClaimDto>.ErrorResponse(customer.Message);

            var bookingDetail = await _jobApiClient.GetBookingDetailByIdAsync(dto.detailId);
            if (bookingDetail.StatusCode != StatusCodeHelper.OK || bookingDetail.Data == null)
                throw new BaseException.BadRequestException("booking_detail_not_found", "Booking detail not found");


            var entity = _mapper.Map<TaskClaim>(dto);

            await _unitOfWork.TaskClaims.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();

            var resultDto = _mapper.Map<TaskClaimDto>(entity);
            return BaseResponse<TaskClaimDto>.OkResponse(resultDto);
        }

        public async Task<BaseResponse<string>> DeleteAsync(int id)
        {
            var entity = await _unitOfWork.TaskClaims.GetByIdAsync(id);
            if (entity == null)
                throw new BaseException.BadRequestException("TaskClaims_not_found", "TaskClaims not found");

            _unitOfWork.TaskClaims.Delete(entity);
            await _unitOfWork.SaveChangesAsync();

            return BaseResponse<string>.OkResponse("Deleted successfully");
        }

        public async Task<BaseResponse<IEnumerable<TaskClaimDto>>> GetAllAsync()
        {
            var taskClaims = await _unitOfWork.TaskClaims.GetAllAsync();
            var dtos = _mapper.Map<IEnumerable<TaskClaimDto>>(taskClaims);
            return BaseResponse<IEnumerable<TaskClaimDto>>.OkResponse(dtos);
        }

        public async Task<BaseResponse<TaskClaimDto>> GetByIdAsync(int id)
        {
            var entity = await _unitOfWork.TaskClaims.GetByIdAsync(id);
            if (entity == null)
                return BaseResponse<TaskClaimDto>.NotFoundResponse("Review not found");
            return BaseResponse<TaskClaimDto>.OkResponse(_mapper.Map<TaskClaimDto>(entity));
        }

        public async Task<BaseResponse<IEnumerable<TaskClaimDto>>> SearchAsync(Expression<Func<TaskClaim, bool>> filter)
        {
            var results = await _unitOfWork.TaskClaims.SearchAsync(filter, includeProperties: "Review");
            var dtos = _mapper.Map<IEnumerable<TaskClaimDto>>(results);
            return BaseResponse<IEnumerable<TaskClaimDto>>.OkResponse(dtos);
        }


        public async Task<BaseResponse<string>> UpdateAsync(int id, UpdateTaskClaimDto dto)
        {
            var entity = await _unitOfWork.TaskClaims.GetByIdAsync(id);

            if (entity == null)
                throw new BaseException.BadRequestException("TaskClaims_not_found", "TaskClaims not found");
            _mapper.Map(dto, entity);
            _unitOfWork.TaskClaims.Update(entity);
            await _unitOfWork.SaveChangesAsync();
            return BaseResponse<string>.OkResponse("TaskClaims updated successfully");
        }
        // Add methods for task claim operations here
        // For example: CreateClaimAsync, GetClaimsAsync, UpdateClaimAsync, etc.

        public async Task<BaseResponse<bool>> ChangeStatusTaskClaim(int id)
        {
            var entity = await _unitOfWork.TaskClaims.GetByIdAsync(id);

            if (entity == null)
                throw new BaseException.BadRequestException("TaskClaims_not_found", "TaskClaims not found");
            entity.statusId = 2;
            _unitOfWork.TaskClaims.Update(entity);
            await _unitOfWork.SaveChangesAsync();
            return BaseResponse<bool>.OkResponse(true); // Fixed by removing the second argument
        }
        public async Task<BaseResponse<IEnumerable<TaskClaimDto>>> GeTaskClaimByAccountIdAsync(int accountId)
        {
            var taskClaims = await _unitOfWork.TaskClaims.SearchAsync(c => c.housekeeperId == accountId);
            if (taskClaims == null || !taskClaims.Any())
                return BaseResponse<IEnumerable<TaskClaimDto>>.NotFoundResponse("No task claims found for this account");
            var TaskClaimResponses = _mapper.Map<IEnumerable<TaskClaimDto>>(taskClaims);

            return BaseResponse<IEnumerable<TaskClaimDto>>.OkResponse(TaskClaimResponses);
        }
        public async Task<BaseResponse<IEnumerable<TaskClaimDto>>> GetTaskClaimByDetailIdAsync(int detailid)
        {
            var taskClaims = await _unitOfWork.TaskClaims.SearchAsync(c => c.detailId == detailid);
            if (taskClaims == null || !taskClaims.Any())
                return BaseResponse<IEnumerable<TaskClaimDto>>.NotFoundResponse("No task claims found for this booking detail");
            var TaskClaimResponses = _mapper.Map<IEnumerable<TaskClaimDto>>(taskClaims);
            return BaseResponse<IEnumerable<TaskClaimDto>>.OkResponse(TaskClaimResponses);
        }
    }
}
