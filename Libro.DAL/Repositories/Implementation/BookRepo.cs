namespace Libro.DAL.Repositories.Implementation
{
    public class BookRepo : Repository<Book>, IBookRepo
    {
        private readonly LibroDbContext _context;
        public BookRepo(LibroDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Book?> UpdateAsync(Book book1, List<int?> categoryIds)
        {
            // 1️⃣ جلب الـ Entity
            var book = await _context.Books
                .Include(b => b.Categories)
                .SingleOrDefaultAsync(b => b.Id == book1.Id);

            if (book == null || book.IsDeleted)
                return null;

            // 2️⃣ تعديل باستخدام Entity logic
            book.Update(book1.Title, book1.AuthorId, book1.Publisher, book1.PublishingDate, book1.Hall,
                        book1.IsAvailableForRental, book1.Description, book1.ImageUrl, book1.ImageThumbnailUrl, book1.ImagePublicId, book1.UpdatedBy!, categoryIds);

            // 3️⃣ حفظ التغييرات
            var success = await _context.SaveChangesAsync();
            return success > 0 ? book : null;
        }

        public async Task<Book?> ToggleStatusAsync(int id, string deletedBy)
        {
            var book = await GetByIdAsync(id);
            book!.ToggleStatus(deletedBy);
            if (await SaveChangesAsync())
                return book;
            return null;
        }

        public async Task<Book?> GetByIdWithCategoriesAsync(int id)
        {
            return await _context.Books
                .Include(b => b.Categories)
                .SingleOrDefaultAsync(b => b.Id == id);
        }

        public async Task<Book?> GetByIdWithAuthorAndCategoriesAsync(int id)
        {
            return await _context.Books
                .Include(b => b.Author)
                .Include(b => b.Categories)
                .SingleOrDefaultAsync(b => b.Id == id);
        }

        public IQueryable<Book> GetBookWithAuthorAndBookCategoriesAndCategoryTableAsync()
        {
            return _context.Books
                .Include(b => b.Author)
                .Include(b => b.Categories)
                .ThenInclude(c => c.Category);
        }
        public async Task<Book?> GetBookWithAuthorAndBookCopyAndBookCategoriesAndCategoryTableAsync(int id)
        {
            return await _context.Books
                .Include(b => b.Author)
                .Include(b => b.Copies)
                .Include(b => b.Categories)
                .ThenInclude(c => c.Category)
                .SingleOrDefaultAsync(b => b.Id == id);
        }

        public async Task<IEnumerable<Book>> GetByAuthorIdAsync(int authorId)
        {
            return await _context.Books
                .Where(b => b.AuthorId == authorId && !b.IsDeleted)
                .ToListAsync();
        }
    }
}