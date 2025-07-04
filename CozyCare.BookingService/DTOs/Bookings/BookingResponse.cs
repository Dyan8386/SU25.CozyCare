using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CozyCare.BookingService.DTOs.Bookings
{
	public class BookingResponse
	{
		public int bookingId { get; set; }

		public string bookingNumber { get; set; }

		public int customerId { get; set; }

		public string promotionCode { get; set; }

		public DateTime? bookingDate { get; set; }

		public DateTime deadline { get; set; }

		public decimal totalAmount { get; set; }

		public string notes { get; set; }

		public int bookingStatusId { get; set; }

		public int paymentStatusId { get; set; }
	}
}
