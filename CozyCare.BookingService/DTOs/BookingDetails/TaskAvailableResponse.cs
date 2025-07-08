namespace CozyCare.BookingService.DTOs.BookingDetails
{
	public class TaskAvailableResponse
	{
		public int DetailId { get; set; }
		public int BookingId { get; set; }
		public int ServiceId { get; set; }         // lấy từ Service
		public string BookingNumber { get; set; }      // lấy từ Booking
		public int BookingStatusId { get; set; }      // lấy từ Booking
		public DateTime ScheduleDatetime { get; set; }
		public int Quantity { get; set; }
		public decimal UnitPrice { get; set; }
	}
}
