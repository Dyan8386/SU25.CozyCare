using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyCare.API.DTOs.Payments
{
    public class PaymentResponse
    {
        public int PaymentId { get; set; }

        public int CustomerId { get; set; }

        public int BookingId { get; set; }

        public decimal Amount { get; set; }

        public string PaymentMethod { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;

        public DateTime? PaymentDate { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public string Notes { get; set; } = string.Empty;

        public string TransactionId { get; set; } = string.Empty;
    }
}
