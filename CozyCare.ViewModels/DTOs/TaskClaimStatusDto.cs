using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyCare.ViewModels.DTOs
{
    public class TaskClaimStatusDto
    {
        public int statusId { get; set; }

       
        public string statusName { get; set; }
    }
    public class CreateAndUpdateTaskClaimStatusDto
    {
       
        public string statusName { get; set; } = string.Empty;
    }
}
