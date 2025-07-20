using CozyCare.SharedKernel.Base;
using CozyCare.ViewModels.DTOs;

namespace CozyCare.IdentityService.Application.Interfaces
{
    public interface IAuthenticationService
    {
        Task<BaseResponse<LoginResponseDto>> AuthenticateAsync(LoginRequestDto request);
        Task<BaseResponse<AccountDto>> RegisterAsync(RegisterRequestDto request);
        Task<BaseResponse<string>> ConfirmEmailAsync(string email, string token);
        Task<BaseResponse<string>> SendOtpAsync(string phone);
        Task<BaseResponse<string>> VerifyOtpAsync(string phone, string otp);
        Task<BaseResponse<string>> ResetPasswordAsync(ResetPasswordRequestDto request);
    }
}
