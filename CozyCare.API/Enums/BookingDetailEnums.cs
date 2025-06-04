using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyCare.API.Enums
{
    public class BookingDetailEnums
    {
        public enum BookingDetailStatus
        {
            PENDING,
            CANCELLED,
            COMPLETED,
            ASSIGNED,
            WAITINGCONFIRM,
            CANCELREQUESTED,
            CHANGESCHEDULEREQUESTED
        }
    }
}
