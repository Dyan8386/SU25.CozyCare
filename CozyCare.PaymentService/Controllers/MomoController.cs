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
    [Authorize]
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
        [HttpGet("callback")]
        public async Task<IActionResult> Callback()
        {
            var model = _momoService.ParseCallback(Request.Query);

            if (model.ResultCode == 0)
            {
                await _momoService.HandleSuccessfulPaymentAsync(model);
                return Redirect($"{_returnUrl}?result=success");
            }
            else
            {
                await _momoService.HandleFailedPaymentAsync(model);
                return Redirect($"{_returnUrl}?result=fail&message={Uri.EscapeDataString(model.Message)}");
            }
        }
    }
}
