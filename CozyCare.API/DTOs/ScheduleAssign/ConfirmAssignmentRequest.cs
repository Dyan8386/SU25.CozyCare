using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyCare.API.DTOs.ScheduleAssign
{
    public class ConfirmAssignmentRequest
    {
        public int CustomerId { get; set; }
        public int BookingDetailId { get; set; }

    }
}
