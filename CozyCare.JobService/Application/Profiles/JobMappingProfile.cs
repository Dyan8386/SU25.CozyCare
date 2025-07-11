using AutoMapper;
using CozyCare.JobService.Domain.Entities;
using CozyCare.SharedKernel.Utils;
using CozyCare.ViewModels.DTOs;

namespace CozyCare.JobService.Application.Profiles
{
    public class JobMappingProfile :Profile
    {
        public JobMappingProfile()
        {
            // Job Mappings
            CreateMap<Review, ReviewDto>().ReverseMap();
            CreateMap<Review, CreateReviewDto>();
            CreateMap<Review, UpdateReviewDto>()
                .ForAllMembers(opts =>
                    opts.Condition((src, dest, srcMember) =>
                        srcMember != null &&
                        (!ProfileHelper.IsNumericType(srcMember.GetType()) || !srcMember.Equals(0))
                    )
                );

            CreateMap<TaskClaim, TaskClaimDto>().ReverseMap();
            CreateMap<TaskClaim, CreateTaskClaimDto>();
            CreateMap<TaskClaim, UpdateTaskClaimDto>()
                .ForAllMembers(opts =>
                    opts.Condition((src, dest, srcMember) =>
                        srcMember != null &&
                        (!ProfileHelper.IsNumericType(srcMember.GetType()) || !srcMember.Equals(0))
                    )
                );

            CreateMap<TaskClaimStatus, TaskClaimStatusDto>().ReverseMap();
            CreateMap<TaskClaimStatus, CreateAndUpdateTaskClaimStatusDto>();
           

        }
    }
}
