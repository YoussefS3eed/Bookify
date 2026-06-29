using Bookify.BLL.Dtos.Role;
using Bookify.BLL.Dtos.User;

namespace Bookify.BLL.Service.Abstraction
{
    public interface IUserService
    {
        Task<Result<UserDto>> CreateAsync(UserCreateDto dto, string currentUserId);
        Task<Result<UserDto>> UpdateAsync(UserUpdateDto dto, string currentUserId);
        Task<Result<string>> ToggleStatusAsync(string id, string currentUserId);
        Task<Result<UserDto>> ResetPasswordAsync(UserResetPasswordDto dto, string currentUserId);
        Task<Result<IEnumerable<UserDto>>> GetAllAsync();
        Task<Result<IEnumerable<RoleDto>>> GetRolesAsync();
        Task<Result<UserUpdateDto>> GetForEditAsync(string id);
        Task<Result<UserResetPasswordDto>> GetForResetPasswordAsync(string id);
        Task<bool> IsUserNameAvailableAsync(string userName, string? userId);
        Task<bool> IsEmailAvailableAsync(string email, string? userId);
    }
}
