namespace Libro.DAL.Repo.Abstraction
{
    public interface IAuthorRepo
    {
        // Command
        Task<Author?> AddAsync(Author author);
        Task<Author?> UpdateAsync(Author author);
        Task<Author?> ToggleStatusAsync(int id);
        Task<bool> AnyAsync(Expression<Func<Author, bool>> predicate);
        // Query
        Task<Author?> GetAuthorByIdAsync(int id);
        IQueryable<Author> GetAllAuthors(Expression<Func<Author, bool>>? filter = null);
    }
}
