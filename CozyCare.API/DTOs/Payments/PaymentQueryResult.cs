using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyCare.API.DTOs.Payments
{
    public class PaymentQueryResult
    {
        [JsonProperty("vnp_ResponseCode")]
        public string ResponseCode { get; set; } = string.Empty;    

        [JsonProperty("vnp_TransactionStatus")]
        public string TransactionStatus { get; set; } = string.Empty ;

        [JsonProperty("vnp_TransactionNo")]
        public string TransactionNo { get; set; }

        [JsonProperty("vnp_TxnRef")]
        public string TransactionId { get; set; }

        [JsonProperty("vnp_Amount")]
        public long Amount { get; set; }

        [JsonProperty("vnp_OrderInfo")]
        public string OrderInfo { get; set; }

        [JsonProperty("vnp_PayDate")]
        public string PayDate { get; set; }
    }

}
