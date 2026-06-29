namespace Bookify.BLL.Service.Abstraction
{
    public interface ICategoryService
    {
        Task<Result<CategoryDto>> CreateAsync(CategoryCreateDto dto);
        Task<Result<CategoryDto>> UpdateAsync(CategoryUpdateDto dto);
        Task<Result<CategoryDto>> ToggleStatusAsync(int categoryId);
        Task<Result<CategoryDto>> GetByIdAsync(int categoryId);
        Task<Result<IEnumerable<CategoryDto>>> GetAllAsync();
        Task<Result<IEnumerable<CategoryDto>>> GetAllNotActiveAsync();
        Task<bool> IsAllowed(int Id, string Name);
    }
}
