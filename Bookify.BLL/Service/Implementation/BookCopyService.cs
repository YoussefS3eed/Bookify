namespace Bookify.BLL.Service.Implementation
{
    public class BookCopyService : IBookCopyService
    {
        private readonly IBookCopyRepo _bookCopyRepo;
        private readonly IMapper _mapper;
        private readonly ILogger<BookCopyService> _logger;
        public BookCopyService(IBookCopyRepo bookCopyRepo, IMapper mapper, ILogger<BookCopyService> logger)
        {
            _bookCopyRepo = bookCopyRepo;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<Result<BookCopyDto>> CreateAsync(BookCopyCreateDto dto)
        {
            if (dto == null)
                return new Error("BookCopy.InvalidData", "Book copy creation data cannot be null.", HttpStatusCode.BadRequest);

            var book = await _bookCopyRepo.GetBookByIdAsync(dto.BookId);
            if (book == null)
                return new Error("Book.NotFound", $"Associated Book with ID {dto.BookId} was not found.", HttpStatusCode.NotFound);

            var bookCopy = new BookCopy(dto.BookId, dto.EditionNumber, book.IsAvailableForRental && dto.IsAvailableForRental, new ApplicationUser());
            var result = await _bookCopyRepo.AddAsync(bookCopy);

            if (result == null)
                return new Error("BookCopy.CreationFailed", $"Failed to create Book copy for Book '{book.Title}' (ID {dto.BookId}) in database.", HttpStatusCode.InternalServerError);

            return _mapper.Map<BookCopyDto>(result);
        }
        public async Task<Result<BookCopyDto>> UpdateAsync(BookCopyUpdateDto dto)
        {
            if (dto == null)
                return new Error("BookCopy.InvalidData", "Book copy update data cannot be null.", HttpStatusCode.BadRequest);

            var existingBookCopy = await _bookCopyRepo.GetByIdAsync(dto.Id);
            if (existingBookCopy == null)
                return new Error("BookCopy.NotFound", $"Book copy with ID {dto.Id} was not found.", HttpStatusCode.NotFound);

            var bookCopy = _mapper.Map<BookCopy>(dto);
            var result = await _bookCopyRepo.UpdateAsync(bookCopy);

            if (result == null)
                return new Error("BookCopy.UpdateFailed", $"Failed to update Book copy with ID {dto.Id} in database.", HttpStatusCode.BadRequest);

            return _mapper.Map<BookCopyDto>(result);
        }
        public async Task<Result<bool>> ToggleStatusAsync(int bookCopyId)
        {
            var bookCopy = await _bookCopyRepo.GetByIdAsync(bookCopyId);
            if (bookCopy == null)
                return new Error("BookCopy.NotFound", $"Book copy with ID {bookCopyId} was not found.", HttpStatusCode.NotFound);

            var result = await _bookCopyRepo.ToggleStatusAsync(bookCopy.Id);
            if (!result)
                return new Error("BookCopy.ToggleFailed", $"Failed to toggle status for Book copy with ID {bookCopyId} in database.", HttpStatusCode.BadRequest);

            return true;
        }
        public async Task<Result<BookCopyDto>> GetByIdAsync(int bookCopyId)
        {
            var bookCopy = await _bookCopyRepo.GetByIdAsync(bookCopyId);
            if (bookCopy == null)
                return new Error("BookCopy.NotFound", $"Book copy with ID {bookCopyId} was not found.", HttpStatusCode.NotFound);

            return _mapper.Map<BookCopyDto>(bookCopy);
        }
        public async Task<Result<BookDto?>> GetBookByIdAsync(int bookId)
        {
            var book = await _bookCopyRepo.GetBookByIdAsync(bookId);
            if (book == null)
            {
                _logger.LogWarning("Book with id {Id} not found", bookId);
                return new Error("Book.NotFound", $"Book with ID {bookId} was not found.", HttpStatusCode.NotFound);
            }
            return _mapper.Map<BookDto>(book);
        }

        public async Task<Result<BookCopyDto?>> GetByIdWithBookIncludesAsync(int id)
        {
            var bookCopy = await _bookCopyRepo.GetByIdWithBookIncludesAsync(id);
            if (bookCopy == null)
            {
                _logger.LogWarning("Book Copy with id {Id} not found", id);
                return new Error("BookCopy.NotFound", $"Book copy with ID {id} was not found.", HttpStatusCode.NotFound);
            }
            return _mapper.Map<BookCopyDto>(bookCopy);
        }
    }
}
