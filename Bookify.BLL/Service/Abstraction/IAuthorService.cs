using Bookify.BLL.Common.ResponseResult;
using Bookify.BLL.DTOs.Author;

namespace Bookify.BLL.Service.Abstraction
{
    public interface IAuthorService
    {
        Task<Response<AuthorDTO>> CreateAsync(CreateAuthorDTO dto);
        Task<Response<AuthorDTO>> UpdateAsync(UpdateAuthorDTO dto);
        Task<Response<AuthorDTO>> ToggleStatusAsync(int authorId);
        Task<Response<AuthorDTO>> GetByIdAsync(int authorId);
        Task<Response<IEnumerable<AuthorDTO>>> GetAllAsync();
        Task<Response<IEnumerable<AuthorDTO>>> GetAllNotActiveAsync();
        Task<bool> IsAllowed(int Id, string Name);
    }
}
