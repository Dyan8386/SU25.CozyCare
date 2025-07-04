using AutoMapper;
using CozyCare.BookingService.Domain.Entities;
using CozyCare.BookingService.DTOs.Bookings;

namespace CozyCare.BookingService.Applications.Profiles
{
	public class BookingProfile : Profile
	{
		public BookingProfile()
		{
			CreateMap<Booking, BookingResponse>().ReverseMap();
			CreateMap<BookingRequest, Booking>();
			CreateMap<BookingRequest, Booking>()
			// Chỉ apply mapping khi srcMember != null (với kiểu nullable)
				.ForAllMembers(opts =>
					opts.Condition((src, dest, srcMember) => srcMember != null)
				);
		}
	}
}
