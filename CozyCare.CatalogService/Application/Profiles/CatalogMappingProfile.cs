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
                // Map tất cả member nhưng với điều kiện chung cho số = 0 và datetime = default
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) =>
                {
                    if (srcMember == null)
                        return false;

                    // Nếu là decimal hoặc số nguyên, chỉ map khi khác 0
                    if (ProfileHelper.IsNumericType(srcMember.GetType()) && srcMember.Equals(0))
                        return false;

                    var srcType = Nullable.GetUnderlyingType(srcMember.GetType()) ?? srcMember.GetType();
                    if (ProfileHelper.IsNumericType(srcType))
                    {
                        var defaultValue = Activator.CreateInstance(srcType)!; // ví dụ default(decimal) = 0m
                        if (srcMember.Equals(defaultValue))
                            return false;
                    }

                    // Nếu là DateTime, chỉ map khi khác default(DateTime)
                    if (srcMember is DateTime dt && dt == default(DateTime))
                        return false;

                    return true;
                }));
            // Service Mappings
            CreateMap<Service, ServiceDto>().ReverseMap();
            CreateMap<CreateServiceDto, Service>();
            CreateMap<UpdateServiceDto, Service>()
                // Map tất cả member nhưng với điều kiện chung cho số = 0 và datetime = default
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) =>
                {
                    if (srcMember == null)
                        return false;

                    // Nếu là decimal hoặc số nguyên, chỉ map khi khác 0
                    if (ProfileHelper.IsNumericType(srcMember.GetType()) && srcMember.Equals(0))
                        return false;

                    var srcType = Nullable.GetUnderlyingType(srcMember.GetType()) ?? srcMember.GetType();
                    if (ProfileHelper.IsNumericType(srcType))
                    {
                        var defaultValue = Activator.CreateInstance(srcType)!; // ví dụ default(decimal) = 0m
                        if (srcMember.Equals(defaultValue))
                            return false;
                    }

                    // Nếu là DateTime, chỉ map khi khác default(DateTime)
                    if (srcMember is DateTime dt && dt == default(DateTime))
                        return false;

                    return true;
                }));
            // ServiceDetail Mappings
            CreateMap<ServiceDetail, ServiceDetailDto>().ReverseMap();
            CreateMap<CreateServiceDetailDto, ServiceDetail>();
            CreateMap<UpdateServiceDetailDto, ServiceDetail>()
                // Map tất cả member nhưng với điều kiện chung cho số = 0 và datetime = default
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) =>
                {
                    if (srcMember == null)
                        return false;

                    // Nếu là decimal hoặc số nguyên, chỉ map khi khác 0
                    if (ProfileHelper.IsNumericType(srcMember.GetType()) && srcMember.Equals(0))
                        return false;

                    var srcType = Nullable.GetUnderlyingType(srcMember.GetType()) ?? srcMember.GetType();
                    if (ProfileHelper.IsNumericType(srcType))
                    {
                        var defaultValue = Activator.CreateInstance(srcType)!; // ví dụ default(decimal) = 0m
                        if (srcMember.Equals(defaultValue))
                            return false;
                    }

                    // Nếu là DateTime, chỉ map khi khác default(DateTime)
                    if (srcMember is DateTime dt && dt == default(DateTime))
                        return false;

                    return true;
                }));
        }
    }
}
