using CozyCare.BLL.Services.Interfaces;
using CozyCare.DAL.Entities;
using CozyCare.DAL.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CozyCare.BLL.Services.Implements
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddAsync(Category category)
        {
            await _unitOfWork.Categories.AddAsync(category);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            await _unitOfWork.Categories.RemoveAsync(id);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
            => await _unitOfWork.Categories.GetAllAsync();

        public async Task<Category?> GetByIdAsync(int id)
            => await _unitOfWork.Categories.GetFirstOrDefaultAsync(p => p.categoryId == id);

        public async Task UpdateAsync(Category category)
        {
            _unitOfWork.Categories.Update(category);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
