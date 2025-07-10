using CozyCare.PaymentService.Application.Interfaces;
using CozyCare.SharedKernel.Base;
using CozyCare.ViewModels.DTOs;
using CozyCare.ViewModels.Momo;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace CozyCare.PaymentService.Controllers
{
    [Route("api/[controller]")]
    public class MomoController : BaseApiController
    {
        private readonly IMomoService _momoService;
        private readonly string _returnUrl;

        public MomoController(IMomoService momoService, IOptions<MomoOptionModel> momoOptions)
        {
            _momoService = momoService;
            _returnUrl = momoOptions.Value.ReturnUrl;
        }


        /// <summary>
        /// Gọi MoMo để tạo payment, trả về URL để người dùng thanh toán
        /// </summary>
        [HttpPost("create")]
        public async Task<IActionResult> CreatePayment([FromBody] CreatePaymentDto request)
        {
            var result = await _momoService.CreatePaymentAsync(request);
            return FromBaseResponse(result);
        }

        /// <summary>
        /// Callback từ MoMo gửi về sau khi người dùng thanh toán
        /// </summary>
        // 1. Endpoint để Momo thông báo kết quả (server‑to‑server)
        [HttpPost("notify")]
        public async Task<IActionResult> Notify([FromBody] MomoExecuteResponseModel model)
        {
            if (model.ResultCode == 0)
                await _momoService.HandleSuccessfulPaymentAsync(model);
            else
                await _momoService.HandleFailedPaymentAsync(model);

            // Momo yêu cầu response JSON { "resultCode": 0, "message": "OK" }
            return Ok(new { resultCode = 0, message = "Received" });
        }

        [HttpGet("return")]
        public IActionResult Return([FromQuery] MomoExecuteResponseModel model)
        {
            // DÙNG ErrorCode cho return URL
            var isSuccess = model.ErrorCode == 0;
            var redirectUrl = isSuccess
                ? $"{_returnUrl}?result=success"
                : $"{_returnUrl}?result=fail&message={Uri.EscapeDataString(model.Message)}";
            return Redirect(redirectUrl);
        }

    }
}
