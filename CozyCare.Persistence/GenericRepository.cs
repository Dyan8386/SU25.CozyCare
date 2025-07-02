using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CozyCare.Persistence
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(DbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        #region Async Methods

        public async Task<IEnumerable<T>> GetAllAsync(string? includeProperties = null)
        {
            IQueryable<T> query = _dbSet;
            if (!string.IsNullOrWhiteSpace(includeProperties))
            {
                foreach (var includeProperty in includeProperties.Split(',', StringSplitOptions.RemoveEmptyEntries))
                    query = query.Include(includeProperty.Trim());
            }
            return await query.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(object id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter, string? includeProperties = null)
        {
            IQueryable<T> query = _dbSet.Where(filter);
            if (!string.IsNullOrWhiteSpace(includeProperties))
            {
                foreach (var includeProperty in includeProperties.Split(',', StringSplitOptions.RemoveEmptyEntries))
                    query = query.Include(includeProperty.Trim());
            }
            return await query.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> SearchAsync(
            Expression<Func<T, bool>> filter,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            string? includeProperties = null)
        {
            IQueryable<T> query = _dbSet.Where(filter);

            // Include các bảng liên quan nếu có
            if (!string.IsNullOrWhiteSpace(includeProperties))
            {
                foreach (var includeProperty in includeProperties.Split(',', StringSplitOptions.RemoveEmptyEntries))
                    query = query.Include(includeProperty.Trim());
            }

            // Nếu không có orderBy, thử mặc định sắp xếp theo property "Id" hoặc tương đương
            if (orderBy != null)
            {
                query = orderBy(query);
            }
            else
            {
                var param = Expression.Parameter(typeof(T), "x");
                var prop = typeof(T).GetProperties()
                    .FirstOrDefault(p => string.Equals(p.Name, "Id", StringComparison.OrdinalIgnoreCase) ||
                                         string.Equals(p.Name, $"{typeof(T).Name}Id", StringComparison.OrdinalIgnoreCase));

                if (prop != null)
                {
                    var propAccess = Expression.Property(param, prop);
                    var lambda = Expression.Lambda(propAccess, param);
                    var method = typeof(Queryable).GetMethods()
                        .Where(m => m.Name == "OrderBy" && m.GetParameters().Length == 2)
                        .Single()
                        .MakeGenericMethod(typeof(T), prop.PropertyType);

                    query = (IOrderedQueryable<T>)method.Invoke(null, new object[] { query, lambda })!;
                }
            }

            return await query.ToListAsync();
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task DeleteAsync(object id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
                _dbSet.Remove(entity);
        }

        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await Task.CompletedTask;
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public async Task UpdateRangeAsync(IEnumerable<T> entities)
        {
            _dbSet.UpdateRange(entities);
            await Task.CompletedTask;
        }

        public async Task DeleteRangeAsync(IEnumerable<object> ids)
        {
            foreach (var id in ids)
            {
                var entity = await _dbSet.FindAsync(id);
                if (entity != null)
                    _dbSet.Remove(entity);
            }
        }


        #endregion

        #region Sync Methods

        public IEnumerable<T> GetAll(string? includeProperties = null)
        {
            IQueryable<T> query = _dbSet;
            if (!string.IsNullOrWhiteSpace(includeProperties))
            {
                foreach (var includeProperty in includeProperties.Split(',', StringSplitOptions.RemoveEmptyEntries))
                    query = query.Include(includeProperty.Trim());
            }
            return query.ToList();
        }

        public T? GetById(object id)
        {
            return _dbSet.Find(id);
        }

        public T? GetFirstOrDefault(Expression<Func<T, bool>> filter, string? includeProperties = null)
        {
            IQueryable<T> query = _dbSet.Where(filter);
            if (!string.IsNullOrWhiteSpace(includeProperties))
            {
                foreach (var includeProperty in includeProperties.Split(',', StringSplitOptions.RemoveEmptyEntries))
                    query = query.Include(includeProperty.Trim());
            }
            return query.FirstOrDefault();
        }

        public IEnumerable<T> Search(
             Expression<Func<T, bool>> filter,
             Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
             string? includeProperties = null)
        {
            IQueryable<T> query = _dbSet.Where(filter);

            if (!string.IsNullOrWhiteSpace(includeProperties))
            {
                foreach (var includeProperty in includeProperties.Split(',', StringSplitOptions.RemoveEmptyEntries))
                    query = query.Include(includeProperty.Trim());
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }
            else
            {
                var param = Expression.Parameter(typeof(T), "x");
                var prop = typeof(T).GetProperties()
                    .FirstOrDefault(p => string.Equals(p.Name, "Id", StringComparison.OrdinalIgnoreCase) ||
                                         string.Equals(p.Name, $"{typeof(T).Name}Id", StringComparison.OrdinalIgnoreCase));

                if (prop != null)
                {
                    var propAccess = Expression.Property(param, prop);
                    var lambda = Expression.Lambda(propAccess, param);
                    var method = typeof(Queryable).GetMethods()
                        .Where(m => m.Name == "OrderBy" && m.GetParameters().Length == 2)
                        .Single()
                        .MakeGenericMethod(typeof(T), prop.PropertyType);

                    query = (IOrderedQueryable<T>)method.Invoke(null, new object[] { query, lambda })!;
                }
            }

            return query.ToList();
        }


        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public void Delete(object id)
        {
            var entity = _dbSet.Find(id);
            if (entity != null)
                _dbSet.Remove(entity);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public void AddRange(IEnumerable<T> entities)
        {
            _dbSet.AddRange(entities);
        }

        public void UpdateRange(IEnumerable<T> entities)
        {
            _dbSet.UpdateRange(entities);
        }

        public void DeleteRange(IEnumerable<object> ids)
        {
            foreach (var id in ids)
            {
                var entity = _dbSet.Find(id);
                if (entity != null)
                    _dbSet.Remove(entity);
            }
        }


        #endregion
    }
}
