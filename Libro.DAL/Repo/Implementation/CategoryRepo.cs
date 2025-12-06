
using Libro.DAL.Database;
using System.Linq.Expressions;

namespace Libro.DAL.Repo.Implementation
{
    public class CategoryRepo : ICategoryRepo
    {
        private readonly LibroDbContext _context;

        public CategoryRepo(LibroDbContext context)
        {
            _context = context;
        }

        public async Task<Category?> AddAsync(Category category)
        {
            try
            {
                if (category is null)
                {
                    return null;
                }
                await _context.Categories.AddAsync(category);
                await _context.SaveChangesAsync();
                return category;
            }
            catch
            {
                //throw new ApplicationException("An error occurred while adding the category.");
                throw;
            }
        }

        public async Task<Category?> UpdateAsync(Category newCategory)
        {
            try
            {
                if (newCategory is null)
                {
                    throw new ArgumentNullException(nameof(newCategory));
                }

                var updatedCategory = await _context.Categories.FindAsync(newCategory.Id);
                if (updatedCategory is not null)
                {
                    var isUpdated = updatedCategory.Update(newCategory.Name, newCategory.UpdatedBy! ?? "System");
                    if (isUpdated)
                    {
                        await _context.SaveChangesAsync();
                        return updatedCategory;
                    }
                }
                return null;
            }
            catch
            {
                //throw new ApplicationException($"An error occurred while updating the category {newCategory?.Name}.");
                throw;
            }
        }

        public async Task<Category?> ToggleStatusAsync(int id)
        {
            try
            {
                var category = await _context.Categories.FindAsync(id);
                if (category is null)
                {
                    return null;
                }

                category.ToggleStatus("System");
                await _context.SaveChangesAsync();
                return category;
            }
            catch
            {
                //throw new ApplicationException($"An error occurred while toggling the category status.");
                throw;
            }
        }
        public async Task<bool> AnyAsync(Expression<Func<Category, bool>> predicate)
            => await _context.Categories.AnyAsync(predicate);
        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            try
            {
                var category = await _context.Categories.FindAsync(id);
                if (category is null)
                {
                    return null!;
                }
                return category;
            }
            catch
            {
                throw;
            }
        }

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
                //throw new ApplicationException("An error occurred while retrieving the categories.");
                throw;
            }
        }


    }
}
