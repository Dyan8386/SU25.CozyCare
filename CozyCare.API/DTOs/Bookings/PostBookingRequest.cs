using CozyCare.API.DTOs.BookingDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyCare.API.DTOs.Bookings
{
    public class PostBookingRequest
    {
        public int CustomerId { get; set; }
        public string? PromotionCode { get; set; } 
        //public DateTime? BookingDate { get; set; }
        //public string BookingStatus { get; set; }
        //public string PaymentStatus { get; set; }
        public string Notes { get; set; } = string.Empty;
        public string PaymentMethod { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public List<PostBookingDetailRequest> BookingDetails { get; set; } = new List<PostBookingDetailRequest>();
    }
}
