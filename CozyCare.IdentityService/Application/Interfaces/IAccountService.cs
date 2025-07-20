using CozyCare.SharedKernel.Base;
using CozyCare.ViewModels.DTOs;

namespace CozyCare.IdentityService.Application.Interfaces
{
    public interface IAccountService
    {
        Task<BaseResponse<AccountDto>> CreateAccountForAdminAsync(CreateAccountRequestDto request);
        Task<BaseResponse<AccountDto>> GetByIdAsync(int id);
        Task<BaseResponse<IEnumerable<AccountDto>>> GetAllAsync();
        Task<BaseResponse<AccountDto>> UpdateAsync(int id, UpdateAccountRequestDto request);
        Task<BaseResponse<string>> DeleteAsync(int id);
        Task<BaseResponse<string>> ToggleAccountStatusAsync(int id);
    }
}
