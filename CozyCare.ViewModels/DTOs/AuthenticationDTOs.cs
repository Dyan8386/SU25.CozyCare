using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyCare.ViewModels.DTOs
{
    public class LoginRequestDto
    {
        public string Input { get; set; } = null!;    // email or phone
        public string Password { get; set; } = null!;
    }

    public class LoginResponseDto
    {
        public string AccessToken { get; set; } = null!;
        public DateTime Expires { get; set; }
        public AccountDto Account { get; set; } = null!;
    }

}
