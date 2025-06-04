using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyCare.API.DTOs.ScheduleAssign
{
    public class AssignIdRequest
    {
        [FromRoute(Name = "id")]
        public int Id { get; set; }
    }
}
