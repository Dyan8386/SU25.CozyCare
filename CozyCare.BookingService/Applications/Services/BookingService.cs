using AutoMapper;
using CozyCare.BookingService.Application.Externals;
using CozyCare.BookingService.Applications.Externals;
using CozyCare.BookingService.Applications.Interfaces;
using CozyCare.BookingService.Domain.Entities;
using CozyCare.BookingService.DTOs.Bookings;
using CozyCare.BookingService.Infrastructure;
using CozyCare.SharedKernel.Base;
using CozyCare.SharedKernel.Store;
using CozyCare.ViewModels.DTOs;

namespace CozyCare.BookingService.Applications.Services
{
	public class BookingService : IBookingService
	{
		private readonly IBookingUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		private readonly IIdentityApiClient _identityApiClient;
		private readonly IPaymentApiClient _paymentApiClient;
		private readonly IJobApiClient _jobApiClient;

		public BookingService(IBookingUnitOfWork unitOfWork, IMapper mapper, IIdentityApiClient identityApiClient, IPaymentApiClient paymentApiClient, IJobApiClient jobApiClient)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
			_identityApiClient = identityApiClient;
			_paymentApiClient = paymentApiClient;
			_jobApiClient = jobApiClient;
		}

		public async Task<BaseResponse<string>> CompleteTask(int id)
		{
			// Lấy booking detail 
			var bookingDetails = await _unitOfWork.BookingDetails
				.SearchAsync(bd => bd.bookingId == id);

			//Lấy detailId đầu tiên
			var detailId = bookingDetails.First().detailId;

			//Lấy thông tin của tasks thông qua detailId
			var tasks = await _jobApiClient.GetTaskByBookingDetailsId(detailId);

			var taskId = tasks.Data.FirstOrDefault().claimId;
			//Thay đổi status của task
			await _jobApiClient.ChangeStatusTaskClaim(taskId);

			//Lấy booking
			var booking = await _unitOfWork.Bookings.GetByIdAsync(id);

			if (booking == null)
			{
				return BaseResponse<string>.NotFoundResponse($"Booking with ID {id} not found.");
			}

			//thay đổi bookingStatusId thành trạng thái completed
			const int COMPLETED = 4;
			booking.bookingStatusId = COMPLETED;
			await _unitOfWork.Bookings.UpdateAsync(booking);
			await _unitOfWork.SaveChangesAsync();

			return BaseResponse<string>.OkResponse($"Booking with {id} is updated COMPLETED sucessfully");

		}

		public async Task<BaseResponse<IEnumerable<BookingDto>>> GetAllBookingsAsync()
		{
			var bookings = await _unitOfWork.Bookings.GetAllAsync("BookingDetails");
			var bookingResponses = _mapper.Map<IEnumerable<BookingDto>>(bookings);
			return BaseResponse<IEnumerable<BookingDto>>.OkResponse(bookingResponses);
		}

		public async Task<BaseResponse<BookingResponse>> GetBookingByIdAsync(int id)
		{
			var booking = await _unitOfWork.Bookings.GetByIdAsync(id);
			if (booking == null)
			{
				return BaseResponse<BookingResponse>.NotFoundResponse($"Booking with ID {id} not found.");
			}
			var bookingResponse = _mapper.Map<BookingResponse>(booking);
			return BaseResponse<BookingResponse>.OkResponse(bookingResponse);
		}

		public async Task<BaseResponse<BookingResponse>> CreateBookingAsync(DTOs.Bookings.BookingRequest bookingRequest)
		{
			var customer = await _identityApiClient.GetAccountById(bookingRequest.customerId);
			if (customer.StatusCode != StatusCodeHelper.OK || customer.Data == null)
			{
				return customer.StatusCode == StatusCodeHelper.NotFound
					? BaseResponse<BookingResponse>.NotFoundResponse($"Customer with ID {bookingRequest.customerId} not found.")
					: BaseResponse<BookingResponse>.ErrorResponse(customer.Message);
			}

			var booking = _mapper.Map<Booking>(bookingRequest);
			//tạo booking number dua tren thời gian hien tai
			var timestamp = DateTime.UtcNow.ToString("yyyyMMddHHmmssfff");
			var randomSuffix = new Random().Next(1000, 9999);
			booking.bookingNumber = $"BOOK-{timestamp}-{randomSuffix}";

			await _unitOfWork.Bookings.AddAsync(booking);
			await _unitOfWork.SaveChangesAsync();
			var bookingResponse = _mapper.Map<BookingResponse>(booking);
			return BaseResponse<BookingResponse>.OkResponse(bookingResponse);
		}

		public async Task<BaseResponse<string>> UpdateBookingAsync(int id, DTOs.Bookings.BookingRequest bookingRequest)
		{
			var existingBooking = await _unitOfWork.Bookings.GetByIdAsync(id);
			if (existingBooking == null)
			{
				return BaseResponse<string>.NotFoundResponse($"Booking with ID {id} not found.");
			}
			_mapper.Map(bookingRequest, existingBooking);
			_unitOfWork.Bookings.Update(existingBooking);
			await _unitOfWork.SaveChangesAsync();
			return BaseResponse<string>.OkResponse("Booking updated successfully.");
		}

		public async Task<BaseResponse<string>> DeleteBookingAsync(int id)
		{
			var booking = await _unitOfWork.Bookings.GetByIdAsync(id);
			if (booking == null)
			{
				return BaseResponse<string>.NotFoundResponse($"Booking with ID {id} not found.");
			}
			await _unitOfWork.Bookings.DeleteAsync(booking);
			await _unitOfWork.SaveChangesAsync();
			return BaseResponse<string>.OkResponse("Booking deleted successfully.");
		}

		public async Task<BaseResponse<IEnumerable<BookingResponse>>> GetAvailableTasksAsync()
		{

			const int PENDING = 1;

			// Dùng phương thức SearchAsync của repository
			var availableBookings = await _unitOfWork.Bookings
				.SearchAsync(b => b.bookingStatusId == PENDING);

			// Nếu cần sắp xếp hoặc include thêm, bạn có thể xài overload của SearchAsync

			// 2. Map sang DTO
			var resultDtos = _mapper.Map<IEnumerable<BookingResponse>>(availableBookings);

			// 3. Trả về
			return BaseResponse<IEnumerable<BookingResponse>>.OkResponse(resultDtos);
		}

		public async Task<BaseResponse<IEnumerable<BookingResponse>>> GetBookingsByStatusAsync(int statusId)
		{
			var bookings = await _unitOfWork.Bookings.SearchAsync(b => b.bookingStatusId == statusId);
			var bookingResponses = _mapper.Map<IEnumerable<BookingResponse>>(bookings);
			return BaseResponse<IEnumerable<BookingResponse>>.OkResponse(bookingResponses);
		}

		public async Task<BaseResponse<IEnumerable<BookingResponse>>> GetBookingsByAccountIdAsync(int accountId)
		{
			var bookings = await _unitOfWork.Bookings.SearchAsync(b => b.customerId == accountId);
			var sortedBookings = bookings
				.OrderByDescending(b => b.bookingStatusId == 2)
				.ThenByDescending(b => b.bookingStatusId == 1 && b.paymentStatusId == 2)
				.ThenByDescending(b => b.bookingDate ?? DateTime.MinValue)
				.ToList();

			var bookingResponses = _mapper.Map<IEnumerable<BookingResponse>>(sortedBookings);
			return BaseResponse<IEnumerable<BookingResponse>>.OkResponse(bookingResponses);
		}
	}
}
