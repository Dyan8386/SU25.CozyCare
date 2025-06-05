using CozyCare.BLL.Services.Interfaces;
using CozyCare.DAL.Entities;
using CozyCare.DAL.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyCare.BLL.Services.Implements
{
    public class BookingService : IBookingService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BookingService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task AddAsync(Booking booking)
        {
            await _unitOfWork.Bookings.AddAsync(booking);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            await _unitOfWork.Bookings.RemoveAsync(id);
            await _unitOfWork.SaveChangesAsync();   
        }

        public async Task<IEnumerable<Booking>> GetAllAsync()
            => await _unitOfWork.Bookings.GetAllAsync();

        public async Task<Booking?> GetByIdAsync(int id)
            => await _unitOfWork.Bookings.GetByIdAsync(id);

        public async Task UpdateAsync(Booking booking)
        {
            _unitOfWork.Bookings.Update(booking);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
