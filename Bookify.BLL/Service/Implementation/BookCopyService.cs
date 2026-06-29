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
        public async Task<Result<BookCopyDTO>> CreateAsync(BookCopyCreateDTO dto)
        {
            if (dto == null)
                return new Error("BookCopy.InvalidData", "Invalid data.", HttpStatusCode.BadRequest);

            var book = await _bookCopyRepo.GetBookByIdAsync(dto.BookId);
            var bookCopy = new BookCopy(dto.BookId, dto.EditionNumber, book!.IsAvailableForRental && dto.IsAvailableForRental, new ApplicationUser());
            var result = await _bookCopyRepo.AddAsync(bookCopy);

            if (result == null)
                return new Error("BookCopy.CreationFailed", "Failed to create bookCopy in database.", HttpStatusCode.InternalServerError);

            return _mapper.Map<BookCopyDTO>(result);
        }
        public async Task<Result<BookCopyDTO>> UpdateAsync(BookCopyUpdateDTO dto)
        {
            if (dto == null)
                return new Error("BookCopy.InvalidData", "Invalid data.", HttpStatusCode.BadRequest);

            var existingBookCopy = await _bookCopyRepo.GetByIdAsync(dto.Id);
            if (existingBookCopy == null)
                return new Error("BookCopy.NotFound", "BookCopy not found.", HttpStatusCode.NotFound);

            var bookCopy = _mapper.Map<BookCopy>(dto);
            var result = await _bookCopyRepo.UpdateAsync(bookCopy);

            if (result == null)
                return new Error("BookCopy.UpdateFailed", "Database error.", HttpStatusCode.BadRequest);

            return _mapper.Map<BookCopyDTO>(result);
        }
        public async Task<Result<bool>> ToggleStatusAsync(int bookCopyId)
        {
            var bookCopy = await _bookCopyRepo.GetByIdAsync(bookCopyId);
            if (bookCopy == null)
                return new Error("BookCopy.NotFound", "BookCopy not found.", HttpStatusCode.NotFound);

            var result = await _bookCopyRepo.ToggleStatusAsync(bookCopy.Id);
            if (!result)
                return new Error("BookCopy.ToggleFailed", "Database error.", HttpStatusCode.BadRequest);

            return true;
        }
        public async Task<Result<BookCopyDTO>> GetByIdAsync(int bookCopyId)
        {
            var bookCopy = await _bookCopyRepo.GetByIdAsync(bookCopyId);
            if (bookCopy == null)
                return new Error("BookCopy.NotFound", "BookCopy not found.", HttpStatusCode.NotFound);

            return _mapper.Map<BookCopyDTO>(bookCopy);
        }
        public async Task<Result<BookDTO?>> GetBookByIdAsync(int bookId)
        {
            var book = await _bookCopyRepo.GetBookByIdAsync(bookId);
            if (book == null)
            {
                _logger.LogWarning("Book with id {Id} not found", bookId);
                return new Error("Book.NotFound", "Book not found.", HttpStatusCode.NotFound);
            }
            return _mapper.Map<BookDTO>(book);
        }

        public async Task<Result<BookCopyDTO?>> GetByIdWithBookIncludesAsync(int id)
        {
            var bookCopy = await _bookCopyRepo.GetByIdWithBookIncludesAsync(id);
            if (bookCopy == null)
            {
                _logger.LogWarning("Book Copy with id {Id} not found", id);
                return new Error("BookCopy.NotFound", "Book Copy not found.", HttpStatusCode.NotFound);
            }
            return _mapper.Map<BookCopyDTO>(bookCopy);
        }
    }
}
