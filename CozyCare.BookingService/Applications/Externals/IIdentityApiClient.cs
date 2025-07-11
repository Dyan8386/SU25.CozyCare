using CozyCare.SharedKernel.Base;
using CozyCare.ViewModels.DTOs;

namespace CozyCare.BookingService.Application.Externals
{
    public interface IIdentityApiClient
    {
        Task<BaseResponse<CurrentAccountDto?>> GetCurrentAccountAsync(string accessToken);
        Task<BaseResponse<AccountDto>> GetAccountById(int accountId, CancellationToken ct = default);

	}

}
