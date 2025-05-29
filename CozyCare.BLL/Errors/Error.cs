using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyCare.BLL.Errors
{
    public class Error
    {
        public int StatusCode { get; set; }
        public List<ErrorDetail> Message { get; set; } = new List<ErrorDetail>();
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
