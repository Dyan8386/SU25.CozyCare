using CozyCare.IdentityService.Domain.Entities;
using CozyCare.IdentityService.Infrastructure.DBContext;
using CozyCare.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CozyCare.IdentityService.Infrastructure
{
    public class IdentityUnitOfWork : IIdentityUnitOfWork
    {
        private readonly CozyCareIdentityDbContext _context;

        public IdentityUnitOfWork(CozyCareIdentityDbContext context)
        {
            _context = context;
            Accounts = new GenericRepository<Account>(_context);
            Roles = new GenericRepository<Role>(_context);
            AccountStatuses = new GenericRepository<AccountStatus>(_context);
        }

        public IGenericRepository<Account> Accounts { get; }
        public IGenericRepository<Role> Roles { get; }
        public IGenericRepository<AccountStatus> AccountStatuses { get; }

        public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();

        public void Dispose() => _context.Dispose();
    }

}
