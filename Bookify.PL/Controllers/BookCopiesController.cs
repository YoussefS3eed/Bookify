using Bookify.BLL.DTOs.BookCopy;
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

            if (book.Result is null)
                return NotFound();

            var viewModel = new BookCopyFormViewModel
            {
                BookId = bookId,
                ShowRentalInput = book.Result.IsAvailableForRental
            };

            return PartialView("Form", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BookCopyFormViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState[nameof(model.BookId)]?.Errors.First().ErrorMessage);

            var dto = _mapper.Map<BookCopyCreateDTO>(model);
            var result = await _bookCopyService.CreateAsync(dto);

            if (result.HasErrorMessage)
                return StatusCode((int)result.StatusCode, result.ErrorMessage);

            var viewModel = _mapper.Map<BookCopyViewModel>(result.Result);
            return PartialView("_BookCopyRow", viewModel);
        }
        [AjaxOnly]
        public async Task<IActionResult> Edit(int id)
        {
            var result = await _bookCopyService.GetByIdWithBookIncludesAsync(id);
            if (result.HasErrorMessage)
                return StatusCode((int)result.StatusCode, result.ErrorMessage);

            var viewModel = _mapper.Map<BookCopyFormViewModel>(result.Result);
            viewModel.ShowRentalInput = result.Result!.Book.IsAvailableForRental;

            return PartialView("Form", viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(BookCopyFormViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState[nameof(model.BookId)]?.Errors.First().ErrorMessage);

            var dto = _mapper.Map<BookCopyUpdateDTO>(model);
            var result = await _bookCopyService.UpdateAsync(dto);

            if (result.HasErrorMessage)
                return StatusCode((int)result.StatusCode, result.ErrorMessage);

            var viewModel = _mapper.Map<BookCopyViewModel>(result.Result);
            return PartialView("_BookCopyRow", viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleStatus(int id)
        {
            var result = await _bookCopyService.ToggleStatusAsync(id);
            if (result.HasErrorMessage)
                return StatusCode((int)result.StatusCode, result.ErrorMessage);
            return Ok(result.Result);
        }
    }
}
