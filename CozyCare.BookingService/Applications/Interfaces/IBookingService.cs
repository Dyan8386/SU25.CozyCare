using CozyCare.BookingService.Domain.Entities;
using CozyCare.BookingService.DTOs.Bookings;
using CozyCare.SharedKernel.Base;

namespace CozyCare.BookingService.Applications.Interfaces
{
	public interface IBookingService
	{
		//CRUD
		Task<BaseResponse<IEnumerable<BookingResponse>>> GetAllBookingsAsync();
		Task<BaseResponse<BookingResponse>> GetBookingByIdAsync(int id);
		Task<BaseResponse<BookingResponse>> CreateBookingAsync(BookingRequest booking);
		Task<BaseResponse<string>> UpdateBookingAsync(int id, BookingRequest booking);
		Task<BaseResponse<string>> DeleteBookingAsync(int id);
		Task<BaseResponse<IEnumerable<BookingResponse>>> GetAvailableTasksAsync();

	}
}
