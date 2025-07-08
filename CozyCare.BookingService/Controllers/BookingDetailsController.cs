using CozyCare.BookingService.Applications.Interfaces;
using CozyCare.BookingService.DTOs.BookingDetails;
using CozyCare.SharedKernel.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CozyCare.BookingService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BookingDetailsController : BaseApiController
    {
        private readonly IBookingDetailService _bookingDetailService;

        public BookingDetailsController(IBookingDetailService bookingDetailService)
        {
            _bookingDetailService = bookingDetailService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBookingDetails() =>
            FromBaseResponse(await _bookingDetailService.GetAllBookingDetailsAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookingDetailById(int id) =>
            FromBaseResponse(await _bookingDetailService.GetBookingDetailByIdAsync(id));

        [HttpPost]
        public async Task<IActionResult> CreateBookingDetail([FromBody] BDetailRequest bookingDetail) =>
            FromBaseResponse(await _bookingDetailService.CreateBookingDetailAsync(bookingDetail));

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBookingDetail(int id, [FromBody] BDetailRequest bookingDetail) =>
            FromBaseResponse(await _bookingDetailService.UpdateBookingDetailAsync(id, bookingDetail));

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookingDetail(int id) =>
            FromBaseResponse(await _bookingDetailService.DeleteBookingDetailAsync(id));

		[HttpGet("available-tasks")]
		public async Task<IActionResult> GetAvailableTasks()
		{
			var resp = await _bookingDetailService.GetAvailableTasksAsync();
			return FromBaseResponse(resp);
		}
	}
}
