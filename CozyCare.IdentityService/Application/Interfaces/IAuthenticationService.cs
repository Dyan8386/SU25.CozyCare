using CozyCare.SharedKernel.Base;
using CozyCare.ViewModels.DTOs;

namespace CozyCare.IdentityService.Application.Interfaces
{
    public interface IAuthenticationService
    {
        Task<BaseResponse<LoginResponseDto>> AuthenticateAsync(LoginRequestDto request);
    }
}
