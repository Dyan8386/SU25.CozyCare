using System.ComponentModel.DataAnnotations;

namespace CozyCare.BookingService.DTOs.BookingStatuses
{
	public class BStatusResponse
	{
		public int statusId { get; set; }

		public string statusName { get; set; } = string.Empty;
	}
}
