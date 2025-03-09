using System.Linq.Expressions;

namespace Blogify.Infrastructure.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task AddAsync(T entity);
        Task<IEnumerable<T>> GetAllAsync();
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
        Task<T> FindAsync(Expression<Func<T, bool>> predicate);
    }
}
