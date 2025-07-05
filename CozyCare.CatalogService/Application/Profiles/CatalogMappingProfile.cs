using AutoMapper;
using CozyCare.CatalogService.Domain.Entities;
using CozyCare.SharedKernel.Utils;
using CozyCare.ViewModels.DTOs;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CozyCare.CatalogService.Application.Profiles
{
    public class CatalogMappingProfile : Profile
    {
        public CatalogMappingProfile()
        {
            // Category Mappings
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<CreateCategoryDto, Category>();
            CreateMap<UpdateCategoryDto, Category>()
                .ForAllMembers(opts =>
                    opts.Condition((src, dest, srcMember) =>
                        srcMember != null &&
                        (!ProfileHelper.IsNumericType(srcMember.GetType()) || !srcMember.Equals(0))
                    )
                );
            // Service Mappings
            CreateMap<Service, ServiceDto>().ReverseMap();
            CreateMap<CreateServiceDto, Service>();
            CreateMap<UpdateServiceDto, Service>()
               .ForAllMembers(opts =>
                    opts.Condition((src, dest, srcMember) =>
                        srcMember != null &&
                        (!ProfileHelper.IsNumericType(srcMember.GetType()) || !srcMember.Equals(0))
                    )
                );
            // ServiceDetail Mappings
            CreateMap<ServiceDetail, ServiceDetailDto>().ReverseMap();
            CreateMap<CreateServiceDetailDto, ServiceDetail>();
            CreateMap<UpdateServiceDetailDto, ServiceDetail>()
                .ForAllMembers(opts =>
                    opts.Condition((src, dest, srcMember) =>
                        srcMember != null &&
                        (!ProfileHelper.IsNumericType(srcMember.GetType()) || !srcMember.Equals(0))
                    )
                );
        }
    }
}
