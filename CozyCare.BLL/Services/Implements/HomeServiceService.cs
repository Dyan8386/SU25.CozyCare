using CozyCare.BLL.Services.Interfaces;
using CozyCare.DAL.Entities;
using CozyCare.DAL.Infrastructure;

namespace CozyCare.BLL.Services.Implements
{
    public class HomeServiceService : IHomeServiceService
    {
        private readonly IUnitOfWork _unitOfWork;

        public HomeServiceService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddAsync(Service service)
        {
            await _unitOfWork.Services.AddAsync(service);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            await _unitOfWork.Services.RemoveAsync(id);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<Service>> GetAllAsync() => await _unitOfWork.Services.GetAllAsync();

        public async Task<Service?> GetByIdAsync(int id) => await _unitOfWork.Services.GetByIdAsync(id);

        public async Task UpdateAsync(Service service)
        {
            _unitOfWork.Services.Update(service);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
