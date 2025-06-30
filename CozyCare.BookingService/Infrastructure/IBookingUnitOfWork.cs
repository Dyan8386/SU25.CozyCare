using CozyCare.BookingService.Domain.Entities;
using CozyCare.Persistence;

namespace CozyCare.BookingService.Infrastructure
{
    public interface IBookingUnitOfWork : IUnitOfWork
    {
        IGenericRepository<Booking> Bookings { get; }
        IGenericRepository<BookingDetail> BookingDetails { get; }
        IGenericRepository<BookingStatus> BookingStatuses { get; }
    }
}
