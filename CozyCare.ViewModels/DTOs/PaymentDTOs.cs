﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyCare.ViewModels.DTOs
{
    public class PaymentDto
    {
        public int PaymentId { get; set; }
        public int UserId { get; set; }
        public int BookingId { get; set; }
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; } = string.Empty;
        public int StatusId { get; set; }
        public DateTime? PaymentDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? Notes { get; set; }
        public string? MomoOrderId { get; set; }
    }

    public class CreatePaymentDto
    {
        public int BookingId { get; set; }
        public int UserId { get; set; }
        public decimal Amount { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string? Notes { get; set; }
    }
    public class CallbackDto
    {
        public decimal Amount { get; set; }
        public string OrderInfo { get; set; } = string.Empty;
        public int ErrorCode { get; set; }
        public string Message { get; set; } = string.Empty;

        public string? Token { get; set; }
    }
}
