using Bookify.BLL.DTOs.Role;
using Bookify.BLL.DTOs.User;

namespace Bookify.BLL.Service.Abstraction
{
    public interface IUserService
    {
        Task<Response<UserDTO>> CreateAsync(UserCreateDTO dto, string currentUserId);
        Task<Response<UserDTO>> UpdateAsync(UserUpdateDTO dto, string currentUserId);
        Task<Response<string>> ToggleStatusAsync(string id, string currentUserId);
        Task<Response<UserDTO>> ResetPasswordAsync(UserResetPasswordDTO dto, string currentUserId);
        Task<Response<IEnumerable<UserDTO>>> GetAllAsync();
        Task<Response<IEnumerable<RoleDTO>>> GetRolesAsync();
        Task<Response<UserUpdateDTO>> GetForEditAsync(string id);
        Task<Response<UserResetPasswordDTO>> GetForResetPasswordAsync(string id);
        Task<bool> IsUserNameAvailableAsync(string userName, string? userId);
        Task<bool> IsEmailAvailableAsync(string email, string? userId);
    }
}
