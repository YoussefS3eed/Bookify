namespace Bookify.BLL.Service.Abstraction
{
    public interface IBookCopyService
    {
        Task<Result<BookCopyDto>> CreateAsync(BookCopyCreateDto dto);
        Task<Result<BookCopyDto>> UpdateAsync(BookCopyUpdateDto dto);
        Task<Result<bool>> ToggleStatusAsync(int bookCopyId);
        Task<Result<BookCopyDto?>> GetByIdWithBookIncludesAsync(int id);
        Task<Result<BookDto?>> GetBookByIdAsync(int bookId);
    }
}
