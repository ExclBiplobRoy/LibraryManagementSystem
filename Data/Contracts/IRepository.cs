using System.Linq.Expressions;

namespace Data.Contracts
{
    public interface IRepository<T>
    {
        T Add(T entity);

        T Get(int id);

        Task<IEnumerable<T>> GetAll();

        void AddRange(IEnumerable<T> entities);

        Task<IEnumerable<T>> QueryAsync(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> obj);

        Task<IEnumerable<T>> QueryAsync(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> obj, Expression<Func<T, object>> next);

        Task<IEnumerable<T>> QueryAsync(Expression<Func<T, bool>> predicate);

        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);

        Task<T> LastOrDefaultAsync(Expression<Func<T, bool>> predicate);

        T GetById(int Key);

        Task<T?> GetByIdAsync(int key);

        void Update(T entity);

        void Delete(T entity);

        Task<T> LoadWithChildAsync<TEntity>(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] expressionList);

        Task<IEnumerable<T>> LoadListWithChildAsync<TEntity>(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] expressionList);
        Task<IEnumerable<T>> LoadListWithChildAsync<TEntity>(Expression<Func<T, bool>> predicate, int skip, int take, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, params Expression<Func<T, object>>[] expressionList);
        int Count(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] expressionList);
    }
}