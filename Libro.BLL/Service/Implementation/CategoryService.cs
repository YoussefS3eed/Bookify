using AutoMapper;
using Libro.BLL.ModelVM.Category;
using Libro.BLL.ModelVM.ViewModel;
using Libro.DAL.Entities;
using Libro.DAL.Repo.Abstraction;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Libro.BLL.Service.Implementation
{
    public class CategoryService : ICategoryService
    {
        public readonly ICategoryRepo _categoryRepo;
        private readonly IMapper _mapper;
        private readonly ILogger<CategoryService> _logger;

        public CategoryService(ICategoryRepo categoryRepo, IMapper mapper, ILogger<CategoryService> logger)
        {
            _categoryRepo = categoryRepo;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<Response<CategoryViewModel>> CreateCategoryAsync(CategoryFormVM model)
        {
            try
            {
                if (model is not null)
                {
                    var mappingCategory = _mapper.Map<Category>(model);
                    var newCategory = await _categoryRepo.AddAsync(mappingCategory);
                    if (newCategory is null)
                    {
                        return new(null, "Not Found Your Category", true);
                    }
                    var newCategoryViewModel = _mapper.Map<CategoryViewModel>(newCategory);
                    return new(newCategoryViewModel, null, false);
                }
                return new(null, "You don't enter a write values", true);
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while adding the category {Name} with error massage: {ErrorMassage}", model?.Name, ex.Message);
                return new(null, "An error occurred while adding the category.", true);
            }
        }

        public async Task<Response<CategoryViewModel>> UpdateCategoryAsync(CategoryFormVM model)
        {
            try
            {
                if (model is not null)
                {
                    var mappingCategory = _mapper.Map<Category>(model);
                    var updateCategory = await _categoryRepo.UpdateAsync(mappingCategory);
                    if (updateCategory is null)
                    {
                        return new(null, "You must change your name", true);
                    }

                    var updatedCategoryViewModel = _mapper.Map<CategoryViewModel>(updateCategory);
                    return new(updatedCategoryViewModel, null, false);
                }
                return new(null, null, false);
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while updating the category {Name} with error massage: {ErrorMassage}", model?.Name, ex.Message);
                return new(null, "An error occurred while updating the category.", true);
            }
        }
        public async Task<Response<CategoryViewModel>> ToggleStatusCategoryAsync(int categoryId)
        {
            try
            {
                //var category = await _categoryRepo.GetCategoryByIdAsync(categoryId);
                var category = await _categoryRepo.ToggleStatusAsync(categoryId);
                if (category is null)
                {
                    return new(null, "Not Found Your Category", true);
                }
                var categoryViewModel = _mapper.Map<CategoryViewModel>(category);
                return new(categoryViewModel, null, false);
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while toggling the category status with id {ID} with error massage: {ErrorMassage}", categoryId, ex.Message);
                return new(null, "An error occurred while toggling the category status.", true);
            }
        }

        public async Task<bool> NameExistsAsync(string name)
        {
            return await _categoryRepo.AnyAsync(c => c.Name == name);
        }
        public async Task<Response<CategoryFormVM>> GetCategoryByIdAsync(int categoryId)
        {
            try
            {
                var category = await _categoryRepo.GetCategoryByIdAsync(categoryId);
                if (category is null)
                {
                    return new(null, "Category not found.", true);
                }
                return new(_mapper.Map<CategoryFormVM>(category), null, false);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error while fetching category by id {ID}: {Message}", categoryId, ex.Message);
                return new(null, "Could not load category.", true);
            }
        }
        public async Task<Response<IEnumerable<CategoryViewModel>>> GetAllCategoriesAsync()
        {
            try
            {
                var categories = await _categoryRepo.GetAllCategories().AsNoTracking().ToListAsync();
                var mappedCategories = _mapper.Map<IEnumerable<CategoryViewModel>>(categories);
                return new(mappedCategories, null, false);

            }
            catch (Exception ex)
            {
                _logger.LogError("Error while fetching active categories: {Message}", ex.Message);
                return new(null, "Could not load categories.", true);
            }
        }

        public async Task<Response<IEnumerable<CategoryViewModel>>> GetAllNotActiveCategoriesAsync()
        {
            try
            {
                var categories = await _categoryRepo.GetAllCategories(x => x.IsDeleted).AsNoTracking().ToListAsync();
                var mappedCategories = _mapper.Map<IEnumerable<CategoryViewModel>>(categories);
                return new(mappedCategories, null, false);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error while fetching inactive categories: {Message}", ex.Message);
                return new(null, "Could not load categories.", true);
            }
        }
    }
}

