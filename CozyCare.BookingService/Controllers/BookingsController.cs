using CozyCare.BookingService.Applications.Interfaces;
using CozyCare.BookingService.DTOs.Bookings;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CozyCare.BookingService.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BookingsController : ControllerBase
	{
		private readonly IBookingService _bookingService;

		public BookingsController(IBookingService bookingService)
		{
			_bookingService = bookingService;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllBookings()
		{
			var response = await _bookingService.GetAllBookingsAsync();
			return Ok(response);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetBookingById(int id)
		{
			var response = await _bookingService.GetBookingByIdAsync(id);

			return Ok(response);
		}

		[HttpPost]
		public async Task<IActionResult> CreateBooking([FromBody] BookingRequest booking)
		{
			var response = await _bookingService.CreateBookingAsync(booking);
			return CreatedAtAction(nameof(GetBookingById), new { id = response.Data.bookingId }, response);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateBooking(int id, [FromBody] BookingRequest booking)
		{
			var response = await _bookingService.UpdateBookingAsync(id, booking);

			return Ok(response);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteBooking(int id)
		{
			var response = await _bookingService.DeleteBookingAsync(id);
			return Ok(response);
		}
	}
}
