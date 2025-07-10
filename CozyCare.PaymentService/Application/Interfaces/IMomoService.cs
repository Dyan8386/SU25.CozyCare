using CozyCare.SharedKernel.Base;
using CozyCare.ViewModels.DTOs;
using CozyCare.ViewModels.Momo;
using CozyyCare.ViewModels.Momo;

namespace CozyCare.PaymentService.Application.Interfaces
{
    public interface IMomoService
    {
        /// <summary>
        /// Gọi MoMo để tạo payment, trả về URL người dùng chuyển hướng để thanh toán.
        /// </summary>
        Task<BaseResponse<string>> CreatePaymentAsync(CreatePaymentDto request);

        /// <summary>
        /// Xác thực callback từ MoMo (signature, resultCode), và trả về dữ liệu MoMoExecuteResponseModel.
        /// </summary>
        MomoExecuteResponseModel ParseCallback(IQueryCollection query);

        /// <summary>
        /// Xử lý callback thành công (cập nhật trạng thái, ghi log…)
        /// </summary>
        Task HandleSuccessfulPaymentAsync(MomoExecuteResponseModel response);

        /// <summary>
        /// Xử lý callback thất bại (ghi log, cập nhật trạng thái “Failed”)
        /// </summary>
        Task HandleFailedPaymentAsync(MomoExecuteResponseModel response);
    }
}
