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
    }
}
