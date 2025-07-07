using CozyCare.BookingService.Applications.Interfaces;
using CozyCare.BookingService.DTOs.BookingStatuses;
using CozyCare.SharedKernel.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CozyCare.BookingService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingStatusesController : BaseApiController
    {
        private readonly IBookingStatusService _bookingStatusService;

        public BookingStatusesController(IBookingStatusService bookingStatusService)
        {
            _bookingStatusService = bookingStatusService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBookingStatuses() =>
            FromBaseResponse(await _bookingStatusService.GetAllBookingStatusesAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookingStatusById(int id) =>
            FromBaseResponse(await _bookingStatusService.GetBookingStatusByIdAsync(id));

        [HttpPost]
        public async Task<IActionResult> CreateBookingStatus([FromBody] BStatusRequest request) =>
            FromBaseResponse(await _bookingStatusService.CreateBookingStatusAsync(request));

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBookingStatus(int id, [FromBody] BStatusRequest request) =>
            FromBaseResponse(await _bookingStatusService.UpdateBookingStatusAsync(id, request));

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookingStatus(int id) =>
            FromBaseResponse(await _bookingStatusService.DeleteBookingStatusAsync(id));
    }
}
