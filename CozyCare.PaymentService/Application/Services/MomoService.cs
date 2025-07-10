using CozyCare.PaymentService.Application.Externals;
using CozyCare.PaymentService.Application.Interfaces;
using CozyCare.PaymentService.Domain.Entities;
using CozyCare.PaymentService.Infrastructure;
using CozyCare.Persistence;
using CozyCare.SharedKernel.Base;
using CozyCare.SharedKernel.Store;
using CozyCare.ViewModels.DTOs;
using CozyCare.ViewModels.Enums;
using CozyCare.ViewModels.Momo;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CozyCare.PaymentService.Application.Services
{
    public class MomoService : IMomoService
    {
        private readonly MomoOptionModel _opts;
        private readonly HttpClient _http;
        private readonly ITokenAccessor _tokenAccessor;
        private readonly IPaymentUnitOfWork _uow;
        private readonly IBookingApiClient _bookingApiClient;

        public MomoService(
            IOptions<MomoOptionModel> opts,
            HttpClient httpClient,
            ITokenAccessor tokenAccessor,
            IPaymentUnitOfWork uow,
            IBookingApiClient bookingApiClient) // 👈 thêm dòng này
        {
            _opts = opts.Value;
            _http = httpClient;
            _tokenAccessor = tokenAccessor;
            _uow = uow;
            _bookingApiClient = bookingApiClient; // 👈 gán vào field
        }

        public async Task<BaseResponse<string>> CreatePaymentAsync(CreatePaymentDto req)
        {
            // 1. Kiểm tra đơn booking có tồn tại
            var bookingRes = await _bookingApiClient.GetBookingByIdAsync(req.BookingId);
            if (bookingRes.StatusCode != StatusCodeHelper.OK || bookingRes.Data == null)
                return BaseResponse<string>.NotFoundResponse($"Booking #{req.BookingId} not found");

            var orderId = Guid.NewGuid().ToString();
            var requestId = Guid.NewGuid().ToString();
            var amount = ((int)req.Amount).ToString("F0");
            var extraData = "";

            var raw =
                $"partnerCode={_opts.PartnerCode}&accessKey={_opts.AccessKey}" +
                $"&requestId={requestId}&amount={amount}" +
                $"&orderId={orderId}&orderInfo={req.Notes}" +
                $"&returnUrl={_opts.ReturnUrl}&notifyUrl={_opts.NotifyUrl}" +
                $"&extraData={extraData}";


            var signature = ComputeHmacSHA256(raw, _opts.SecretKey);

            var payload = new
            {
                partnerCode = _opts.PartnerCode,
                accessKey = _opts.AccessKey,
                requestId,
                amount,
                orderId,
                orderInfo = req.Notes,
                returnUrl = _opts.ReturnUrl,
                notifyUrl = _opts.NotifyUrl,
                extraData,
                requestType = _opts.RequestType,
                signature
            };

            var json = JsonSerializer.Serialize(payload);
            var body = new StringContent(json, Encoding.UTF8, "application/json");
            var request = new HttpRequestMessage(HttpMethod.Post, new Uri(_opts.MomoApiUrl))
            {
                Content = body
            };

            var resp = await _http.SendAsync(request);

            resp.EnsureSuccessStatusCode();

            var respJson = await resp.Content.ReadAsStringAsync();
            Console.WriteLine("Momo raw JSON response: " + respJson);

            var model = JsonSerializer.Deserialize<MomoCreatePaymentResponseModel>(
                respJson,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
            )!;

            // 2. Tạo Payment record
            var payment = new Payment
            {
                userId = req.UserId,
                bookingId = req.BookingId,
                amount = req.Amount,
                paymentMethod = "Momo",
                statusId = (int)PaymentStatusEnum.Unpaid,
                notes = req.Notes,
                paymentDate = req.PaymentDate ?? DateTime.UtcNow,
                createdDate = DateTime.UtcNow,
                momoOrderId = orderId
            };

            await _uow.Payments.AddAsync(payment);
            await _uow.SaveChangesAsync();

            return BaseResponse<string>.OkResponse(data: model.PayUrl);
        }


        public MomoExecuteResponseModel ParseCallback(IQueryCollection q)
        {
            string G(string k) => q.ContainsKey(k) ? q[k].ToString() : "";

            var model = new MomoExecuteResponseModel
            {
                PartnerCode = G("partnerCode"),
                OrderId = G("orderId"),
                RequestId = G("requestId"),
                Amount = G("amount"),
                OrderInfo = G("orderInfo"),
                OrderType = G("orderType"),
                TransId = G("transId"),
                ErrorCode = int.TryParse(G("errorCode"), out var ec) ? ec : -1,
                ResultCode = int.TryParse(G("resultCode"), out var rc) ? rc : -1,
                Message = G("message"),
                PayType = G("payType"),
                ResponseTime = long.TryParse(G("responseTime"), out var rt) ? rt : 0,
                ExtraData = G("extraData"),
                Signature = G("signature")
            };

            return model;
        }


        public async Task HandleSuccessfulPaymentAsync(MomoExecuteResponseModel r)
        {
            var repo = _uow.Payments;
            var payment = await repo.GetFirstOrDefaultAsync(p => p.momoOrderId == r.OrderId);
            if (payment == null || payment.statusId == (int)PaymentStatusEnum.Paid) return;

            payment.statusId = (int)PaymentStatusEnum.Paid;
            payment.statusId = 2;
            payment.updatedDate = DateTime.UtcNow;
            await repo.UpdateAsync(payment);
            await _uow.SaveChangesAsync();
        }

        public async Task HandleFailedPaymentAsync(MomoExecuteResponseModel r)
        {
            var repo = _uow.Payments;
            var payment = await repo.GetFirstOrDefaultAsync(p => p.momoOrderId == r.OrderId);
            if (payment == null || payment.statusId == (int)PaymentStatusEnum.Fail) return;

            payment.statusId = (int)PaymentStatusEnum.Fail;


            payment.statusId = 3;
            payment.updatedDate = DateTime.UtcNow;
            await repo.UpdateAsync(payment);
            await _uow.SaveChangesAsync();
        }


        private static string ComputeHmacSHA256(string data, string key)
        {
            using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(key));
            var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(data));
            return BitConverter.ToString(hash).Replace("-", "").ToLower();
        }
    }
}
