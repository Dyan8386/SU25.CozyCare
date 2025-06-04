using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyCare.API.DTOs.Bookings
{
    public class BookingResponse
    {
        public int BookingId { get; set; }

        public int CustomerId { get; set; }

        public string Email { get; set; } = string.Empty;

        public string PromotionCode { get; set; } = string.Empty;

        public DateTime? BookingDate { get; set; }

        public decimal TotalAmount { get; set; }

        public string BookingStatus { get; set; } = string.Empty;

        public string PaymentStatus { get; set; } = string.Empty;

        public string Notes { get; set; } = string.Empty;

        public string PaymentMethod { get; set; } = string.Empty;
    }
}
