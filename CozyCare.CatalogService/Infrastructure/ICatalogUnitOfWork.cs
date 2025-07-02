using CozyCare.CatalogService.Domain.Entities;
using CozyCare.Persistence;

namespace CozyCare.CatalogService.Infrastructure
{
    public interface ICatalogUnitOfWork : IUnitOfWork
    {
        IGenericRepository<Category> Categories { get; }
        IGenericRepository<Service> Services { get; }
        IGenericRepository<ServiceDetail> ServiceDetails { get; }
    }
}
