namespace Bookify.BLL.Service.Abstraction
{
    public interface IBookCopyService
    {
        Task<Response<BookCopyDTO>> CreateAsync(BookCopyCreateDTO dto);
        Task<Response<BookCopyDTO>> UpdateAsync(BookCopyUpdateDTO dto);
        Task<Response<bool>> ToggleStatusAsync(int bookCopyId);
        Task<Response<BookCopyDTO?>> GetByIdWithBookIncludesAsync(int id);
        Task<Response<BookDTO?>> GetBookByIdAsync(int bookId);
    }
}
