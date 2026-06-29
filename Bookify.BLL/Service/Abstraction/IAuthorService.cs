namespace Bookify.BLL.Service.Abstraction
{
    public interface IAuthorService
    {
        Task<Result<AuthorDTO>> CreateAsync(CreateAuthorDTO dto);
        Task<Result<AuthorDTO>> UpdateAsync(UpdateAuthorDTO dto);
        Task<Result<AuthorDTO>> ToggleStatusAsync(int authorId);
        Task<Result<AuthorDTO>> GetByIdAsync(int authorId);
        Task<Result<IEnumerable<AuthorDTO>>> GetAllAsync();
        Task<Result<IEnumerable<AuthorDTO>>> GetAllNotActiveAsync();
        Task<bool> IsAllowed(int Id, string Name);
    }
}
