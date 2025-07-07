namespace CozyCare.BookingService.DTOs.BookingDetails
{
	public class BDetailRequest
	{
		public int bookingId { get; set; }

		public int serviceId { get; set; }

		public DateTime? scheduleDatetime { get; set; }

		public int quantity { get; set; }

		public decimal? unitPrice { get; set; }
	}
}
