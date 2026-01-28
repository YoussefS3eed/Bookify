namespace Libro.DAL.Repositories.Abstraction
{
    public interface ICategoryRepo
    {
        // Command
        Task<Category?> AddAsync(Category category);
        Task<Category?> UpdateAsync(Category category);
        Task<Category?> ToggleStatusAsync(int id);

        // Query
        Task<Category?> GetCategoryByIdAsync(int id);
        IQueryable<Category> GetAllCategories(Expression<Func<Category, bool>>? filter = null);
        Task<Category?> GetSingleOrDefaultAsync(Expression<Func<Category, bool>> predicate);
        Task<bool> AnyAsync(Expression<Func<Category, bool>> predicate);
    }
}
