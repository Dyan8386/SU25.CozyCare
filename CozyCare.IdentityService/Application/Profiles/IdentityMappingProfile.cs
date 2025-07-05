using AutoMapper;
using CozyCare.IdentityService.Domain.Entities;
using CozyCare.SharedKernel.Utils;
using CozyCare.ViewModels.DTOs;

namespace CozyCare.IdentityService.Application.Profiles
{
    public class IdentityMappingProfile : Profile
    {
        public IdentityMappingProfile()
        {
            // Entity to DTO
            CreateMap<Account, AccountDto>();

            // DTO to Entity
            CreateMap<CreateAccountRequestDto, Account>();
            CreateMap<UpdateAccountRequestDto, Account>()
                .ForAllMembers(opts =>
                    opts.Condition((src, dest, srcMember) =>
                        srcMember != null &&
                        (!ProfileHelper.IsNumericType(srcMember.GetType()) || !srcMember.Equals(0))
                    )
                );

            // Authentication mapping
            CreateMap<LoginRequestDto, Account>();

            // Nested mapping for login response
            CreateMap<Account, LoginResponseDto>()
                .ForAllMembers(opts =>
                    opts.Condition((src, dest, srcMember) =>
                        srcMember != null &&
                        (!ProfileHelper.IsNumericType(srcMember.GetType()) || !srcMember.Equals(0))
                    )
                );
        }
    }
}
