using CozyCare.IdentityService.Domain.Entities;
using CozyCare.Persistence;

namespace CozyCare.IdentityService.Infrastructure.Repositories
{
    public interface IAuthenticationRepository : IGenericRepository<Account>
    {
        Task<Account?> AuthenticateAsync(string input, string password);
    }
}
