using CozyCare.JobService.Domain.Entities;
using CozyCare.JobService.Infrastructure.DBContext;
using CozyCare.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CozyCare.JobService.Infrastructure
{
    public class JobUnitOfWork : IJobUnitOfWork
    {
        private readonly CozyCareJobDbContext _context;
        public JobUnitOfWork(CozyCareJobDbContext context)
        {
            _context = context;
            TaskClaims = new GenericRepository<TaskClaim>(_context);
            TaskClaimStatus = new GenericRepository<TaskClaimStatus>(_context);
            Reviews = new GenericRepository<Review>(_context);
        }

        public IGenericRepository<TaskClaim> TaskClaims { get; }
        public IGenericRepository<TaskClaimStatus> TaskClaimStatus { get; }
        public IGenericRepository<Review> Reviews { get; }

        public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();

        public void Dispose() => _context.Dispose();
    }
}
