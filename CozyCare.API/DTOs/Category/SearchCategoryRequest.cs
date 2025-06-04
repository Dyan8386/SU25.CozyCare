using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyCare.API.DTOs.Category
{
    public class SearchCategoryRequest
    {
        public string CategoryName { get; set; } = string.Empty;

        public bool? IsActive { get; set; }
    }
}
