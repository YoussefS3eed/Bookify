using Libro.DAL.Repositories.Abstraction;

namespace Libro.DAL.Repositories.Implementation
{
    public class CategoryRepo : ICategoryRepo
    {
        private readonly LibroDbContext _context;
        public CategoryRepo(LibroDbContext context)
        {
            _context = context;
        }
        private async Task<bool> SaveChangesAsync()
            => await _context.SaveChangesAsync() > 0;
        public async Task<Category?> AddAsync(Category category)
        {
            try
            {
                await _context.Categories.AddAsync(category);
                if (await SaveChangesAsync())
                    return category;
                return null;
            }
            catch
            {
                throw;
            }
        }
        public async Task<Category?> UpdateAsync(Category newCategory)
        {
            try
            {
                var updatedCategory = await GetCategoryByIdAsync(newCategory.Id);
                if (updatedCategory is not null)
                {
                    var isUpdated = updatedCategory.Update(newCategory.Name, newCategory.UpdatedBy! ?? "System");
                    if (isUpdated)
                    {
                        if (await SaveChangesAsync())
                            return updatedCategory;
                    }
                }
                return null;
            }
            catch
            {
                throw;
            }
        }
        public async Task<Category?> ToggleStatusAsync(int id)
        {
            try
            {
                var category = await GetCategoryByIdAsync(id);
                category!.ToggleStatus("System");
                if (await SaveChangesAsync())
                    return category;
                return null;
            }
            catch
            {
                throw;
            }
        }
        public async Task<bool> AnyAsync(Expression<Func<Category, bool>> predicate)
            => await _context.Categories.AnyAsync(predicate);
        public async Task<Category?> GetSingleOrDefaultAsync(Expression<Func<Category, bool>> predicate) =>
            await _context.Categories.SingleOrDefaultAsync(predicate);
        public async Task<Category?> GetCategoryByIdAsync(int id)
            => await _context.Categories.FindAsync(id);
        public IQueryable<Category> GetAllCategories(Expression<Func<Category, bool>>? filter = null)
        {
            try
            {
                IQueryable<Category> query = _context.Categories;

                if (filter is not null)
                {
                    query = query.Where(filter);
                }

                return query;
            }
            catch
            {
                throw;
            }
        }
    }
}
