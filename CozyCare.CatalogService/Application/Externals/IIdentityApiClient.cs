using CozyCare.SharedKernel.Base;
using CozyCare.ViewModels.DTOs;

namespace CozyCare.CatalogService.Application.Externals
{
    public interface IIdentityApiClient
    {
        Task<BaseResponse<CurrentAccountDto?>> GetCurrentAccountAsync(string accessToken);
    }

}
