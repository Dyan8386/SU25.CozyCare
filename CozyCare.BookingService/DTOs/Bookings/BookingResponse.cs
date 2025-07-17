using CozyCare.BookingService.Domain.Entities;
using CozyCare.BookingService.DTOs.BookingDetails;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CozyCare.BookingService.DTOs.Bookings
{
	public class BookingResponse
	{
		public int bookingId { get; set; }

		public string bookingNumber { get; set; } = string.Empty;

		public int customerId { get; set; }

		public string promotionCode { get; set; } = string.Empty;

		public DateTime? bookingDate { get; set; }

		public DateTime deadline { get; set; }

		public decimal totalAmount { get; set; }

		public string notes { get; set; } = string.Empty;

		public int bookingStatusId { get; set; }

		public int paymentStatusId { get; set; }

		public string address { get; set; } = string.Empty; // Add the Address field

		public List<BDetailResponse?> bookingDetails { get; set; } = new List<BDetailResponse?>();

	}
}
