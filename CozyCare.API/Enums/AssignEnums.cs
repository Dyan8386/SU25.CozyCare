using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyCare.API.Enums
{
    public class AssignEnums
    {
        public enum Status
        {
            ASSIGNED,
            INPROGRESS,
            COMPLETED,
            CANCELLED,
            WAITINGCONFIRM,
			REQUESTCANCEL
		}
    }
}
