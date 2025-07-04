using CozyCare.BookingService.Applications.Interfaces;
using CozyCare.BookingService.DTOs.BookingDetails;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CozyCare.BookingService.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BookingDetailsController : ControllerBase
	{
		private readonly IBookingDetailService _bookingDetailService;

		public BookingDetailsController(IBookingDetailService bookingDetailService)
		{
			_bookingDetailService = bookingDetailService;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllBookingDetails()
		{
			var response = await _bookingDetailService.GetAllBookingDetailsAsync();
			return Ok(response);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetBookingDetailById(int id)
		{
			var response = await _bookingDetailService.GetBookingDetailByIdAsync(id);
			return Ok(response);
		}

		[HttpPost]
		public async Task<IActionResult> CreateBookingDetail([FromBody] BDetailRequest bookingDetail)
		{
			var response = await _bookingDetailService.CreateBookingDetailAsync(bookingDetail);
			return CreatedAtAction(nameof(GetBookingDetailById), new { id = response.Data.detailId }, response);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateBookingDetail(int id, [FromBody] BDetailRequest bookingDetail)
		{
			var response = await _bookingDetailService.UpdateBookingDetailAsync(id, bookingDetail);
			return Ok(response);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteBookingDetail(int id)
		{
			var response = await _bookingDetailService.DeleteBookingDetailAsync(id);
			return Ok(response);
		}
	}
}
