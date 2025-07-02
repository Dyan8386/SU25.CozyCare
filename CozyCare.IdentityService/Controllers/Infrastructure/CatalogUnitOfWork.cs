using CozyCare.CatalogService.Domain.Entities;
using CozyCare.CatalogService.Infrastructure.DBContext;
using CozyCare.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CozyCare.CatalogService.Infrastructure
{
    public class CatalogUnitOfWork : ICatalogUnitOfWork
    {
        private readonly CozyCareCatalogDbContext _context;

        public CatalogUnitOfWork(CozyCareCatalogDbContext context)
        {
            _context = context;
            Categories = new GenericRepository<Category>(_context);
            Services = new GenericRepository<Service>(_context);
            ServiceDetails = new GenericRepository<ServiceDetail>(_context);
        }

        public IGenericRepository<Category> Categories { get; }
        public IGenericRepository<Service> Services { get; }
        public IGenericRepository<ServiceDetail> ServiceDetails { get; }

        public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();

        public void Dispose() => _context.Dispose();
    }

}
