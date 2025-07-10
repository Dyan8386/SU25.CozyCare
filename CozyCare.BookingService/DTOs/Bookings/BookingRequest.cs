namespace CozyCare.BookingService.DTOs.Bookings
{
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
