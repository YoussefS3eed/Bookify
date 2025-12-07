namespace Libro.DAL.Repo.Abstraction
{
    public interface ICategoryRepo
    {
        // Command
        Task<Category?> AddAsync(Category category);
        Task<Category?> UpdateAsync(Category category);
        Task<Category?> ToggleStatusAsync(int id);
        Task<bool> AnyAsync(Expression<Func<Category, bool>> predicate);
        // Query
        Task<Category?> GetCategoryByIdAsync(int id);
        IQueryable<Category> GetAllCategories(Expression<Func<Category, bool>>? filter = null);
    }
}
