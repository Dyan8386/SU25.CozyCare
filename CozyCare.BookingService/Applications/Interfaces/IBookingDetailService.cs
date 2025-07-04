using CozyCare.BookingService.DTOs.BookingDetails;
using CozyCare.SharedKernel.Base;

namespace CozyCare.BookingService.Applications.Interfaces
{
	public interface IBookingDetailService
	{
		//CRUD
		Task<BaseResponse<IEnumerable<BDetailResponse>>> GetAllBookingDetailsAsync();
		Task<BaseResponse<BDetailResponse>> GetBookingDetailByIdAsync(int id);
		Task<BaseResponse<BDetailResponse>> CreateBookingDetailAsync(BDetailRequest request);
		Task<BaseResponse<string>> UpdateBookingDetailAsync(int id, BDetailRequest request);
		Task<BaseResponse<string>> DeleteBookingDetailAsync(int id);
	}
}
