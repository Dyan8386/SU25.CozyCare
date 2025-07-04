using CozyCare.BookingService.DTOs.BookingStatuses;
using CozyCare.SharedKernel.Base;

namespace CozyCare.BookingService.Applications.Interfaces
{
	public interface IBookingStatusService
	{
		// CRUD
		Task<BaseResponse<IEnumerable<BStatusResponse>>> GetAllBookingStatusesAsync();
		Task<BaseResponse<BStatusResponse>> GetBookingStatusByIdAsync(int id);
		Task<BaseResponse<BStatusResponse>> CreateBookingStatusAsync(BStatusRequest request);
		Task<BaseResponse<string>> UpdateBookingStatusAsync(int id, BStatusRequest request);
		Task<BaseResponse<string>> DeleteBookingStatusAsync(int id);
	}
}
