using AutoMapper;
using CozyCare.BookingService.Domain.Entities;
using CozyCare.BookingService.DTOs.Bookings;
using CozyCare.SharedKernel.Utils;
using CozyCare.ViewModels.DTOs;

namespace CozyCare.BookingService.Applications.Profiles
{
	public class BookingProfile : Profile
	{
		public BookingProfile()
		{
            CreateMap<Booking, BookingDto>().ReverseMap();
            CreateMap<Booking, BookingResponse>().ReverseMap();
			CreateMap<DTOs.Bookings.BookingRequest, Booking>();
			CreateMap<DTOs.Bookings.BookingRequest, Booking>()
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