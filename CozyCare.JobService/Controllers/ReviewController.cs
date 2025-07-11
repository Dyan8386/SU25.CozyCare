using CozyCare.JobService.Infrastructure;
using CozyCare.ViewModels.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using CozyCare.SharedKernel.Base;
using CozyCare.JobService.Application.Services;
using CozyCare.JobService.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Azure;

namespace CozyCare.JobService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
  //  [Authorize]
    public class ReviewController : BaseApiController
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewSService)
        {
            _reviewService = reviewSService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            FromBaseResponse(await _reviewService.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) =>
            FromBaseResponse(await _reviewService.GetByIdAsync(id));

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateReviewDto dto) =>
            FromBaseResponse(await _reviewService.CreateAsync(dto));

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateReviewDto dto) =>
            FromBaseResponse(await _reviewService.UpdateAsync(id, dto));

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) =>
            FromBaseResponse(await _reviewService.DeleteAsync(id));

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string? keyword)
        {
            Expression<Func<Domain.Entities.Review, bool>> filter = c =>
                string.IsNullOrEmpty(keyword) || c.reviewTarget.Contains(keyword);
            return FromBaseResponse(await _reviewService.SearchAsync(filter));
        }
        [HttpGet("users/{accountId}")]
        public async Task<IActionResult> GetByAccountId(int accountId)
        {
            var result = await _reviewService.GeReviewByAccountIdAsync(accountId);
            return FromBaseResponse(result);
        }
        [HttpGet("bookingdetails/{Id}")]
        public async Task<IActionResult> GetByBookingDetailsId(int Id)
        {
            var result = await _reviewService.GetReviewByDetailIdAsync(Id);
            return FromBaseResponse(result);
        }
    }
}
