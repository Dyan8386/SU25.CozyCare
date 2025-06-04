using CozyCare.DAL.Entities;

namespace CozyCare.DAL.Infrastructure
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> SaveChangesAsync();

        IGenericRepository<Account> Accounts { get; }
        IGenericRepository<AccountStatus> AccountStatuses { get; }
        IGenericRepository<AssignmentStatus> AssignmentStatuses { get; }
        IGenericRepository<Booking> Bookings { get; }
        IGenericRepository<BookingDetail> BookingDetails { get; }
        IGenericRepository<BookingStatus> BookingStatuses { get; }
        IGenericRepository<Category> Categories { get; }
        IGenericRepository<Housekeeper> Housekeepers { get; }
        IGenericRepository<HousekeeperAvailability> HousekeeperAvailabilities { get; }
        IGenericRepository<Payment> Payments { get; }
        IGenericRepository<PaymentStatus> PaymentStatuses { get; }
        IGenericRepository<Promotion> Promotions { get; }
        IGenericRepository<Report> Reports { get; }
        IGenericRepository<Review> Reviews { get; }
        IGenericRepository<Role> Roles { get; }
        IGenericRepository<ScheduleAssignment> ScheduleAssignments { get; }
        IGenericRepository<Service> Services { get; }
        IGenericRepository<ServiceDetail> ServiceDetails { get; }
    }
}
