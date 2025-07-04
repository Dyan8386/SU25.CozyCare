using AutoMapper;
using CozyCare.BookingService.Domain.Entities;
using CozyCare.BookingService.DTOs.BookingStatuses;

namespace CozyCare.BookingService.Applications.Profiles
{
	public class BookingStatusProfile : Profile
	{
		public BookingStatusProfile()
		{
			CreateMap<BookingStatus, BStatusResponse>().ReverseMap();
			CreateMap<BStatusRequest, BookingStatus>();
			CreateMap<BStatusRequest, BookingStatus>()
				// Chỉ apply mapping khi srcMember != null (với kiểu nullable)
				.ForAllMembers(opts =>
					opts.Condition((src, dest, srcMember) => srcMember != null)
				);
		}
	}
}
