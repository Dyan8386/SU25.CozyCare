using System.Linq.Expressions;

namespace CozyCare.Persistence
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync(string? includeProperties = null);
        Task<T?> GetByIdAsync(object id);
        Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter, string? includeProperties = null);
        Task<IEnumerable<T>>? SearchAsync(Expression<Func<T, bool>> filter,
                                              Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy,
                                              string? includeProperties = null);        
        Task AddAsync(T entity);
        void Update(T entity);
        Task DeleteAsync(object id);
    }
}
