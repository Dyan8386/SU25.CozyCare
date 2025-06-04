using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyCare.API.DTOs.BookingDetails
{
    public class BookingDetailResponse
    {
        public int DetailId { get; set; }

        public int BookingId { get; set; }

        public int ServiceId { get; set; }

        public string ServiceName { get; set; } = string.Empty;

        public int ServiceDetailId { get; set; }

        public string ServiceDetailName { get; set; } = string.Empty;

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public DateOnly ScheduleDate { get; set; }
        
        public TimeOnly ScheduleTime { get; set; }

		public bool? IsAssign { get; set; }

		public string BookdetailStatus { get; set; } = string.Empty;
		//public virtual Booking Booking { get; set; }

		//public virtual CCSystem.DAL.Models.Service Service { get; set; }

		//public virtual ServiceDetail ServiceDetail { get; set; }
	}
}
