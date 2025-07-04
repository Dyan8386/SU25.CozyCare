using AutoMapper;
using CozyCare.BookingService.Domain.Entities;
using CozyCare.BookingService.DTOs.BookingDetails;

namespace CozyCare.BookingService.Applications.Profiles
{
	public class BookingDetailProfile : Profile
	{
		public BookingDetailProfile()
		{
			CreateMap<BookingDetail, BDetailResponse>().ReverseMap();
			CreateMap<BDetailRequest, BookingDetail>();
			CreateMap<BDetailRequest, BookingDetail>()
				.ForAllMembers(opts =>
					opts.Condition((src, dest, srcMember) => srcMember != null)
				);
		}
	}
}
