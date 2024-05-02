using Data;
using Data.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DataContext context;

        public Repository(DataContext context)
        {
            this.context = context;
        }

        public T Add(T entity)
        {
            try
            {
                return context.Set<T>().Add(entity).Entity;
            }
            catch
            {
                throw;
            }
        }

        public T Get(int id)
        {
            try
            {
                return context.Set<T>().Find(id);
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            try
            {
                return await context.Set<T>()
                    .AsQueryable()
                    .AsNoTracking()
                    .ToListAsync();
            }
            catch
            {
                throw;
            }
        }

        public void AddRange(IEnumerable<T> entities)
        {
            context.Set<T>().AddRange(entities);
        }

        public async Task<IEnumerable<T>> QueryAsync(Expression<Func<T, bool>> predicate)
        {
            try
            {
                return await context.Set<T>()
                    .AsQueryable()
                    .AsNoTracking()
                    .Where(predicate)
                    .ToListAsync();
            }
            catch
            {
                throw;
            }
        }

        public virtual async Task<IEnumerable<T>> QueryAsync(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> obj)
        {
            try
            {
                return await context.Set<T>()
                    .AsQueryable()
                    .Where(predicate)
                    .Include(obj)
                    .ToListAsync();
            }
            catch
            {
                throw;
            }
        }

        public virtual async Task<IEnumerable<T>> QueryAsync(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> obj, Expression<Func<T, object>> next)
        {
            try
            {
                return await context.Set<T>()
                      .AsQueryable()
                      .Where(predicate)
                      .Include(obj)
                      .Include(next)
                      .ToListAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            try
            {
                return await context.Set<T>()
                    .AsNoTracking()
                    .FirstOrDefaultAsync(predicate);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<T> LastOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            try
            {
                return await context.Set<T>()
                    .AsNoTracking()
                    .OrderByDescending(predicate)
                    .FirstOrDefaultAsync(predicate);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Update(T entity)
        {
            try
            {
                context.Entry(entity).State = EntityState.Modified;
                context.Set<T>().Update(entity);
            }
            catch
            {
                throw;
            }
        }

        public void Delete(T entity)
        {
            try
            {
                context.Set<T>().Remove(entity);
            }
            catch
            {
                throw;
            }
        }

        public async Task<T> LoadWithChildWithOrderByAsync<TEntity>(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, params Expression<Func<T, object>>[] expressionList)
        {
            var query = context.Set<T>().AsQueryable();

            foreach (var expression in expressionList)
            {
                query = query.Include(expression);
            }

            if (orderBy != null)
                query = orderBy(query);

            return await query.FirstOrDefaultAsync(predicate);
        }

        public async Task<T> LoadWithChildAsync<TEntity>(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] expressionList)
        {
            var query = context.Set<T>().AsQueryable();

            foreach (var expression in expressionList)
            {
                query = query.Include(expression);
            }

            return await query.FirstOrDefaultAsync(predicate);
        }

        public async Task<IEnumerable<T>> LoadListWithChildAsync<TEntity>(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] expressionList)
        {
            var query = context.Set<T>().AsQueryable();

            foreach (var expression in expressionList)
            {
                query = query.Include(expression);
            }

            return await query.Where(predicate).ToListAsync();
        }
        public async Task<IEnumerable<T>> LoadListWithChildAsync<TEntity>(Expression<Func<T, bool>> predicate, int skip, int take, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, params Expression<Func<T, object>>[] expressionList)
        {
            var query = context.Set<T>().AsQueryable();

            foreach (var expression in expressionList)
            {
                query = query.Include(expression);
            }
            if (orderBy != null)
                query = orderBy(query);

            return await query.Where(predicate).Skip(skip).Take(take).ToListAsync();
        }
        public int Count(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] expressionList)
        {
            foreach (var expression in expressionList)
            {
                context.Set<T>().Include(expression);
            }

            return context.Set<T>().Where(filter).Count();

        }

        public T GetById(int Key)
        {
            try
            {
                var entity = context.Set<T>().Find(Key);
                context.Entry(entity).State = EntityState.Detached;
                return entity;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<T?> GetByIdAsync(int key)
        {
            var entity = await context.Set<T>().FindAsync(key);
            if (entity == null)
            {
                return null;
            }

            context.Entry(entity).State = EntityState.Detached;
            return entity;
        }

    }

    public class EntityBase
    {
        public int Id { get; set; }
    }
}