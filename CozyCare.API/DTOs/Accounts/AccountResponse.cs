using CozyCare.API.DTOs.AccountTokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyCare.API.DTOs.Accounts
{
    public class AccountResponse
    {
        public int AccountId { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public AccountTokenResponse? Tokens { get; set; }
    }
}
