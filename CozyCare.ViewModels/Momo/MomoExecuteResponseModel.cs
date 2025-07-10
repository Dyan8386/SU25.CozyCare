﻿using System.Text.Json.Serialization;

namespace CozyCare.ViewModels.Momo
{
    public class MomoExecuteResponseModel
    {
        [JsonPropertyName("partnerCode")]
        public string PartnerCode { get; set; } = string.Empty;

        [JsonPropertyName("orderId")]
        public string OrderId { get; set; } = string.Empty;

        [JsonPropertyName("requestId")]
        public string RequestId { get; set; } = string.Empty;

        [JsonPropertyName("amount")]
        public string Amount { get; set; } = string.Empty;

        [JsonPropertyName("orderInfo")]
        public string OrderInfo { get; set; } = string.Empty;

        [JsonPropertyName("orderType")]
        public string OrderType { get; set; } = string.Empty;

        [JsonPropertyName("transId")]
        public string TransId { get; set; } = string.Empty;

        // Dùng cho notify (POST JSON)
        [JsonPropertyName("resultCode")]
        public int ResultCode { get; set; }

        // Dùng cho return (redirect) — Momo trả về query param errorCode
        [JsonPropertyName("errorCode")]
        public int ErrorCode { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; } = string.Empty;

        [JsonPropertyName("payType")]
        public string PayType { get; set; } = string.Empty;

        [JsonPropertyName("responseTime")]
        public long ResponseTime { get; set; }

        [JsonPropertyName("extraData")]
        public string ExtraData { get; set; } = string.Empty;

        [JsonPropertyName("signature")]
        public string Signature { get; set; } = string.Empty;
    }
}
