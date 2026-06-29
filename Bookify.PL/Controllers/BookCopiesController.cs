using Bookify.BLL.Dtos.BookCopy;
using Bookify.PL.ViewModels.BookCopy;

namespace Bookify.PL.Controllers
{
    public class BookCopiesController : Controller
    {
        private readonly IBookCopyService _bookCopyService;
        private readonly IMapper _mapper;
        public BookCopiesController(IBookCopyService bookCopyService, IMapper mapper)
        {
            _bookCopyService = bookCopyService;
            _mapper = mapper;
        }

        [AjaxOnly]
        public async Task<IActionResult> Create(int bookId)
        {
            var book = await _bookCopyService.GetBookByIdAsync(bookId);

            if (book.IsFailure || book.Value is null)
                return NotFound();

            var viewModel = new BookCopyFormViewModel
            {
                BookId = bookId,
                ShowRentalInput = book.Value.IsAvailableForRental
            };

            return PartialView("Form", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BookCopyFormViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState[nameof(model.BookId)]?.Errors.First().ErrorMessage);

            var dto = _mapper.Map<BookCopyCreateDto>(model);
            var result = await _bookCopyService.CreateAsync(dto);

            if (result.IsFailure)
                return StatusCode((int)result.Error.StatusCode, result.Error.Message);

            var viewModel = _mapper.Map<BookCopyViewModel>(result.Value);
            return PartialView("_BookCopyRow", viewModel);
        }

        [AjaxOnly]
        public async Task<IActionResult> Edit(int id)
        {
            var result = await _bookCopyService.GetByIdWithBookIncludesAsync(id);
            if (result.IsFailure)
                return StatusCode((int)result.Error.StatusCode, result.Error.Message);

            var viewModel = _mapper.Map<BookCopyFormViewModel>(result.Value);
            viewModel.ShowRentalInput = result.Value!.Book.IsAvailableForRental;

            return PartialView("Form", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(BookCopyFormViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState[nameof(model.BookId)]?.Errors.First().ErrorMessage);

            var dto = _mapper.Map<BookCopyUpdateDto>(model);
            var result = await _bookCopyService.UpdateAsync(dto);

            if (result.IsFailure)
                return StatusCode((int)result.Error.StatusCode, result.Error.Message);

            var viewModel = _mapper.Map<BookCopyViewModel>(result.Value);
            return PartialView("_BookCopyRow", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleStatus(int id)
        {
            var result = await _bookCopyService.ToggleStatusAsync(id);
            if (result.IsFailure)
                return StatusCode((int)result.Error.StatusCode, result.Error.Message);
            return Ok(result.Value);
        }
    }
}
