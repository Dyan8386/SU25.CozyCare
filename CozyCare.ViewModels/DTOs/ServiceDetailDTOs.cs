using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyCare.ViewModels.DTOs
{
     public class ServiceDetailDto
    {
        public int ServiceDetailId { get; set; }
        public int ServiceId { get; set; }
        public string OptionName { get; set; } = string.Empty;
        public string OptionType { get; set; } = string.Empty;
        public decimal BasePrice { get; set; }
        public string Unit { get; set; } = string.Empty;
        public int Duration { get; set; }
        public string? Description { get; set; }
        public bool? IsActive { get; set; }
    }

    public class CreateServiceDetailDto
    {
        public int ServiceId { get; set; }
        public string OptionName { get; set; } = string.Empty;
        public string OptionType { get; set; } = string.Empty;
        public decimal BasePrice { get; set; }
        public string Unit { get; set; } = string.Empty;
        public int Duration { get; set; }
        public string? Description { get; set; }
    }

    public class UpdateServiceDetailDto
    {
        public string OptionName { get; set; } = string.Empty;
        public string OptionType { get; set; } = string.Empty;
        public decimal BasePrice { get; set; }
        public string Unit { get; set; } = string.Empty;
        public int Duration { get; set; }
        public string? Description { get; set; }
    }

    public class LockServiceDetailDto
    {
        public bool? IsActive { get; set; }

    }
}
