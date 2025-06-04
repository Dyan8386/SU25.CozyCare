using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyCare.API.DTOs.Payments
{
    public class PutPaymentWithBooking
    {
        public string PaymentMethod { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;

        public string TransactionId { get; set; } = string.Empty;
    }
}
