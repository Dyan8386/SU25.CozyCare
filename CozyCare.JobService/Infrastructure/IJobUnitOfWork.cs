using CozyCare.JobService.Domain.Entities;
using CozyCare.Persistence;

namespace CozyCare.JobService.Infrastructure
{
    public interface IJobUnitOfWork : IUnitOfWork
    {
        IGenericRepository<TaskClaim> TaskClaims { get; }
        IGenericRepository<TaskClaimStatus> TaskClaimStatus { get; }
        IGenericRepository<Review> Reviews { get; }
    }
}
