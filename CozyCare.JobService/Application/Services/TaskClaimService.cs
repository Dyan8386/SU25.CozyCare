using AutoMapper;
using CozyCare.JobService.Application.Interfaces;
using CozyCare.JobService.Domain.Entities;
using CozyCare.JobService.Infrastructure;
using CozyCare.SharedKernel.Base;
using CozyCare.ViewModels.DTOs;
using System.Linq.Expressions;

namespace CozyCare.JobService.Application.Services
{   
    public class TaskClaimService : ITaskClaimService
    {
        private readonly IJobUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

      public TaskClaimService(IJobUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public Task<BaseResponse<TaskClaimStatusDto>> CreateAsync(CreateReviewDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<string>> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<IEnumerable<TaskClaimStatusDto>>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<TaskClaimStatusDto>> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<IEnumerable<TaskClaimStatusDto>>> SearchAsync(Expression<Func<Review, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<string>> SetReviewStatusAsync(int id, LockReviewDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<string>> UpdateAsync(int id, UpdateReviewDto dto)
        {
            throw new NotImplementedException();
        }
        // Add methods for task claim operations here
        // For example: CreateClaimAsync, GetClaimsAsync, UpdateClaimAsync, etc.

    }
}
