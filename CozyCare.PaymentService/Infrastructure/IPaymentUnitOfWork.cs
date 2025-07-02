using CozyCare.PaymentService.Domain.Entities;
using CozyCare.Persistence;

namespace CozyCare.PaymentService.Infrastructure
{
    public interface IPaymentUnitOfWork : IUnitOfWork
    {
        IGenericRepository<Payment> Payments { get; }
        IGenericRepository<Promotion> Promotions { get; }
        IGenericRepository<PaymentStatus> PaymentStatuses { get; }
    }
}
