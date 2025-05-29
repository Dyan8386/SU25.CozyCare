using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyCare.BLL.Errors
{
    public class ErrorDetail
    {
        public string FieldNameError { get; set; } = string.Empty;
        public List<string> DescriptionError { get; set; } = new List<string>();
    }
}
