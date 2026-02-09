namespace Libro.DAL.Repositories.Abstraction
{
    public interface IRepository<T> where T : class
    {
        // Command
        Task<T?> AddAsync(T entity);
        // Query
        Task<T?> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null);
        Task<T?> GetSingleOrDefaultAsync(Expression<Func<T, bool>> predicate);
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
    }
}
