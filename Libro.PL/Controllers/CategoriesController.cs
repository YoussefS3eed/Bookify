namespace Libro.PL.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            if (categories.HasErrorMessage)
            {
                return StatusCode((int)categories.StatusCode, categories.ErrorMessage);
            }

            return View(categories.Result);
        }

        [HttpGet]
        [AjaxOnly]
        public IActionResult Create()
        {
            return PartialView("_Form");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryFormVM model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState[nameof(model.Name)]?.Errors[0].ErrorMessage);
            }

            var category = await _categoryService.CreateCategoryAsync(model);
            if (category.HasErrorMessage)
            {
                return StatusCode((int)category.StatusCode, category.ErrorMessage);
            }

            return PartialView("_CategoryRow", category?.Result);
        }

        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> Edit(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category.HasErrorMessage)
            {
                return StatusCode((int)category.StatusCode, category.ErrorMessage);
            }

            return PartialView("_Form", category.Result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CategoryFormVM model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState[nameof(model.Name)]?.Errors[0].ErrorMessage);
            }

            var category = await _categoryService.UpdateCategoryAsync(model);
            if (category.HasErrorMessage)
            {
                return StatusCode((int)category.StatusCode, category.ErrorMessage);
            }

            return PartialView("_CategoryRow", category.Result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleStatus(int id)
        {
            var category = await _categoryService.ToggleStatusCategoryAsync(id);
            if (category.HasErrorMessage)
            {
                return StatusCode((int)category.StatusCode, category.ErrorMessage);
            }

            return Ok(category.Result!.UpdatedOn?.ToString());
        }
    }
}
