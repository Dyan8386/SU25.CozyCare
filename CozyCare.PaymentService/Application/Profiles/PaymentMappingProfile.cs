using AutoMapper;
using CozyCare.PaymentService.Domain.Entities;
using CozyCare.SharedKernel.Utils;
using CozyCare.ViewModels.DTOs;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CozyCare.PaymentService.Application.Profiles
{
    public class PaymentMappingProfile : Profile
    {
        public PaymentMappingProfile()
        {
            // Payment
            CreateMap<Payment, PaymentDto>().ReverseMap();
            CreateMap<CreatePaymentDto, Payment>();

                     // Promotion
            CreateMap<Promotion, PromotionDto>().ReverseMap();
            CreateMap<CreatePromotionDto, Promotion>();

            CreateMap<UpdatePromotionDto, Promotion>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) =>
                {
                    if (srcMember == null)
                        return false;

                    if (ProfileHelper.IsNumericType(srcMember.GetType()) && srcMember.Equals(0))
                        return false;

                    var srcType = Nullable.GetUnderlyingType(srcMember.GetType()) ?? srcMember.GetType();
                    if (ProfileHelper.IsNumericType(srcType))
                    {
                        var defaultValue = Activator.CreateInstance(srcType)!;
                        if (srcMember.Equals(defaultValue))
                            return false;
                    }

                    if (srcMember is DateTime dt && dt == default(DateTime))
                        return false;

                    return true;
                }));
        }
    }
}
