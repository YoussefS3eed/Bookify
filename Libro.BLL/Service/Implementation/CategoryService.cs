using System.Net;

namespace Libro.BLL.Service.Implementation
{
    public class CategoryService : ICategoryService, IUniqueNameValidator
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
                if (model is null)
                    return new(null, "You don't enter a write values when you creating category are you atker!!", true, HttpStatusCode.NotAcceptable);

                var mappingCategory = _mapper.Map<Category>(model);
                var newCategory = await _categoryRepo.AddAsync(mappingCategory);
                if (newCategory is null)
                {
                    return new(null, "something went wrong when we adding in database", true, HttpStatusCode.InternalServerError);
                }
                var newCategoryViewModel = _mapper.Map<CategoryViewModel>(newCategory);
                return new(newCategoryViewModel, null, false);
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while adding the category {Name} with error massage: {ErrorMassage}", model?.Name, ex.Message);
                return new(null, "An error occurred while adding the category.", true, HttpStatusCode.GatewayTimeout);
            }
        }

        public async Task<Response<CategoryViewModel>> UpdateCategoryAsync(CategoryFormVM model)
        {
            try
            {
                if (model is null)
                    return new(null, "You don't enter a write values when you editing category are you atker!!", true, HttpStatusCode.NotAcceptable);

                var mappingCategory = _mapper.Map<Category>(model);
                var updateCategory = await _categoryRepo.UpdateAsync(mappingCategory);
                if (updateCategory is null)
                {
                    return new(null, "You must change your category name", true, HttpStatusCode.BadRequest);
                }

                var updatedCategoryViewModel = _mapper.Map<CategoryViewModel>(updateCategory);
                return new(updatedCategoryViewModel, null, false);
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while updating the category {Name} with error massage: {ErrorMassage}", model?.Name, ex.Message);
                return new(null, "An error occurred while updating the category.", true, HttpStatusCode.GatewayTimeout);
            }
        }
        public async Task<Response<CategoryViewModel>> ToggleStatusCategoryAsync(int categoryId)
        {
            try
            {
                var category = await _categoryRepo.ToggleStatusAsync(categoryId);
                if (category is null)
                {
                    return new(null, "Not Found Your Category or soething went wrong when we save in database", true, HttpStatusCode.GatewayTimeout);
                }
                var categoryViewModel = _mapper.Map<CategoryViewModel>(category);
                return new(categoryViewModel, null, false);
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while toggling the category status with id {ID} with error massage: {ErrorMassage}", categoryId, ex.Message);
                return new(null, "An error occurred while toggling the category status.", true, HttpStatusCode.BadRequest);
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

