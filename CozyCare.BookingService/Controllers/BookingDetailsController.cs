﻿using CozyCare.BookingService.Applications.Externals;
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
        private readonly IJobApiClient _jobApiClient;

        public BookingDetailsController(IBookingDetailService bookingDetailService, IJobApiClient jobApiClient)
        {
            _bookingDetailService = bookingDetailService;
            _jobApiClient = jobApiClient;
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

        //getbybookingid/{bookingId}
        [HttpGet("booking/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetBookingDetailsByBookingId(int id)
        {
            var response = await _bookingDetailService.GetBookingDetailsByBookingIdAsync(id);
            return FromBaseResponse(response);
        }

		[HttpGet("getTasksbyDetail/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetTasksByDetailId(int id)
        {
            var response = await _jobApiClient.GetTaskByBookingDetailsId(id);
            return FromBaseResponse(response);
        }
	}
}
