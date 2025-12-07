namespace Libro.BLL.Service.Abstraction
{
    public interface ICategoryService
    {
        Task<Response<CategoryViewModel>> CreateCategoryAsync(CategoryFormVM model);
        Task<Response<CategoryViewModel>> UpdateCategoryAsync(CategoryFormVM model);
        Task<Response<CategoryViewModel>> ToggleStatusCategoryAsync(int categoryId);
        Task<Response<CategoryFormVM>> GetCategoryByIdAsync(int categoryId);
        Task<Response<IEnumerable<CategoryViewModel>>> GetAllCategoriesAsync();
        Task<Response<IEnumerable<CategoryViewModel>>> GetAllNotActiveCategoriesAsync();
    }
}
