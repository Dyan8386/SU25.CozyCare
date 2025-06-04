using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyCare.API.Enums
{
    public class PaymentEnums
    {
        public enum Status
        {
            SUCCESS,
            FAILED,
            PENDING,
            REFUNDREQUESTED,
            REFUNDED
        }
    }
}
