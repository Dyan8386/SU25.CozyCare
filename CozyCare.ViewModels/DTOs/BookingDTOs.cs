using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyCare.ViewModels.DTOs
{
    public class BookingDto
    {
        public int BookingId { get; set; }
        public string BookingNumber { get; set; } = string.Empty;
        public int CustomerId { get; set; }
        public string PromotionCode { get; set; } = string.Empty;
        public DateTime? BookingDate { get; set; }
        public DateTime Deadline { get; set; }
        public decimal TotalAmount { get; set; }
        public string Notes { get; set; } = string.Empty;
        public int BookingStatusId { get; set; }
        public int PaymentStatusId { get; set; }
        public string Address { get; set; } = string.Empty;
        public List<BookingDetailDto?> bookingDetails { get; set; } = new List<BookingDetailDto?>();
    }

    public class BookingRequest
    {
        //public string? bookingNumber { get; set; }

        public int customerId { get; set; }

        public string? promotionCode { get; set; }

        public DateTime? bookingDate { get; set; }

        public DateTime? deadline { get; set; }

        public decimal totalAmount { get; set; }

        public string? notes { get; set; }

        public int bookingStatusId { get; set; }

        public int paymentStatusId { get; set; }

        public string? address { get; set; } // Add the Address field

    }
}
