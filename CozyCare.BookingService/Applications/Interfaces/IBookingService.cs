﻿using CozyCare.BookingService.Domain.Entities;
using CozyCare.BookingService.DTOs.Bookings;
using CozyCare.SharedKernel.Base;
using CozyCare.ViewModels.DTOs;

namespace CozyCare.BookingService.Applications.Interfaces
{
	public interface IBookingService
	{
		//CRUD
		Task<BaseResponse<IEnumerable<BookingDto>>> GetAllBookingsAsync();
		Task<BaseResponse<BookingResponse>> GetBookingByIdAsync(int id);
		Task<BaseResponse<BookingResponse>> CreateBookingAsync(DTOs.Bookings.BookingRequest booking);
		Task<BaseResponse<string>> UpdateBookingAsync(int id, DTOs.Bookings.BookingRequest booking);
		Task<BaseResponse<string>> DeleteBookingAsync(int id);
		Task<BaseResponse<IEnumerable<BookingResponse>>> GetAvailableTasksAsync();
		Task<BaseResponse<IEnumerable<BookingResponse>>> GetBookingsByAccountIdAsync(int accountId);
		Task<BaseResponse<IEnumerable<BookingResponse>>> GetBookingsByStatusAsync(int statusId);
		Task<BaseResponse<string>> CompleteTask(int id);

	}
}
