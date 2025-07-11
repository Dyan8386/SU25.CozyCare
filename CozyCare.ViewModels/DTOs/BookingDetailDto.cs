using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyCare.ViewModels.DTOs
{
    public class BookingDetailDto
    {
        public int detailId { get; set; }

        public int bookingId { get; set; }

        public int serviceId { get; set; }

        public DateTime scheduleDatetime { get; set; }

        public int quantity { get; set; }

        public decimal unitPrice { get; set; }
    }
}
