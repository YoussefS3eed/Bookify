namespace Bookify.BLL.Service.Abstraction
{
    public interface IBookService
    {
        Task<Result<BookDTO>> GetByIdAsync(int bookId);
        Task<Result<IEnumerable<BookDTO>>> GetAllAsync();
        Task<Result<BookDTO>> GetByIdWithAuthorAndCategoriesAsync(int id);
        Task<Result<BookDTO>> GetBookWithAuthorAndBookCopyAndBookCategoriesAndCategoryTableAsync(int id);
        Task<(Result<IEnumerable<BookDTO>>, int TotalRecords)> GetBooks(int skip, int pageSize, string? searchValue, string? sortColumn, string? sortColumnDirection);
        Task<Result<BookDTO>> CreateAsync(BookCreateDTO dto);
        Task<Result<BookDTO>> UpdateAsync(BookUpdateDTO dto);
        Task<Result<BookDTO>> ToggleStatusAsync(int bookId, string deletedBy);
        Task<Result<IEnumerable<SelectListItemDTO>>> GetActiveAuthorsForDropdownAsync();
        Task<Result<IEnumerable<SelectListItemDTO>>> GetActiveCategoriesForDropdownAsync();
        Task<bool> IsAllowed(int Id, string Title, int AuthorId);
    }
}