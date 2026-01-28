using Libro.BLL.Common.ResponseResult;
using Libro.BLL.DTOs.Author;

namespace Libro.BLL.Service.Abstraction
{
    public interface IAuthorService
    {
        Task<Response<AuthorDto>> CreateAsync(AuthorCreateDto dto);
        Task<Response<AuthorDto>> UpdateAsync(AuthorUpdateDto dto);
        Task<Response<AuthorDto>> ToggleStatusAsync(int authorId);
        Task<Response<AuthorDto>> GetByIdAsync(int authorId);
        Task<Response<IEnumerable<AuthorDto>>> GetAllAsync();
        Task<Response<IEnumerable<AuthorDto>>> GetAllNotActiveAsync();
        Task<bool> NameExistsAsync(string name);
        Task<bool> IsAllowed(int Id, string Name);
    }
}
