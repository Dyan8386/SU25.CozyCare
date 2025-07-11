using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyCare.ViewModels.DTOs
{
    public class ServiceDto
    {
        public int ServiceId { get; set; }
        public int CategoryId { get; set; }
        public string ServiceName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? Image { get; set; }
        public decimal Price { get; set; }
        public int Duration { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }

    public class CreateServiceDto
    {
        public int CategoryId { get; set; }
        public string ServiceName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? Image { get; set; }
        public decimal Price { get; set; }
        public int Duration { get; set; }
    }

    public class UpdateServiceDto
    {
        public string ServiceName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? Image { get; set; }
        public decimal Price { get; set; }
        public int Duration { get; set; }
        
    }

    public class LockServiceDto
    {
        public bool? IsActive { get; set; }
    }
}
