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
    public class ReviewService : IReviewService
    {
        private readonly IJobUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ReviewService(IJobUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<ReviewDto>> CreateAsync(CreateReviewDto dto)
        {
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
    }
}
