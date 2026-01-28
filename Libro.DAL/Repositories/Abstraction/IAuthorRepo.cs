namespace Libro.DAL.Repositories.Abstraction
{
    public interface IAuthorRepo
    {
        // Command
        Task<Author?> AddAsync(Author author);
        Task<Author?> UpdateAsync(Author author);
        Task<Author?> ToggleStatusAsync(int id);

        // Query
        Task<Author?> GetAuthorByIdAsync(int id);
        IQueryable<Author> GetAllAuthors(Expression<Func<Author, bool>>? filter = null);
        Task<Author?> GetSingleOrDefaultAsync(Expression<Func<Author, bool>> predicate);
        Task<bool> AnyAsync(Expression<Func<Author, bool>> predicate);
    }
}
