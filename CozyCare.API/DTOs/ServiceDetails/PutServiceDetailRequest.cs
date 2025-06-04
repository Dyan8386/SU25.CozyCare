using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyCare.API.DTOs.ServiceDetails
{
    public class PutServiceDetailRequest
    {
        //public int ServiceDetailId { get; set; }
        public int ServiceId { get; set; }  // Foreign key
        public string OptionName { get; set; } = string.Empty;
        public string OptionType { get; set; } = string.Empty;
        public decimal BasePrice { get; set; }
        public string Unit { get; set; } = string.Empty;
        public int Duration { get; set; }
        public string Description { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}
