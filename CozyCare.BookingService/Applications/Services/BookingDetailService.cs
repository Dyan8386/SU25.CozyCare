using AutoMapper;
using CozyCare.BookingService.Applications.Externals;
using CozyCare.BookingService.Applications.Interfaces;
using CozyCare.BookingService.Domain.Entities;
using CozyCare.BookingService.DTOs.BookingDetails;
using CozyCare.BookingService.Infrastructure;
using CozyCare.SharedKernel.Base;
using CozyCare.SharedKernel.Store;

namespace CozyCare.BookingService.Applications.Services
{
	public class BookingDetailService : IBookingDetailService
	{
		private readonly IBookingUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		private readonly ICatalogApiClient _catalogApiClient;

		public BookingDetailService(IBookingUnitOfWork unitOfWork, IMapper mapper, ICatalogApiClient catalogApiClient)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
			_catalogApiClient = catalogApiClient;
		}

		public async Task<BaseResponse<IEnumerable<BDetailResponse>>> GetAllBookingDetailsAsync()
		{
			var bookingDetails = await _unitOfWork.BookingDetails.GetAllAsync();
			var response = _mapper.Map<IEnumerable<BDetailResponse>>(bookingDetails);
			return BaseResponse<IEnumerable<BDetailResponse>>.OkResponse(response);
		}

		public async Task<BaseResponse<BDetailResponse>> GetBookingDetailByIdAsync(int id)
		{
			var bookingDetail = await _unitOfWork.BookingDetails.GetByIdAsync(id);
			if (bookingDetail == null)
			{
				return BaseResponse<BDetailResponse>.NotFoundResponse($"Booking detail with ID {id} not found.");
			}
			var response = _mapper.Map<BDetailResponse>(bookingDetail);
			return BaseResponse<BDetailResponse>.OkResponse(response);
		}

		public async Task<BaseResponse<BDetailResponse>> CreateBookingDetailAsync(BDetailRequest request)
		{
			var service = await _catalogApiClient.GetServiceByIdAsync(request.serviceId);
			if (service.StatusCode != StatusCodeHelper.OK || service.Data == null)
			{
				return service.StatusCode == StatusCodeHelper.NotFound
					? BaseResponse<BDetailResponse>.NotFoundResponse($"Service with ID {request.serviceId} not found.")
					: BaseResponse<BDetailResponse>.ErrorResponse(service.Message);
			}

			var bookingDetail = _mapper.Map<BookingDetail>(request);
			
			await _unitOfWork.BookingDetails.AddAsync(bookingDetail);
			await _unitOfWork.SaveChangesAsync();
			var response = _mapper.Map<BDetailResponse>(bookingDetail);
			return BaseResponse<BDetailResponse>.OkResponse(response);
		}

		public async Task<BaseResponse<string>> UpdateBookingDetailAsync(int id, BDetailRequest request)
		{
			var service = await _catalogApiClient.GetServiceByIdAsync(request.serviceId);
			if (service.StatusCode != StatusCodeHelper.OK || service.Data == null)
			{
				return BaseResponse<string>.NotFoundResponse($"Service with ID {request.serviceId} not found.");
			}
			var bookingDetail = await _unitOfWork.BookingDetails.GetByIdAsync(id);
			if (bookingDetail == null)
			{
				return BaseResponse<string>.NotFoundResponse($"Booking detail with ID {id} not found.");
			}
			_mapper.Map(request, bookingDetail);
			_unitOfWork.BookingDetails.Update(bookingDetail);
			await _unitOfWork.SaveChangesAsync();
			return BaseResponse<string>.OkResponse("Booking detail updated successfully.");
		}

		public async Task<BaseResponse<string>> DeleteBookingDetailAsync(int id)
		{
			var bookingDetail = await _unitOfWork.BookingDetails.GetByIdAsync(id);
			if (bookingDetail == null)
			{
				return BaseResponse<string>.NotFoundResponse($"Booking detail with ID {id} not found.");
			}
			_unitOfWork.BookingDetails.Delete(bookingDetail);
			await _unitOfWork.SaveChangesAsync();
			return BaseResponse<string>.OkResponse("Booking detail deleted successfully.");
		}

		public async Task<BaseResponse<IEnumerable<TaskAvailableResponse>>> GetAvailableTasksAsync()
		{
			const int PENDING = 1;  // trạng thái cần lọc
			const int PAID = 2; // trạng thái thanh toán cần lọc

			// Lọc bookingDetail sao cho booking.bookingStatusId == AVAILABLE_STATUS
			// Include luôn navigation property "booking" để lấy BookingNumber và BookingStatusId
			var details = await _unitOfWork.BookingDetails
				.SearchAsync(
					filter: bd => bd.booking.bookingStatusId == PENDING && bd.booking.paymentStatusId == PAID, // Fixed comparison operator
					includeProperties: "booking"
				);

			// Map sang DTO
			var dtos = details.Select(bd =>
			{
				var dto = _mapper.Map<TaskAvailableResponse>(bd);
				// Nếu AutoMapper không cấu hình map navigation,
				// bạn có thể gán thủ công:
				dto.BookingNumber = bd.booking.bookingNumber;
				dto.BookingStatusId = bd.booking.bookingStatusId;
				return dto;
			});

			return BaseResponse<IEnumerable<TaskAvailableResponse>>.OkResponse(dtos);
		}

		public async Task<BaseResponse<IEnumerable<BDetailResponse>>> GetBookingDetailsByBookingIdAsync(int bookingId)
		{
			var bookingDetails = await _unitOfWork.BookingDetails
				.SearchAsync(bd => bd.bookingId == bookingId);
			if (bookingDetails == null || !bookingDetails.Any())
			{
				return BaseResponse<IEnumerable<BDetailResponse>>.NotFoundResponse($"No booking details found for Booking ID {bookingId}.");
			}
			var response = _mapper.Map<IEnumerable<BDetailResponse>>(bookingDetails);
			return BaseResponse<IEnumerable<BDetailResponse>>.OkResponse(response);
		}
	}
}
