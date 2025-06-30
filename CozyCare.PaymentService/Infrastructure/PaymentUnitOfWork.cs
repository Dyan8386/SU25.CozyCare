using CozyCare.PaymentService.Domain.Entities;
using CozyCare.PaymentService.Infrastructure.DBContext;
using CozyCare.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CozyCare.PaymentService.Infrastructure
{
    public class PaymentUnitOfWork : IPaymentUnitOfWork
    {
        private readonly CozyCarePaymentDbContext _context;

        public PaymentUnitOfWork(CozyCarePaymentDbContext context)
        {
            _context = context;
            Payments = new GenericRepository<Payment>(_context);
            Promotions = new GenericRepository<Promotion>(_context);
            PaymentStatuses = new GenericRepository<PaymentStatus>(_context);
        }

        public IGenericRepository<Payment> Payments { get; }
        public IGenericRepository<Promotion> Promotions { get; }
        public IGenericRepository<PaymentStatus> PaymentStatuses { get; }

        public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();

        public void Dispose() => _context.Dispose();
    }

}
