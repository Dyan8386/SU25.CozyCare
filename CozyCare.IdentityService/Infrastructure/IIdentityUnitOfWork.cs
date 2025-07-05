using CozyCare.IdentityService.Domain.Entities;
using CozyCare.IdentityService.Infrastructure.Repositories;
using CozyCare.Persistence;

namespace CozyCare.IdentityService.Infrastructure
{
    public interface IIdentityUnitOfWork : IUnitOfWork
    {
        IGenericRepository<Account> Accounts { get; }
        IGenericRepository<Role> Roles { get; }
        IGenericRepository<AccountStatus> AccountStatuses { get; }
        IAuthenticationRepository Authentications { get; }
    }
}
