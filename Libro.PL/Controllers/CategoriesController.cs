using Libro.BLL.ModelVM.Category;
using Libro.BLL.Service.Abstraction;
using Libro.PL.Filters;
using Microsoft.AspNetCore.Mvc;

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
            // TODO: use viewModel 
            var categories = await _categoryService.GetAllCategoriesAsync();
            return View(categories);
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
                return BadRequest("Invalid data received");
            }
            var category = await _categoryService.CreateCategoryAsync(model);
            return PartialView("_CategoryRow", category?.Result);
        }

        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> Edit(int id)
        {

            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category.Result is null)
                return BadRequest(category.ErrorMessage);


            return PartialView("_Form", category.Result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CategoryFormVM model)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data received");

            
            var category = await _categoryService.UpdateCategoryAsync(model);
            if (category.HasErrorMessage)
            {
                return BadRequest(category.ErrorMessage);
            }

            return PartialView("_CategoryRow", category.Result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleStatus(int id)
        {
            var category = await _categoryService.ToggleStatusCategoryAsync(id);

            
            if (category.HasErrorMessage)
                return BadRequest(category.ErrorMessage);


            return Ok(category.Result!.UpdatedOn?.ToString());
        }
        //public IActionResult AllowItem(CategoryFormVM model)
        //{
        //    var isExist = 
        //}
    }
}
