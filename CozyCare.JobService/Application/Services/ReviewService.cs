using AutoMapper;
using Azure.Core;
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
    public class ReviewService : IReviewService
    {
        private readonly IJobUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IJobApiClient _jobApiClient;

        public ReviewService(IJobUnitOfWork unitOfWork, IMapper mapper , IJobApiClient jobApiClient)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _jobApiClient = jobApiClient;
        }

        public async Task<BaseResponse<ReviewDto>> CreateAsync(CreateReviewDto dto)
        {
            var customer = await _jobApiClient.GetAccountByIdAsync(dto.customerId);
            if (customer.StatusCode != StatusCodeHelper.OK || customer.Data == null)
                return customer.StatusCode == StatusCodeHelper.NotFound
                    ? BaseResponse<ReviewDto>.NotFoundResponse($"Customer with ID {dto.customerId} not found.")
                    : BaseResponse<ReviewDto>.ErrorResponse(customer.Message);

            var bookingDetail = await _jobApiClient.GetBookingDetailByIdAsync(dto.detailId);
            if (bookingDetail.StatusCode != StatusCodeHelper.OK || bookingDetail.Data == null)
                throw new BaseException.BadRequestException("booking_detail_not_found", "Booking detail not found");

            var entity = _mapper.Map<Review>(dto);
            entity.reviewDate = CoreHelper.SystemTimeNow.UtcDateTime;

            await _unitOfWork.Reviews.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();

            var resultDto = _mapper.Map<ReviewDto>(entity);
            return BaseResponse<ReviewDto>.OkResponse(resultDto);
        }

        public async Task<BaseResponse<string>> DeleteAsync(int id)
        {
            var entity = await _unitOfWork.Reviews.GetByIdAsync(id);
            if (entity == null)
                throw new BaseException.BadRequestException("Review_not_found", "Review not found");

            _unitOfWork.Reviews.Delete(entity);
            await _unitOfWork.SaveChangesAsync();

            return BaseResponse<string>.OkResponse("Deleted successfully");
        }

        public async Task<BaseResponse<IEnumerable<ReviewDto>>> GetAllAsync()
        {
            var review = await _unitOfWork.Reviews.GetAllAsync();
            var dtos = _mapper.Map<IEnumerable<ReviewDto>>(review);
            return BaseResponse<IEnumerable<ReviewDto>>.OkResponse(dtos);
        }

        public async Task<BaseResponse<ReviewDto>> GetByIdAsync(int id)
        {
            var entity = await _unitOfWork.Reviews.GetByIdAsync(id);
            if (entity == null)
                return BaseResponse<ReviewDto>.NotFoundResponse("Review not found");
            return BaseResponse<ReviewDto>.OkResponse(_mapper.Map<ReviewDto>(entity));
        }

        public async Task<BaseResponse<IEnumerable<ReviewDto>>> SearchAsync(Expression<Func<Review, bool>> filter)
        {
            var results = await _unitOfWork.Reviews.SearchAsync(filter, includeProperties: "Review");
            var dtos = _mapper.Map<IEnumerable<ReviewDto>>(results);
            return BaseResponse<IEnumerable<ReviewDto>>.OkResponse(dtos);
        }

        public async Task<BaseResponse<string>> UpdateAsync(int id, UpdateReviewDto dto)
        {
           var entity = await _unitOfWork.Reviews.GetByIdAsync(id);

            if (entity == null)
                throw new BaseException.BadRequestException("review_not_found", "Review not found");
            _mapper.Map(dto, entity);
            _unitOfWork.Reviews.Update(entity);
            await _unitOfWork.SaveChangesAsync();
            return BaseResponse<string>.OkResponse("Review updated successfully");
        }
        public async Task<BaseResponse<IEnumerable<ReviewDto>>> GeReviewByAccountIdAsync(int accountId)
        {
            var reviews = await _unitOfWork.Reviews.SearchAsync(c => c.customerId == accountId);
            if (reviews == null || !reviews.Any())
                return BaseResponse<IEnumerable<ReviewDto>>.NotFoundResponse("No review found for this account");
            var ReviewResponses = _mapper.Map<IEnumerable<ReviewDto>>(reviews);

            return BaseResponse<IEnumerable<ReviewDto>>.OkResponse(ReviewResponses);
        }
        public async Task<BaseResponse<IEnumerable<ReviewDto>>> GetReviewByDetailIdAsync(int detailid)
        {
            var taskClaims = await _unitOfWork.TaskClaims.SearchAsync(c => c.detailId == detailid);
            if (taskClaims == null || !taskClaims.Any())
                return BaseResponse<IEnumerable<ReviewDto>>.NotFoundResponse("No task claims found for this booking detail");
            var TaskClaimResponses = _mapper.Map<IEnumerable<ReviewDto>>(taskClaims);
            return BaseResponse<IEnumerable<ReviewDto>>.OkResponse(TaskClaimResponses);
        }

    }
}
