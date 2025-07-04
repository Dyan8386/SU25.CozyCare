using System.ComponentModel.DataAnnotations.Schema;

namespace CozyCare.BookingService.DTOs.BookingDetails
{
	public class BDetailResponse
	{
		public int detailId { get; set; }

		public int bookingId { get; set; }

		public int serviceId { get; set; }

		public DateTime scheduleDatetime { get; set; }

		public int quantity { get; set; }

		public decimal unitPrice { get; set; }
	}
}
