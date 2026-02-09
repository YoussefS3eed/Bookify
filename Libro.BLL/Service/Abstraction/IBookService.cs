namespace Libro.BLL.Service.Abstraction
{
    public interface IBookService
    {
        Task<Response<BookDTO>> GetByIdAsync(int bookId);
        Task<Response<IEnumerable<BookDTO>>> GetAllAsync();
        Task<Response<BookDTO>> GetByIdWithAuthorAndCategoriesAsync(int id);
        Task<Response<BookDTO>> GetBookWithAuthorAndBookCopyAndBookCategoriesAndCategoryTableAsync(int id);
        Task<(Response<IEnumerable<BookDTO>>, int TotalRecords)> GetBooks(int skip, int pageSize, string? searchValue, string? sortColumn, string? sortColumnDirection);
        Task<Response<BookDTO>> CreateAsync(CreateBookDTO dto);
        Task<Response<BookDTO>> UpdateAsync(UpdateBookDTO dto);
        Task<Response<BookDTO>> ToggleStatusAsync(int bookId, string deletedBy);
        Task<Response<IEnumerable<SelectListItemDTO>>> GetActiveAuthorsForDropdownAsync();
        Task<Response<IEnumerable<SelectListItemDTO>>> GetActiveCategoriesForDropdownAsync();
        Task<bool> IsAllowed(int Id, string Title, int AuthorId);
    }
}