using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
namespace Bookify.BLL.Service.Implementation
{
    public class BookService : IBookService
    {
        private readonly IBookRepo _bookRepo;
        private readonly IRepository<Author> _authorRepository;
        private readonly IRepository<Category> _categoryRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<BookService> _logger;

        public BookService(
            IBookRepo bookRepository,
            IRepository<Author> authorRepository,
            IRepository<Category> categoryRepository,
            IMapper mapper,
            ILogger<BookService> logger)
        {
            _bookRepo = bookRepository;
            _authorRepository = authorRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<Result<BookDto>> GetByIdAsync(int bookId)
        {
            var book = await _bookRepo.GetByIdAsync(bookId);
            if (book == null)
                return new Error("Book.NotFound", $"Book with ID {bookId} was not found.", HttpStatusCode.NotFound);

            return _mapper.Map<BookDto>(book);
        }
        public async Task<Result<BookDto>> GetByIdWithAuthorAndCategoriesAsync(int id)
        {
            var book = await _bookRepo.GetByIdWithAuthorAndCategoriesAsync(id);
            if (book == null)
                return new Error("Book.NotFound", $"Book with ID {id} was not found.", HttpStatusCode.NotFound);

            return _mapper.Map<BookDto>(book);
        }

        public async Task<Result<BookDto>> GetBookWithAuthorAndBookCopyAndBookCategoriesAndCategoryTableAsync(int id)
        {
            var book = await _bookRepo.GetBookWithAuthorAndBookCopyAndBookCategoriesAndCategoryTableAsync(id);
            if (book == null)
                return new Error("Book.NotFound", $"Book with ID {id} was not found.", HttpStatusCode.NotFound);
            var mapped = _mapper.Map<BookDto>(book);
            return mapped;
        }
        public async Task<(Result<IEnumerable<BookDto>>, int TotalRecords)> GetBooks(int skip, int pageSize, string? searchValue, string? sortColumn, string? sortColumnDirection)
        {
            IQueryable<Book> books = _bookRepo.GetBookWithAuthorAndBookCategoriesAndCategoryTableAsync();
            if (!string.IsNullOrEmpty(searchValue))
                books = books.Where(b => b.Title.Contains(searchValue) || b.Author!.Name.Contains(searchValue));

            var totalRecords = await books.CountAsync();
            books = books.OrderBy($"{sortColumn} {sortColumnDirection}");
            var data = books.Skip(skip).Take(pageSize).ToList();

            var dtos = _mapper.Map<IEnumerable<BookDto>>(data);
            return (Result.Success(dtos), totalRecords);
        }
        public async Task<Result<IEnumerable<BookDto>>> GetAllAsync()
        {
            var books = await _bookRepo.GetAllAsync(b => !b.IsDeleted);
            var dtos = _mapper.Map<IEnumerable<BookDto>>(books);

            return Result.Success(dtos);
        }
        public async Task<Result<BookDto>> CreateAsync(BookCreateDto dto)
        {
            // Check for duplicate book
            var exists = await ExistsByTitleAndAuthorAsync(dto.Title, dto.AuthorId);
            if (exists)
                return new Error("Book.Duplicate", $"Book with title '{dto.Title}' and author ID {dto.AuthorId} already exists.", HttpStatusCode.Conflict);

            var book = _mapper.Map<Book>(dto);

            // Save book
            var createdBook = await _bookRepo.AddAsync(book);
            if (createdBook == null)
                return new Error("Book.CreationFailed", $"Failed to save book '{dto.Title}' in database.", HttpStatusCode.InternalServerError);

            return _mapper.Map<BookDto>(createdBook);
        }
        public async Task<Result<BookDto>> UpdateAsync(BookUpdateDto dto)
        {
            // Validate Author
            var author = await _authorRepository.GetByIdAsync(dto.AuthorId);
            if (author == null || author.IsDeleted)
                return new Error("Book.AuthorNotFound", $"Author with ID {dto.AuthorId} was not found or is deleted.", HttpStatusCode.BadRequest);

            // Call Repository UpdateAsync
            var updatedBook = await _bookRepo.UpdateAsync(_mapper.Map<Book>(dto), dto.CategoryIds);
            if (updatedBook == null)
                return new Error("Book.UpdateFailed", $"Failed to update book '{dto.Title}' in database.", HttpStatusCode.BadRequest);

            var resultDto = _mapper.Map<BookDto>(updatedBook);
            return resultDto;
        }
        public async Task<Result<BookDto>> ToggleStatusAsync(int bookId, string deletedBy)
        {
            var book = await _bookRepo.GetByIdAsync(bookId);
            if (book == null)
                return new Error("Book.NotFound", $"Book with ID {bookId} was not found.", HttpStatusCode.NotFound);

            var result = await _bookRepo.ToggleStatusAsync(book.Id, deletedBy);
            if (result == null)
                return new Error("Book.ToggleFailed", $"Failed to toggle status for Book with ID {bookId} in database.", HttpStatusCode.BadRequest);

            return _mapper.Map<BookDto>(result);
        }
        public async Task<Result<IEnumerable<SelectListItemDto>>> GetActiveAuthorsForDropdownAsync()
        {
            var authors = await _authorRepository.GetAllAsync(a => !a.IsDeleted);
            var dtos = _mapper.Map<IEnumerable<SelectListItemDto>>(authors.OrderBy(a => a.Name));
            return Result.Success(dtos);
        }
        public async Task<Result<IEnumerable<SelectListItemDto>>> GetActiveCategoriesForDropdownAsync()
        {
            var categories = await _categoryRepository.GetAllAsync(c => !c.IsDeleted);
            var dtos = _mapper.Map<IEnumerable<SelectListItemDto>>(categories.OrderBy(c => c.Name));
            return Result.Success(dtos);
        }
        public async Task<bool> IsAllowed(int Id, string Title, int AuthorId)
        {
            var book = await _bookRepo.GetSingleOrDefaultAsync(b => b.Title == Title && b.AuthorId == AuthorId);
            return book is null || book.Id.Equals(Id);
        }
        private async Task<bool> ExistsByTitleAndAuthorAsync(string title, int authorId)
        => await _bookRepo.AnyAsync(b => b.Title == title && b.AuthorId == authorId && !b.IsDeleted);
    }
}