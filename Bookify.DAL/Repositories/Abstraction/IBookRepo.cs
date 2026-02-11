namespace Bookify.DAL.Repositories.Abstraction
{
    public interface IBookRepo : IRepository<Book>
    {
        Task<Book?> UpdateAsync(Book book, List<int?> categoryIds);
        Task<Book?> ToggleStatusAsync(int id, string deletedBy);
        Task<Book?> GetByIdWithCategoriesAsync(int id);
        Task<Book?> GetByIdWithAuthorAndCategoriesAsync(int id);
        IQueryable<Book> GetBookWithAuthorAndBookCategoriesAndCategoryTableAsync();
        Task<Book?> GetBookWithAuthorAndBookCopyAndBookCategoriesAndCategoryTableAsync(int id);
        Task<IEnumerable<Book>> GetByAuthorIdAsync(int authorId);
    }
}