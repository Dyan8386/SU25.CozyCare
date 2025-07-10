using AutoMapper;
using CozyCare.BookingService.Applications.Interfaces;
using CozyCare.BookingService.Domain.Entities;
using CozyCare.BookingService.DTOs.Bookings;
using CozyCare.BookingService.Infrastructure;
using CozyCare.SharedKernel.Base;

namespace CozyCare.BookingService.Applications.Services
{
	public class BookingService : IBookingService
	{
		private readonly IBookingUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public BookingService(IBookingUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<BaseResponse<IEnumerable<BookingResponse>>> GetAllBookingsAsync()
		{
			var bookings = await _unitOfWork.Bookings.GetAllAsync();
			var bookingResponses = _mapper.Map<IEnumerable<BookingResponse>>(bookings);
			return BaseResponse<IEnumerable<BookingResponse>>.OkResponse(bookingResponses);
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

		public async Task<BaseResponse<BookingResponse>> CreateBookingAsync(BookingRequest bookingRequest)
		{
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

		public async Task<BaseResponse<string>> UpdateBookingAsync(int id, BookingRequest bookingRequest)
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
			var bookingResponses = _mapper.Map<IEnumerable<BookingResponse>>(bookings);
			return BaseResponse<IEnumerable<BookingResponse>>.OkResponse(bookingResponses);
		}
	}
}
