using Bookify.BLL.Common.ResponseResult;
using Bookify.BLL.DTOs.Book;
using Bookify.BLL.DTOs.BookCopy;

namespace Bookify.BLL.Service.Abstraction
{
    public interface IBookCopyService
    {
        Task<Response<BookCopyDTO>> CreateAsync(CreateBookCopyDTO dto);
        Task<Response<BookCopyDTO>> UpdateAsync(UpdateBookCopyDTO dto);
        Task<Response<bool>> ToggleStatusAsync(int bookCopyId);
        Task<Response<BookCopyDTO?>> GetByIdWithBookIncludesAsync(int id);
        Task<Response<BookDTO?>> GetBookByIdAsync(int bookId);
    }
}
