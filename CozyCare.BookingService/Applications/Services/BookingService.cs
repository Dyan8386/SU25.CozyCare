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
	}
}
