using CozyCare.BookingService.Infrastructure;
using CozyCare.Persistence;

namespace CozyCare.BookingService.Applications.Externals
{
	public class BookingExpiryService : BackgroundService
	{
		private readonly IServiceProvider _svcProvider;
		private readonly ILogger<BookingExpiryService> _logger;
		private readonly TimeSpan _interval = TimeSpan.FromMinutes(5);

		public BookingExpiryService(IServiceProvider svcProvider,
									ILogger<BookingExpiryService> logger)
		{
			_svcProvider = svcProvider;
			_logger = logger;
		}

		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			_logger.LogInformation("BookingExpiryService started.");

			while (!stoppingToken.IsCancellationRequested)
			{
				try
				{
					using var scope = _svcProvider.CreateScope();
					// Lấy UoW trong scope
					var uow = scope.ServiceProvider.GetRequiredService<IBookingUnitOfWork>();

					var nowUtc = DateTime.UtcNow;
					const int PENDING = 1, EXPIRED = 3;

					// Lấy booking pending
					var pending = await uow.Bookings
						.SearchAsync(b => b.bookingStatusId == PENDING);

					foreach (var b in pending)
					{
						var dt = b.bookingDate?.ToUniversalTime();
						if (dt.HasValue && dt.Value <= nowUtc.AddHours(3))
						{
							b.bookingStatusId = EXPIRED;
							uow.Bookings.Update(b);
							_logger.LogInformation("Expired booking {id}", b.bookingId);
						}
					}

					await uow.SaveChangesAsync();
				}
				catch (Exception ex)
				{
					_logger.LogError(ex, "Error in BookingExpiryService");
				}

				await Task.Delay(_interval, stoppingToken);
			}

			_logger.LogInformation("BookingExpiryService stopped.");
		}
	}
}
