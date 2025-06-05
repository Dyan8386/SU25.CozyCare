using CozyCare.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyCare.BLL.Services.Interfaces
{
    public interface IBookingDetailService
    {
        Task<IEnumerable<BookingDetail>> GetAllAsync();
        Task<BookingDetail?> GetByIdAsync(int id);
        Task AddAsync(BookingDetail bookingDetail);
        Task UpdateAsync(BookingDetail bookingDetail);
        Task DeleteAsync(int id);
    }
}
