using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyCare.API.DTOs.BookingDetails
{
    public class RescheduleResponse
    {
        public int DetailId { get; set; }
        public DateOnly ScheduleDate { get; set; }
        public TimeOnly ScheduleTime { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
