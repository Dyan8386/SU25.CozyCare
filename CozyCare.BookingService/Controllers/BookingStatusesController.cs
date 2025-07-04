using CozyCare.BookingService.Applications.Interfaces;
using CozyCare.BookingService.DTOs.BookingStatuses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CozyCare.BookingService.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BookingStatusesController : ControllerBase
	{
		private readonly IBookingStatusService _bookingStatusService;

		public BookingStatusesController(IBookingStatusService bookingStatusService)
		{
			_bookingStatusService = bookingStatusService;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllBookingStatuses()
		{
			var response = await _bookingStatusService.GetAllBookingStatusesAsync();
			return Ok(response);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetBookingStatusById(int id)
		{
			var response = await _bookingStatusService.GetBookingStatusByIdAsync(id);
			return Ok(response);
		}

		[HttpPost]
		public async Task<IActionResult> CreateBookingStatus([FromBody] BStatusRequest request)
		{
			var response = await _bookingStatusService.CreateBookingStatusAsync(request);
			return CreatedAtAction(nameof(GetBookingStatusById), new { id = response.Data.statusId }, response);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateBookingStatus(int id, [FromBody] BStatusRequest request)
		{
			var response = await _bookingStatusService.UpdateBookingStatusAsync(id, request);
			return Ok(response);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteBookingStatus(int id)
		{
			var response = await _bookingStatusService.DeleteBookingStatusAsync(id);
			return Ok(response);
		}
	}
}
