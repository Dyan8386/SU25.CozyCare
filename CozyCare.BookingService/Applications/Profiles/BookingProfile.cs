using AutoMapper;
using CozyCare.BookingService.Domain.Entities;
using CozyCare.BookingService.DTOs.Bookings;
using CozyCare.SharedKernel.Utils;

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
					opts.Condition((src, dest, srcMember) =>
						srcMember != null &&
						(!ProfileHelper.IsNumericType(srcMember.GetType()) || !srcMember.Equals(0))
					)
				);
		}

		//private static bool IsNumericType(Type type)
		//{
		//	type = Nullable.GetUnderlyingType(type) ?? type;
		//	return type == typeof(byte) || type == typeof(sbyte)
		//		|| type == typeof(short) || type == typeof(ushort)
		//		|| type == typeof(int) || type == typeof(uint)
		//		|| type == typeof(long) || type == typeof(ulong)
		//		|| type == typeof(float) || type == typeof(double)
		//		|| type == typeof(decimal);
		//}

	}
}