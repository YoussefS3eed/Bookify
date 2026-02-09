using Bookify.DAL.Database;
using Bookify.DAL.Entities;
using Bookify.DAL.Repositories.Abstraction;

namespace Bookify.DAL.Repositories.Implementation
{
    public class BookCopyRepo : Repository<BookCopy>, IBookCopyRepo
    {
        private readonly LibroDbContext _context;
        public BookCopyRepo(LibroDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<BookCopy?> UpdateAsync(BookCopy newBookCopy)
        {
            var updatedBookCopy = await GetByIdAsync(newBookCopy.Id);
            if (updatedBookCopy is not null)
            {
                var copy = await GetByIdWithBookIncludesAsync(newBookCopy.Id);
                updatedBookCopy.Update(copy!.Book!.IsAvailableForRental && newBookCopy.IsAvailableForRental, newBookCopy.EditionNumber, newBookCopy.UpdatedBy!);

                if (await SaveChangesAsync())
                    return updatedBookCopy;
            }
            return null;
        }
        public async Task<bool> ToggleStatusAsync(int id)
        {
            var bookCopy = await GetByIdAsync(id);
            bookCopy!.ToggleStatus("System");
            return await SaveChangesAsync();
        }
        public async Task<BookCopy?> GetByIdWithBookIncludesAsync(int id)
        {
            return await _context.BookCopies
                .Include(bc => bc.Book)
                .FirstOrDefaultAsync(bc => bc.Id == id);
        }
        public async Task<Book?> GetBookByIdAsync(int bookId)
        {
            return await _context.Books.FindAsync(bookId);
        }
    }
}
