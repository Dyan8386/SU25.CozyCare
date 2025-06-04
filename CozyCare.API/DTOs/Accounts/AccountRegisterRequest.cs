using CozyCare.API.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CozyCare.API.DTOs.Accounts
{
    public class AccountRegisterRequest
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public AccountEnums.Role? Role { get; set; } = AccountEnums.Role.CUSTOMER;

    }
}
