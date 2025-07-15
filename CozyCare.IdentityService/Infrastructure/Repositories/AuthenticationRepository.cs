using CozyCare.IdentityService.Domain.Entities;
using CozyCare.IdentityService.Infrastructure.DBContext;
using CozyCare.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CozyCare.IdentityService.Infrastructure.Repositories
{
    public class AuthenticationRepository : GenericRepository<Account>,IAuthenticationRepository
    {
        private readonly CozyCareIdentityDbContext _context;

        public AuthenticationRepository(CozyCareIdentityDbContext context) : base(context)
        {
            _context = context;
        }


        public async Task<Account?> AuthenticateAsync(string input, string password)
        {
            return await _context.Accounts
                .Include(a => a.role)
                .Include(a => a.status)
                .FirstOrDefaultAsync(a =>
                    (a.email == input || a.phone == input) &&
                    a.password == password && a.status.statusId == 1);
        }
    }
}
