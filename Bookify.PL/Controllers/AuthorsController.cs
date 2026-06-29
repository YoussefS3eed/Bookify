using Bookify.BLL.Dtos.Author;
using Bookify.PL.ViewModels.Author;

namespace Bookify.PL.Controllers
{
    public class AuthorsController : Controller
    {
        private readonly IAuthorService _authorService;
        private readonly IMapper _mapper;
        public AuthorsController(IAuthorService authorService, IMapper mapper)
        {
            _authorService = authorService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await _authorService.GetAllAsync();
            if (result.IsFailure)
                return StatusCode((int)result.Error.StatusCode, result.Error.Message);

            var vm = _mapper.Map<IEnumerable<AuthorViewModel>>(result.Value);
            return View(vm);
        }

        [HttpGet]
        [AjaxOnly]
        public IActionResult Create()
        {
            return PartialView("_Form", new AuthorFormViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AuthorFormViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState[nameof(model.Name)]?.Errors.First().ErrorMessage);

            var dto = _mapper.Map<CreateAuthorDto>(model);
            var result = await _authorService.CreateAsync(dto);

            if (result.IsFailure)
                return StatusCode((int)result.Error.StatusCode, result.Error.Message);

            var rowVm = _mapper.Map<AuthorViewModel>(result.Value);
            return PartialView("_AuthorRow", rowVm);
        }

        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> Edit(int id)
        {
            var result = await _authorService.GetByIdAsync(id);
            if (result.IsFailure)
                return StatusCode((int)result.Error.StatusCode, result.Error.Message);

            var vm = _mapper.Map<AuthorFormViewModel>(result.Value);
            return PartialView("_Form", vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AuthorFormViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState[nameof(model.Name)]?.Errors.First().ErrorMessage);

            var dto = _mapper.Map<UpdateAuthorDto>(model);
            var result = await _authorService.UpdateAsync(dto);

            if (result.IsFailure)
                return StatusCode((int)result.Error.StatusCode, result.Error.Message);

            var rowVm = _mapper.Map<AuthorViewModel>(result.Value);
            return PartialView("_AuthorRow", rowVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleStatus(int id)
        {
            var result = await _authorService.ToggleStatusAsync(id);
            if (result.IsFailure)
                return StatusCode((int)result.Error.StatusCode, result.Error.Message);
            var rowVm = _mapper.Map<AuthorViewModel>(result.Value);
            return Ok(rowVm?.UpdatedOn?.ToString());
        }

        public async Task<IActionResult> AllowItem(AuthorFormViewModel model)
        {
            return Json(await _authorService.IsAllowed(model.Id, model.Name));
        }
    }
}
