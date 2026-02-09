using Bookify.BLL.Common.ResponseResult;
using Bookify.BLL.DTOs.Category;

namespace Bookify.BLL.Service.Abstraction
{
    public interface ICategoryService
    {
        Task<Response<CategoryDTO>> CreateAsync(CreateCategoryDTO dto);
        Task<Response<CategoryDTO>> UpdateAsync(UpdateCategoryDTO dto);
        Task<Response<CategoryDTO>> ToggleStatusAsync(int categoryId);
        Task<Response<CategoryDTO>> GetByIdAsync(int categoryId);
        Task<Response<IEnumerable<CategoryDTO>>> GetAllAsync();
        Task<Response<IEnumerable<CategoryDTO>>> GetAllNotActiveAsync();
        Task<bool> IsAllowed(int Id, string Name);
    }
}
