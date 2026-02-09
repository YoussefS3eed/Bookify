using Bookify.DAL.Entities;

namespace Bookify.DAL.Repositories.Abstraction
{
    public interface IBookCopyRepo : IRepository<BookCopy>
    {
        Task<BookCopy?> GetByIdWithBookIncludesAsync(int id);
        Task<BookCopy?> UpdateAsync(BookCopy newBookCopy);
        Task<bool> ToggleStatusAsync(int id);
        Task<Book?> GetBookByIdAsync(int bookId);
    }
}
