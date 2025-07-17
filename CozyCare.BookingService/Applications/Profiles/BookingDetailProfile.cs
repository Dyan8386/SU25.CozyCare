using AutoMapper;
using CozyCare.BookingService.Domain.Entities;
using CozyCare.BookingService.DTOs.BookingDetails;
using CozyCare.SharedKernel.Utils;
using CozyCare.ViewModels.DTOs;

namespace CozyCare.BookingService.Applications.Profiles
{
	public class BookingDetailProfile : Profile
	{
		public BookingDetailProfile()
		{
			CreateMap<BookingDetail, BDetailResponse>().ReverseMap();
            CreateMap<BookingDetail, BookingDetailDto>().ReverseMap();
            CreateMap<BookingDetail, TaskAvailableResponse>().ReverseMap();
			CreateMap<BDetailRequest, BookingDetail>();
			CreateMap<BDetailRequest, BookingDetail>()
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
