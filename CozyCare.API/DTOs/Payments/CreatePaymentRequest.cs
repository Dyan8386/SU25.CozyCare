using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyCare.API.DTOs.Payments
{
    public class CreatePaymentRequest
    {
        public int BookingId { get; set; } // ID của Booking cần thanh toán
        //public double Money { get; set; } // Số tiền cần thanh toán
        public string Description { get; set; } = string.Empty; // Mô tả giao dịch  
    }
}
