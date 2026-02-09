namespace Libro.BLL.Service.Abstraction
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
