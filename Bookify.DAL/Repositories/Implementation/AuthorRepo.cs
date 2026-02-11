namespace Bookify.DAL.Repositories.Implementation
{
    public class AuthorRepo : Repository<Author>, IAuthorRepo
    {
        private readonly BookifyDbContext _context;
        public AuthorRepo(BookifyDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<Author?> UpdateAsync(Author newAuthor)
        {

            var updatedAuthor = await GetByIdAsync(newAuthor.Id);
            if (updatedAuthor is not null)
            {
                var isUpdated = updatedAuthor.Update(newAuthor.Name, newAuthor.LastUpdatedById! ?? "System");
                if (isUpdated)
                {
                    if (await SaveChangesAsync())
                        return updatedAuthor;
                }
            }
            return null;
        }
        public async Task<Author?> ToggleStatusAsync(int id)
        {
            var author = await GetByIdAsync(id);
            author!.ToggleStatus("System");
            if (await SaveChangesAsync())
                return author;
            return null;
        }
    }
}