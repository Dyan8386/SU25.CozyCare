using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyCare.API.DTOs.ScheduleAssign
{
    public class ScheduleAssignmentResponse
    {
        public int AssignmentId { get; set; }
        public int HousekeeperId { get; set; }
        public string HouseKeeperName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string HouseKeeperPhone { get; set; } = string.Empty;
        public int DetailId { get; set; }
        public DateOnly AssignDate { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public string Status { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
    }
}
