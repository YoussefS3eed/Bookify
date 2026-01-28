using Libro.BLL.DTOs.Category;

namespace Libro.BLL.Service.Abstraction
{
    public interface ICategoryService
    {
        Task<Response<CategoryDto>> CreateAsync(CategoryCreateDto dto);
        Task<Response<CategoryDto>> UpdateAsync(CategoryUpdateDto dto);
        Task<Response<CategoryDto>> ToggleStatusAsync(int categoryId);
        Task<Response<CategoryDto>> GetByIdAsync(int categoryId);
        Task<Response<IEnumerable<CategoryDto>>> GetAllAsync();
        Task<Response<IEnumerable<CategoryDto>>> GetAllNotActiveAsync();
        Task<bool> NameExistsAsync(string name);
        Task<bool> IsAllowed(int Id, string Name);
    }
}
