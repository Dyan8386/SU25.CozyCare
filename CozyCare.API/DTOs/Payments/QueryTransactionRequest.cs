using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyCare.API.DTOs.Payments
{
    public class QueryTransactionRequest
    {
        public string TransactionId { get; set; } = string.Empty;
        public string TransactionDate { get; set; } = string.Empty;
    }
}
