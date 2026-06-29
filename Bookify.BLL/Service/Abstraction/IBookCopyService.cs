namespace Bookify.BLL.Service.Abstraction
{
    public interface IBookCopyService
    {
        Task<Result<BookCopyDTO>> CreateAsync(BookCopyCreateDTO dto);
        Task<Result<BookCopyDTO>> UpdateAsync(BookCopyUpdateDTO dto);
        Task<Result<bool>> ToggleStatusAsync(int bookCopyId);
        Task<Result<BookCopyDTO?>> GetByIdWithBookIncludesAsync(int id);
        Task<Result<BookDTO?>> GetBookByIdAsync(int bookId);
    }
}
