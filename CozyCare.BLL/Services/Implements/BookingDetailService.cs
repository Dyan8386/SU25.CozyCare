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
    public class BookingDetailService : IBookingDetailService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BookingDetailService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task AddAsync(BookingDetail bookingDetail)
        {
            await _unitOfWork.BookingDetails.AddAsync(bookingDetail);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            await _unitOfWork.BookingDetails.RemoveAsync(id);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<BookingDetail>> GetAllAsync()
            => await _unitOfWork.BookingDetails.GetAllAsync();

        public async Task<BookingDetail?> GetByIdAsync(int id)
            => await _unitOfWork.BookingDetails.GetByIdAsync(id);

        public async Task UpdateAsync(BookingDetail bookingDetail)
        {
            _unitOfWork.BookingDetails.Update(bookingDetail);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
