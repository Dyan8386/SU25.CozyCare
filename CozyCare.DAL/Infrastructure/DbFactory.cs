using CozyCare.DAL.DBContext;
using Microsoft.EntityFrameworkCore;

namespace CozyCare.DAL.Infrastructure
{
    public class DbFactory : IDbFactory
    {
        private CozyCareContext? _dbContext;
        private readonly DbContextOptions<CozyCareContext> _options;

        public DbFactory(DbContextOptions<CozyCareContext> options)
        {
            _options = options;
        }

        public CozyCareContext Init()
        {
            return _dbContext ??= new CozyCareContext(_options);
        }

        public void Dispose()
        {
            _dbContext?.Dispose();
        }
    }
}
