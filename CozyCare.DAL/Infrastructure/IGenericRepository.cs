using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CozyCare.DAL.Infrastructure
{
	public interface IGenericRepository<T> where T : class
	{
        Task<IEnumerable<T>> SearchAsync(Expression<Func<T, bool>> filter,
                                     string? includeProperties = null);
        Task<T?> GetByIdAsync(object id);
        Task<IEnumerable<T>> GetAllAsync(string? includeProperties = null);
        Task AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);
        void Update(T entity);
        void UpdateRange(IEnumerable<T> entities);
        Task Remove(object id);
        Task RemoveRangeById(IEnumerable<object> ids);
        Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter, string? includeProperties = null);
    }
}
