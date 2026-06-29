namespace Bookify.BLL.Service.Abstraction
{
    public interface ICategoryService
    {
        Task<Result<CategoryDTO>> CreateAsync(CategoryCreateDTO dto);
        Task<Result<CategoryDTO>> UpdateAsync(CategoryUpdateDTO dto);
        Task<Result<CategoryDTO>> ToggleStatusAsync(int categoryId);
        Task<Result<CategoryDTO>> GetByIdAsync(int categoryId);
        Task<Result<IEnumerable<CategoryDTO>>> GetAllAsync();
        Task<Result<IEnumerable<CategoryDTO>>> GetAllNotActiveAsync();
        Task<bool> IsAllowed(int Id, string Name);
    }
}
