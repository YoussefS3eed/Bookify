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
        public async Task<Result<CategoryDTO>> CreateAsync(CategoryCreateDTO dto)
        {
            if (dto == null)
                return new Error("Category.InvalidData", "Invalid data.", HttpStatusCode.BadRequest);

            if (await NameExistsAsync(dto.Name))
                return new Error("Category.NameExists", "Category name already exists.", HttpStatusCode.Conflict);

            var category = _mapper.Map<Category>(dto);
            var result = await _categoryRepo.AddAsync(category);

            if (result == null)
                return new Error("Category.CreationFailed", "Failed to create category in database.", HttpStatusCode.InternalServerError);

            return _mapper.Map<CategoryDTO>(result);
        }
        public async Task<Result<CategoryDTO>> UpdateAsync(CategoryUpdateDTO dto)
        {
            if (dto == null)
                return new Error("Category.InvalidData", "Invalid data.", HttpStatusCode.BadRequest);

            var existingCategory = await _categoryRepo.GetByIdAsync(dto.Id);
            if (existingCategory == null)
                return new Error("Category.NotFound", "Category not found.", HttpStatusCode.NotFound);

            // Check if name changed and validate uniqueness
            if (existingCategory.Name != dto.Name && await NameExistsAsync(dto.Name))
                return new Error("Category.NameExists", "Category name already exists.", HttpStatusCode.Conflict);

            var category = _mapper.Map<Category>(dto);
            var result = await _categoryRepo.UpdateAsync(category);
            if (result == null)
                return new Error("Category.UpdateFailed", "Database error.", HttpStatusCode.BadRequest);

            return _mapper.Map<CategoryDTO>(result);
        }
        public async Task<Result<CategoryDTO>> ToggleStatusAsync(int categoryId)
        {
            var category = await _categoryRepo.GetByIdAsync(categoryId);
            if (category == null)
                return new Error("Category.NotFound", "Category not found.", HttpStatusCode.NotFound);

            var result = await _categoryRepo.ToggleStatusAsync(category.Id);
            if (result == null)
                return new Error("Category.ToggleFailed", "Database error.", HttpStatusCode.BadRequest);

            return _mapper.Map<CategoryDTO>(result);
        }
        public async Task<Result<CategoryDTO>> GetByIdAsync(int categoryId)
        {
            var category = await _categoryRepo.GetByIdAsync(categoryId);
            if (category == null)
                return new Error("Category.NotFound", "Category not found.", HttpStatusCode.NotFound);

            return _mapper.Map<CategoryDTO>(category);
        }
        public async Task<Result<IEnumerable<CategoryDTO>>> GetAllAsync()
        {
            var categories = await _categoryRepo.GetAllAsync();
            return Result.Success(_mapper.Map<IEnumerable<CategoryDTO>>(categories));
        }
        public async Task<Result<IEnumerable<CategoryDTO>>> GetAllNotActiveAsync()
        {
            var categories = await _categoryRepo.GetAllAsync(c => c.IsDeleted);
            return Result.Success(_mapper.Map<IEnumerable<CategoryDTO>>(categories));
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