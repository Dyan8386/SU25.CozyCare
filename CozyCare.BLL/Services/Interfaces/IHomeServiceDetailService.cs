using CozyCare.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyCare.BLL.Services.Interfaces
{
    public interface IHomeServiceDetailService
    {
        Task<IEnumerable<ServiceDetail>> GetAllAsync();
        Task<ServiceDetail?> GetByIdAsync(int id);
        Task AddAsync(ServiceDetail serviceDetail);
        Task UpdateAsync(ServiceDetail serviceDetail);
        Task DeleteAsync(int id);
    }
}
