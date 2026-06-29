namespace Bookify.BLL.Service.Abstraction
{
    public interface IBookService
    {
        Task<Result<BookDto>> GetByIdAsync(int bookId);
        Task<Result<IEnumerable<BookDto>>> GetAllAsync();
        Task<Result<BookDto>> GetByIdWithAuthorAndCategoriesAsync(int id);
        Task<Result<BookDto>> GetBookWithAuthorAndBookCopyAndBookCategoriesAndCategoryTableAsync(int id);
        Task<(Result<IEnumerable<BookDto>>, int TotalRecords)> GetBooks(int skip, int pageSize, string? searchValue, string? sortColumn, string? sortColumnDirection);
        Task<Result<BookDto>> CreateAsync(BookCreateDto dto);
        Task<Result<BookDto>> UpdateAsync(BookUpdateDto dto);
        Task<Result<BookDto>> ToggleStatusAsync(int bookId, string deletedBy);
        Task<Result<IEnumerable<SelectListItemDto>>> GetActiveAuthorsForDropdownAsync();
        Task<Result<IEnumerable<SelectListItemDto>>> GetActiveCategoriesForDropdownAsync();
        Task<bool> IsAllowed(int Id, string Title, int AuthorId);
    }
}