using CozyCare.PaymentService.Application.Interfaces;
using CozyCare.SharedKernel.Base;
using CozyCare.ViewModels.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace CozyCare.PaymentService.Controllers
{
    [Route("api/[controller]")]
    public class PromotionsController : BaseApiController
    {
        private readonly IPromotionService _promotionService;

        public PromotionsController(IPromotionService promotionService)
        {
            _promotionService = promotionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _promotionService.GetAllAsync();
            return FromBaseResponse(result);
        }

        [HttpGet("{code}")]
        public async Task<IActionResult> GetByCode(string code)
        {
            var result = await _promotionService.GetByCodeAsync(code);
            return FromBaseResponse(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePromotionDto request)
        {
            var result = await _promotionService.CreateAsync(request);
            return FromBaseResponse(result);
        }

        [HttpPut("{code}")]
        public async Task<IActionResult> Update(string code, [FromBody] UpdatePromotionDto request)
        {
            var result = await _promotionService.UpdateAsync(code, request);
            return FromBaseResponse(result);
        }
    }
}
