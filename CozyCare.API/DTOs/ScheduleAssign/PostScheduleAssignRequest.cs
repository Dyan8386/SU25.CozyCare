using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyCare.API.DTOs.ScheduleAssign
{
    public class PostScheduleAssignRequest
    {
        public int HousekeeperId { get; set; }

        public int DetailId { get; set; }

        public string Notes { get; set; } = string.Empty;
    }
}
