using CozyCare.DAL.DBContext;
using CozyCare.DAL.Entities;

namespace CozyCare.DAL.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbFactory _dbFactory;
        private CozyCareContext _context => _dbFactory.Init();

        public UnitOfWork(DbFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }

        // Lazy-loading of repositories
        private IGenericRepository<Account>? _accounts;
        public IGenericRepository<Account> Accounts => _accounts ??= new GenericRepository<Account>(_context);

        private IGenericRepository<AccountStatus>? _accountStatuses;
        public IGenericRepository<AccountStatus> AccountStatuses => _accountStatuses ??= new GenericRepository<AccountStatus>(_context);

        private IGenericRepository<AssignmentStatus>? _assignmentStatuses;
        public IGenericRepository<AssignmentStatus> AssignmentStatuses => _assignmentStatuses ??= new GenericRepository<AssignmentStatus>(_context);

        private IGenericRepository<Booking>? _bookings;
        public IGenericRepository<Booking> Bookings => _bookings ??= new GenericRepository<Booking>(_context);

        private IGenericRepository<BookingDetail>? _bookingDetails;
        public IGenericRepository<BookingDetail> BookingDetails => _bookingDetails ??= new GenericRepository<BookingDetail>(_context);

        private IGenericRepository<BookingStatus>? _bookingStatuses;
        public IGenericRepository<BookingStatus> BookingStatuses => _bookingStatuses ??= new GenericRepository<BookingStatus>(_context);

        private IGenericRepository<Category>? _categories;
        public IGenericRepository<Category> Categories => _categories ??= new GenericRepository<Category>(_context);

        private IGenericRepository<Housekeeper>? _housekeepers;
        public IGenericRepository<Housekeeper> Housekeepers => _housekeepers ??= new GenericRepository<Housekeeper>(_context);

        private IGenericRepository<HousekeeperAvailability>? _housekeeperAvailabilities;
        public IGenericRepository<HousekeeperAvailability> HousekeeperAvailabilities => _housekeeperAvailabilities ??= new GenericRepository<HousekeeperAvailability>(_context);

        private IGenericRepository<Payment>? _payments;
        public IGenericRepository<Payment> Payments => _payments ??= new GenericRepository<Payment>(_context);

        private IGenericRepository<PaymentStatus>? _paymentStatuses;
        public IGenericRepository<PaymentStatus> PaymentStatuses => _paymentStatuses ??= new GenericRepository<PaymentStatus>(_context);

        private IGenericRepository<Promotion>? _promotions;
        public IGenericRepository<Promotion> Promotions => _promotions ??= new GenericRepository<Promotion>(_context);

        private IGenericRepository<Report>? _reports;
        public IGenericRepository<Report> Reports => _reports ??= new GenericRepository<Report>(_context);

        private IGenericRepository<Review>? _reviews;
        public IGenericRepository<Review> Reviews => _reviews ??= new GenericRepository<Review>(_context);

        private IGenericRepository<Role>? _roles;
        public IGenericRepository<Role> Roles => _roles ??= new GenericRepository<Role>(_context);

        private IGenericRepository<ScheduleAssignment>? _scheduleAssignments;
        public IGenericRepository<ScheduleAssignment> ScheduleAssignments => _scheduleAssignments ??= new GenericRepository<ScheduleAssignment>(_context);

        private IGenericRepository<Service>? _services;
        public IGenericRepository<Service> Services => _services ??= new GenericRepository<Service>(_context);

        private IGenericRepository<ServiceDetail>? _serviceDetails;
        public IGenericRepository<ServiceDetail> ServiceDetails => _serviceDetails ??= new GenericRepository<ServiceDetail>(_context);

        public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();

        public void Dispose() => _context.Dispose();
    }
}
