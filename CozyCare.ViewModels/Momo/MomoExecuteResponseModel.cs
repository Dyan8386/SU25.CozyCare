using Microsoft.AspNetCore.Mvc;

namespace CozyCare.ViewModels.Momo
{
    public class MomoExecuteResponseModel
    {
        [FromQuery(Name = "partnerCode")]
        public string? PartnerCode { get; set; }

        [FromQuery(Name = "orderId")]
        public string? OrderId { get; set; }

        [FromQuery(Name = "requestId")]
        public string? RequestId { get; set; }

        [FromQuery(Name = "amount")]
        public string? Amount { get; set; }

        [FromQuery(Name = "orderInfo")]
        public string? OrderInfo { get; set; }

        [FromQuery(Name = "orderType")]
        public string? OrderType { get; set; }

        [FromQuery(Name = "transId")]
        public string? TransId { get; set; }

        [FromQuery(Name = "errorCode")]
        public string? ErrorCode { get; set; }

        [FromQuery(Name = "resultCode")]
        public string? ResultCode { get; set; }

        [FromQuery(Name = "message")]
        public string? Message { get; set; }

        [FromQuery(Name = "payType")]
        public string? PayType { get; set; }

        [FromQuery(Name = "responseTime")]
        public string? ResponseTime { get; set; }

        [FromQuery(Name = "extraData")]
        public string? ExtraData { get; set; }

        [FromQuery(Name = "signature")]
        public string? Signature { get; set; }
    }
}
