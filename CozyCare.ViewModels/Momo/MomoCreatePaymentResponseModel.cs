using System.Text.Json.Serialization;

namespace CozyCare.ViewModels.Momo
{
    public class MomoCreatePaymentResponseModel
    {
        public string RequestId { get; set; } = string.Empty;
        public int ErrorCode { get; set; }
        public string OrderId { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public string LocalMessage { get; set; } = string.Empty;
        public string RequestType { get; set; } = string.Empty;

        [JsonPropertyName("payUrl")]
        public string PayUrl { get; set; } = string.Empty;

        [JsonPropertyName("signature")]
        public string Signature { get; set; } = string.Empty;

        [JsonPropertyName("qrCodeUrl")]
        public string QrCodeUrl { get; set; } = string.Empty;

        [JsonPropertyName("deeplink")]
        public string Deeplink { get; set; } = string.Empty;

        [JsonPropertyName("deeplinkWebInApp")]
        public string DeeplinkWebInApp { get; set; } = string.Empty;
    }
}
