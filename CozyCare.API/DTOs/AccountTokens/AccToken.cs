using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyCare.API.DTOs.AccountTokens
{
    public class AccToken
    {
        public int AccountId { get; set; }
        public string JWTId { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime ExpiredDate { get; set; }
    }
}
