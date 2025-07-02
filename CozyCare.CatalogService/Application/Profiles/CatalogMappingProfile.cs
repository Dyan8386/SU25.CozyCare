using AutoMapper;
using CozyCare.CatalogService.Domain.Entities;
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
            CreateMap<UpdateCategoryDto, Category>();

            // Service Mappings
            CreateMap<Service, ServiceDto>().ReverseMap();
            CreateMap<CreateServiceDto, Service>();
            CreateMap<UpdateServiceDto, Service>();

            // ServiceDetail Mappings
            CreateMap<ServiceDetail, ServiceDetailDto>().ReverseMap();
            CreateMap<CreateServiceDetailDto, ServiceDetail>();
            CreateMap<UpdateServiceDetailDto, ServiceDetail>();
        }
    }
}
