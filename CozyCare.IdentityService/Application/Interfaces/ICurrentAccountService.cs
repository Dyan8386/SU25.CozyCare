using CozyCare.ViewModels.DTOs;

namespace CozyCare.IdentityService.Application.Interfaces
{
    public interface ICurrentAccountService
    {
        CurrentAccountDto? GetCurrentAccount();
    }
}
