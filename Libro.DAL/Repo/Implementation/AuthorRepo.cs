namespace Libro.DAL.Repo.Implementation
{
    public class AuthorRepo : IAuthorRepo
    {
        private readonly LibroDbContext _context;

        public AuthorRepo(LibroDbContext context)
        {
            _context = context;
        }
        public async Task<Author?> AddAsync(Author newAuthor)
        {
            try
            {
                await _context.Authors.AddAsync(newAuthor);
                if (await _context.SaveChangesAsync() > 0)
                    return newAuthor;
                return null;
            }
            catch
            {
                throw;
            }
        }
        public async Task<Author?> UpdateAsync(Author newAuthor)
        {
            try
            {
                var isUpdated = newAuthor.Update(newAuthor.Name, newAuthor.UpdatedBy! ?? "System Author from Update");
                if (isUpdated)
                {
                    if (await _context.SaveChangesAsync() > 0)
                        return newAuthor;
                }
                return null;
            }
            catch
            {
                throw;
            }
        }
        public async Task<Author?> ToggleStatusAsync(int id)
        {
            try
            {
                var Author = await GetAuthorByIdAsync(id);
                if (Author is null)
                    return null;

                Author.ToggleStatus("System from Author Toggle Status");
                await _context.SaveChangesAsync();
                return Author;
            }
            catch
            {
                throw;
            }
        }
        public async Task<bool> AnyAsync(Expression<Func<Author, bool>> predicate)
            => await _context.Authors.AnyAsync(predicate);
        public async Task<Author?> GetAuthorByIdAsync(int id)
            => await _context.Authors.FindAsync(id);
        public IQueryable<Author> GetAllAuthors(Expression<Func<Author, bool>>? filter = null)
        {
            try
            {
                IQueryable<Author> query = _context.Authors;

                if (filter is not null)
                    query = query.Where(filter);

                return query;
            }
            catch
            {
                throw;
            }
        }
    }
}
