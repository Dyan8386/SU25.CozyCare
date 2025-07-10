using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyCare.ViewModels.DTOs
{
    public class TaskClaimDto
    {
        public int claimId { get; set; }

        public int detailId { get; set; }

        public int housekeeperId { get; set; }

        
        public DateTime? claimDate { get; set; }

        public int statusId { get; set; }

        public string note { get; set; }
    }
    public class CreateTaskClaimDto
    {
        public int detailId { get; set; }
        public int housekeeperId { get; set; }
        public DateTime? claimDate { get; set; }
        public int statusId { get; set; }
        public string note { get; set; }
    }
    public class UpdateTaskClaimDto
    {
        public int detailId { get; set; }
        public int housekeeperId { get; set; }
        public DateTime? claimDate { get; set; }
        public int statusId { get; set; }
        public string note { get; set; }
    }
   
}
