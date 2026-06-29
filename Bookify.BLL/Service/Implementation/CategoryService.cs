namespace Bookify.BLL.Service.Implementation
{
    public class CategoryService : ICategoryService, IUniqueNameValidator
    {
        private readonly ICategoryRepo _categoryRepo;
        private readonly IMapper _mapper;
        private readonly ILogger<CategoryService> _logger;
        public CategoryService(ICategoryRepo categoryRepo, IMapper mapper, ILogger<CategoryService> logger)
        {
            _categoryRepo = categoryRepo;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<Result<CategoryDto>> CreateAsync(CategoryCreateDto dto)
        {
            if (dto == null)
                return new Error("Category.InvalidData", "Category creation data cannot be null.", HttpStatusCode.BadRequest);

            if (await NameExistsAsync(dto.Name))
                return new Error("Category.NameExists", $"Category with name '{dto.Name}' already exists.", HttpStatusCode.Conflict);

            var category = _mapper.Map<Category>(dto);
            var result = await _categoryRepo.AddAsync(category);

            if (result == null)
                return new Error("Category.CreationFailed", $"Failed to save Category '{dto.Name}' in the database.", HttpStatusCode.InternalServerError);

            return _mapper.Map<CategoryDto>(result);
        }
        public async Task<Result<CategoryDto>> UpdateAsync(CategoryUpdateDto dto)
        {
            if (dto == null)
                return new Error("Category.InvalidData", "Category update data cannot be null.", HttpStatusCode.BadRequest);

            var existingCategory = await _categoryRepo.GetByIdAsync(dto.Id);
            if (existingCategory == null)
                return new Error("Category.NotFound", $"Category with ID {dto.Id} was not found.", HttpStatusCode.NotFound);

            // Check if name changed and validate uniqueness
            if (existingCategory.Name != dto.Name && await NameExistsAsync(dto.Name))
                return new Error("Category.NameExists", $"Category with name '{dto.Name}' already exists.", HttpStatusCode.Conflict);

            var category = _mapper.Map<Category>(dto);
            var result = await _categoryRepo.UpdateAsync(category);
            if (result == null)
                return new Error("Category.UpdateFailed", $"Failed to update Category '{dto.Name}' in database.", HttpStatusCode.BadRequest);

            return _mapper.Map<CategoryDto>(result);
        }
        public async Task<Result<CategoryDto>> ToggleStatusAsync(int categoryId)
        {
            var category = await _categoryRepo.GetByIdAsync(categoryId);
            if (category == null)
                return new Error("Category.NotFound", $"Category with ID {categoryId} was not found.", HttpStatusCode.NotFound);

            var result = await _categoryRepo.ToggleStatusAsync(category.Id);
            if (result == null)
                return new Error("Category.ToggleFailed", $"Failed to toggle status for Category with ID {categoryId} in database.", HttpStatusCode.BadRequest);

            return _mapper.Map<CategoryDto>(result);
        }
        public async Task<Result<CategoryDto>> GetByIdAsync(int categoryId)
        {
            var category = await _categoryRepo.GetByIdAsync(categoryId);
            if (category == null)
                return new Error("Category.NotFound", $"Category with ID {categoryId} was not found.", HttpStatusCode.NotFound);

            return _mapper.Map<CategoryDto>(category);
        }
        public async Task<Result<IEnumerable<CategoryDto>>> GetAllAsync()
        {
            var categories = await _categoryRepo.GetAllAsync();
            return Result.Success(_mapper.Map<IEnumerable<CategoryDto>>(categories));
        }
        public async Task<Result<IEnumerable<CategoryDto>>> GetAllNotActiveAsync()
        {
            var categories = await _categoryRepo.GetAllAsync(c => c.IsDeleted);
            return Result.Success(_mapper.Map<IEnumerable<CategoryDto>>(categories));
        }
        public async Task<bool> NameExistsAsync(string name) =>
            await _categoryRepo.AnyAsync(c => c.Name == name);
        public async Task<bool> IsAllowed(int Id, string name)
        {
            var category = await _categoryRepo.GetSingleOrDefaultAsync(c => c.Name == name);
            return category is null || category.Id.Equals(Id);
        }
        private string GetCurrentUser()
        {
            // TODO: Implement this to return the current user for categories
            // يجب تنفيذ هذا ليعيد المستخدم الحالي
            // مثال: يمكن استخدام IHttpContextAccessor
            return "System"; // مؤقت
        }
    }
}