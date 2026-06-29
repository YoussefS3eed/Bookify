namespace Bookify.BLL.Service.Abstraction
{
    public interface IAuthorService
    {
        Task<Result<AuthorDto>> CreateAsync(CreateAuthorDto dto);
        Task<Result<AuthorDto>> UpdateAsync(UpdateAuthorDto dto);
        Task<Result<AuthorDto>> ToggleStatusAsync(int authorId);
        Task<Result<AuthorDto>> GetByIdAsync(int authorId);
        Task<Result<IEnumerable<AuthorDto>>> GetAllAsync();
        Task<Result<IEnumerable<AuthorDto>>> GetAllNotActiveAsync();
        Task<bool> IsAllowed(int Id, string Name);
    }
}
