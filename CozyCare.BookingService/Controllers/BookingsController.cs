using CozyCare.BookingService.Application.Externals;
using CozyCare.BookingService.Applications.Externals;
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
        private readonly IIdentityApiClient _identityApiClient;
        private readonly IPaymentApiClient _paymentApiClient;

        public BookingsController(IBookingService bookingService, IIdentityApiClient identityApiClient, IPaymentApiClient paymentApiClient)
        {
            _bookingService = bookingService;
            _identityApiClient = identityApiClient;
            _paymentApiClient = paymentApiClient;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBookings() =>
            FromBaseResponse(await _bookingService.GetAllBookingsAsync());

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetBookingById(int id) =>
            FromBaseResponse(await _bookingService.GetBookingByIdAsync(id));

        [HttpPost]
        public async Task<IActionResult> CreateBooking([FromBody] BookingRequest booking) =>
            FromBaseResponse(await _bookingService.CreateBookingAsync(booking));

        [HttpPut("{id}")]
        [AllowAnonymous]
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
        [AllowAnonymous]
        public async Task<IActionResult> GetBookingsByUserId(int id)
        {
            var response = await _bookingService.GetBookingsByAccountIdAsync(id);
            return FromBaseResponse(response);

        }

        [HttpGet("getUser/{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var reponse = await _identityApiClient.GetAccountById(id);
            return FromBaseResponse(reponse);
        }

        [HttpGet("getPayment/{id}")]
        public async Task<IActionResult> GetPaymentById(int id)
        {
            var response = await _paymentApiClient.GetPaymentByIdAsync(id);
            return FromBaseResponse(response);
        }

        [HttpGet("getPromotion/{code}")]
        public async Task<IActionResult> GetPromotionByCode(string code)
        {
            var response = await _paymentApiClient.GetPromotionByCode(code);
            return FromBaseResponse(response);
        }

        [HttpPatch("completed-task/{id}")]
        public async Task<IActionResult> PatchCompletedTask(int id)
        {
            var response = await _bookingService.CompleteTask(id);
            return FromBaseResponse(response);
        }
    }
}
