using CozyCare.BookingService.Domain.Entities;
using CozyCare.BookingService.Infrastructure.DBContext;
using CozyCare.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CozyCare.BookingService.Infrastructure
{
    public class BookingUnitOfWork : IBookingUnitOfWork
    {
        private readonly CozyCareBookingDbContext _context;

        public BookingUnitOfWork(CozyCareBookingDbContext context)
        {
            _context = context;
            Bookings = new GenericRepository<Booking>(_context);
            BookingDetails = new GenericRepository<BookingDetail>(_context);
            BookingStatuses = new GenericRepository<BookingStatus>(_context);
        }

        public IGenericRepository<Booking> Bookings { get; }
        public IGenericRepository<BookingDetail> BookingDetails { get; }
        public IGenericRepository<BookingStatus> BookingStatuses { get; }

        public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();

        public void Dispose() => _context.Dispose();
    }
}
