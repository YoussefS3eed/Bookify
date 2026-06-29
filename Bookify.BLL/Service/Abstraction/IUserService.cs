using Bookify.BLL.DTOs.Role;
using Bookify.BLL.DTOs.User;

namespace Bookify.BLL.Service.Abstraction
{
    public interface IUserService
    {
        Task<Result<UserDTO>> CreateAsync(UserCreateDTO dto, string currentUserId);
        Task<Result<UserDTO>> UpdateAsync(UserUpdateDTO dto, string currentUserId);
        Task<Result<string>> ToggleStatusAsync(string id, string currentUserId);
        Task<Result<UserDTO>> ResetPasswordAsync(UserResetPasswordDTO dto, string currentUserId);
        Task<Result<IEnumerable<UserDTO>>> GetAllAsync();
        Task<Result<IEnumerable<RoleDTO>>> GetRolesAsync();
        Task<Result<UserUpdateDTO>> GetForEditAsync(string id);
        Task<Result<UserResetPasswordDTO>> GetForResetPasswordAsync(string id);
        Task<bool> IsUserNameAvailableAsync(string userName, string? userId);
        Task<bool> IsEmailAvailableAsync(string email, string? userId);
    }
}
