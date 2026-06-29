using Bookify.BLL.DTOs.Category;
using Bookify.PL.ViewModels.Category;

namespace Bookify.PL.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoriesController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await _categoryService.GetAllAsync();
            if (result.IsFailure)
                return StatusCode((int)result.Error.StatusCode, result.Error.Message);

            var vm = _mapper.Map<IEnumerable<CategoryViewModel>>(result.Value);
            return View(vm);
        }

        [HttpGet]
        [AjaxOnly]
        public IActionResult Create()
        {
            return PartialView("_Form", new CategoryFormViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryFormViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState[nameof(model.Name)]?.Errors.First().ErrorMessage);

            var dto = _mapper.Map<CategoryCreateDTO>(model);
            var result = await _categoryService.CreateAsync(dto);

            if (result.IsFailure)
                return StatusCode((int)result.Error.StatusCode, result.Error.Message);

            var rowVm = _mapper.Map<CategoryViewModel>(result.Value);
            return PartialView("_CategoryRow", rowVm);
        }

        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> Edit(int id)
        {
            var result = await _categoryService.GetByIdAsync(id);
            if (result.IsFailure)
                return StatusCode((int)result.Error.StatusCode, result.Error.Message);

            var vm = _mapper.Map<CategoryFormViewModel>(result.Value);
            return PartialView("_Form", vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CategoryFormViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState[nameof(model.Name)]?.Errors.First().ErrorMessage);

            var dto = _mapper.Map<CategoryUpdateDTO>(model);
            var result = await _categoryService.UpdateAsync(dto);

            if (result.IsFailure)
                return StatusCode((int)result.Error.StatusCode, result.Error.Message);

            var rowVm = _mapper.Map<CategoryViewModel>(result.Value);
            return PartialView("_CategoryRow", rowVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleStatus(int id)
        {
            var result = await _categoryService.ToggleStatusAsync(id);
            if (result.IsFailure)
                return StatusCode((int)result.Error.StatusCode, result.Error.Message);

            var rowVm = _mapper.Map<CategoryViewModel>(result.Value);
            return Ok(rowVm?.UpdatedOn?.ToString());
        }

        public async Task<IActionResult> AllowItem(CategoryFormViewModel model)
        {
            return Json(await _categoryService.IsAllowed(model.Id, model.Name));
        }
    }
}