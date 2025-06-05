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
    public class HomeServiceDetailService : IHomeServiceDetailService
    {
        private readonly IUnitOfWork _unitOfWork;

        public HomeServiceDetailService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddAsync(ServiceDetail serviceDetail)
        {
            await _unitOfWork.ServiceDetails.AddAsync(serviceDetail);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            await _unitOfWork.ServiceDetails.RemoveAsync(id);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<ServiceDetail>> GetAllAsync()
            => await _unitOfWork.ServiceDetails.GetAllAsync();

        public async Task<ServiceDetail?> GetByIdAsync(int id)
            => await _unitOfWork.ServiceDetails.GetByIdAsync(id);

        public async Task UpdateAsync(ServiceDetail serviceDetail)
        {
            _unitOfWork.ServiceDetails.Update(serviceDetail);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
