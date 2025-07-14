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
        //[HttpPost("notify")]
        //public async Task<IActionResult> Notify([FromBody] MomoExecuteResponseModel model)
        //{
        //    Console.WriteLine("🔥 Received MoMo Notify callback:");
        //    Console.WriteLine($"ResultCode: {model.ResultCode}, OrderId: {model.OrderId}, Amount: {model.Amount}");

        //    if (model.ResultCode == 0)
        //    {
        //        Console.WriteLine("✅ MoMo payment success callback.");
        //        await _momoService.HandleSuccessfulPaymentAsync(model);
        //    }
        //    else
        //    {
        //        Console.WriteLine("❌ MoMo payment failed callback.");
        //        await _momoService.HandleFailedPaymentAsync(model);
        //    }

        //    return Ok(new { resultCode = 0, message = "Received" });
        //}


        [HttpGet("return")]
        public async Task<IActionResult> Return([FromQuery] MomoExecuteResponseModel model)
        {
            // Log đầu vào
            Console.WriteLine("🔥 Received MoMo Return callback:");
            Console.WriteLine($"ErrorCode: {model.ErrorCode}, OrderId: {model.OrderId}, Amount: {model.Amount}");

            // Xử lý thanh toán giống như notify
            if (model.ErrorCode == "0")
            {
                Console.WriteLine("✅ MoMo payment success via return.");
                await _momoService.HandleSuccessfulPaymentAsync(model);
            }
            else
            {
                Console.WriteLine("❌ MoMo payment failed via return.");
                await _momoService.HandleFailedPaymentAsync(model);
            }

            // Redirect về frontend
            var clientUrl = $"http://localhost:3000/payment/callback?orderId={model.OrderId}";
            if (model.ErrorCode != "0")
            {
                var escapedMessage = Uri.EscapeDataString(model.Message ?? "Lỗi không xác định");
                clientUrl += $"&error=1&message={escapedMessage}";
            }

            return Redirect(clientUrl); // ✅ Đây là redirect về frontend UI
        }


        /// <summary>
        /// Cho client fetch kết quả thanh toán sau khi redirect về frontend
        /// </summary>
        [HttpGet("callback/{orderId}")]
        public async Task<IActionResult> GetCallbackData(string orderId) 
        {
            var model = await _momoService.GetCallbackDataAsync(orderId);
            if (model == null)
                return NotFound(new { message = $"Không tìm thấy orderId {orderId}" });

            // Trả thẳng JSON MomoExecuteResponseModel
            return Ok(model);
        }

    }
}
