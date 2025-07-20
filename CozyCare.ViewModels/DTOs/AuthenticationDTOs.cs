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

    public class RegisterRequestDto
    {
        public string Email { get; set; } = null!;  // required email
        public string Phone { get; set; } = null!;  // required phone for OTP
        public string FullName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string ConfirmPassword { get; set; } = null!;
    }

    public class ConfirmEmailRequestDto
    {
        public string Email { get; set; } = null!;
        public string Token { get; set; } = null!;
    }

    public class SendOtpRequestDto
    {
        public string Phone { get; set; } = null!;
    }

    public class VerifyOtpRequestDto
    {
        public string Phone { get; set; } = null!;
        public string Otp { get; set; } = null!;
    }

    public class ResetPasswordRequestDto
    {
        public string EmailOrPhone { get; set; } = null!;
        public string Token { get; set; } = null!; // could be OTP or email token
        public string NewPassword { get; set; } = null!;
        public string ConfirmPassword { get; set; } = null!;
    }

}
