using Bookify.BLL.Common.ResponseResult;
using Bookify.BLL.DTOs.Book;
using Bookify.BLL.DTOs.BookCopy;
using Bookify.BLL.Service.Abstraction;
using Bookify.DAL.Entities;
using Bookify.DAL.Repositories.Abstraction;

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
        public async Task<Response<BookCopyDTO>> CreateAsync(CreateBookCopyDTO dto)
        {
            try
            {
                if (dto == null)
                    return new(null, "Invalid data.", true, HttpStatusCode.BadRequest);

                var book = await _bookCopyRepo.GetBookByIdAsync(dto.BookId);
                var bookCopy = new BookCopy(dto.BookId, dto.EditionNumber, book!.IsAvailableForRental && dto.IsAvailableForRental, dto.CreatedBy ?? "System");
                var result = await _bookCopyRepo.AddAsync(bookCopy);

                if (result == null)
                    return new(null, "Failed to create bookCopy in database.", true, HttpStatusCode.InternalServerError);

                return new(_mapper.Map<BookCopyDTO>(result), null, false);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating bookCopy with bookId {bookId}", dto?.BookId);
                return new(null, "Unexpected error.", true, HttpStatusCode.InternalServerError);
            }
        }
        public async Task<Response<BookCopyDTO>> UpdateAsync(UpdateBookCopyDTO dto)
        {
            try
            {
                if (dto == null)
                    return new(null, "Invalid data.", true, HttpStatusCode.BadRequest);

                var existingBookCopy = await _bookCopyRepo.GetByIdAsync(dto.Id);
                if (existingBookCopy == null)
                    return new(null, "BookCopy not found.", true, HttpStatusCode.NotFound);


                var bookCopy = _mapper.Map<BookCopy>(dto);
                var result = await _bookCopyRepo.UpdateAsync(bookCopy);

                if (result == null)
                    return new(null, "Database error.", true, HttpStatusCode.BadRequest);

                return new(_mapper.Map<BookCopyDTO>(result), null, false);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating bookCopy with Id {Id}", dto?.Id);
                return new(null, "Unexpected error.", true, HttpStatusCode.InternalServerError);
            }
        }
        public async Task<Response<bool>> ToggleStatusAsync(int bookCopyId)
        {
            try
            {

                var bookCopy = await _bookCopyRepo.GetByIdAsync(bookCopyId);
                if (bookCopy == null)
                    return new(false, "BookCopy not found.", true, HttpStatusCode.NotFound);

                var result = await _bookCopyRepo.ToggleStatusAsync(bookCopy.Id);
                if (!result)
                    return new(false, "Database error.", true, HttpStatusCode.BadRequest);

                return new(true, null, false);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error toggling status for bookCopy {Id}", bookCopyId);
                return new(false, "Unexpected error.", true, HttpStatusCode.InternalServerError);
            }
        }
        public async Task<Response<BookCopyDTO>> GetByIdAsync(int bookCopyId)
        {
            try
            {
                var bookCopy = await _bookCopyRepo.GetByIdAsync(bookCopyId);
                if (bookCopy == null)
                    return new(null, "BookCopy not found.", true, HttpStatusCode.NotFound);

                return new(_mapper.Map<BookCopyDTO>(bookCopy), null, false);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching bookCopy with id {Id}", bookCopyId);
                return new(null, "Could not load bookCopy.", true, HttpStatusCode.InternalServerError);
            }
        }
        public async Task<Response<BookDTO?>> GetBookByIdAsync(int bookId)
        {
            try
            {
                var book = await _bookCopyRepo.GetBookByIdAsync(bookId);
                if (book == null)
                {
                    _logger.LogWarning("Book with id {Id} not found", bookId);
                    return new(null, "Book not found.", true, HttpStatusCode.NotFound);
                }
                return new(_mapper.Map<BookDTO>(book), null, false);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching book with id {Id}", bookId);
                return new(null, "Could not load book.", true, HttpStatusCode.InternalServerError);
            }
        }

        public async Task<Response<BookCopyDTO?>> GetByIdWithBookIncludesAsync(int id)
        {
            try
            {
                var bookCopy = await _bookCopyRepo.GetByIdWithBookIncludesAsync(id);
                if (bookCopy == null)
                {
                    _logger.LogWarning("Book Copy with id {Id} not found", id);
                    return new(null, "Book Copy not found.", true, HttpStatusCode.NotFound);
                }
                var mapped = _mapper.Map<BookCopyDTO>(bookCopy);
                return new(_mapper.Map<BookCopyDTO>(bookCopy), null, false);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching book with id {Id}", id);
                return new(null, "Could not load book.", true, HttpStatusCode.InternalServerError);
            }
        }
    }
}
