using CozyCare.BookingService.Applications.Interfaces;
using CozyCare.BookingService.DTOs.Bookings;
using CozyCare.SharedKernel.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CozyCare.BookingService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BookingsController : BaseApiController
    {
        private readonly IBookingService _bookingService;

        public BookingsController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBookings() =>
            FromBaseResponse(await _bookingService.GetAllBookingsAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookingById(int id) =>
            FromBaseResponse(await _bookingService.GetBookingByIdAsync(id));

        [HttpPost]
        public async Task<IActionResult> CreateBooking([FromBody] BookingRequest booking) =>
            FromBaseResponse(await _bookingService.CreateBookingAsync(booking));

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBooking(int id, [FromBody] BookingRequest booking) =>
            FromBaseResponse(await _bookingService.UpdateBookingAsync(id, booking));

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooking(int id) =>
            FromBaseResponse(await _bookingService.DeleteBookingAsync(id));

        //[HttpGet("available-tasks")]
        //public async Task<IActionResult> GetAvailableTasks()
        //{
        //	var resp = await _bookingService.GetAvailableTasksAsync();
        //	return FromBaseResponse(resp);
        //}

        [HttpGet("users/{id}")]
        public async Task<IActionResult> GetBookingsByUserId(int id)
        {
            var response = await _bookingService.GetBookingsByAccountIdAsync(id);
            return FromBaseResponse(response);

        }
    }
}
