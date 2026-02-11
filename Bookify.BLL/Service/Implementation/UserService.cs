using Bookify.BLL.DTOs.Role;
using Bookify.BLL.DTOs.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Bookify.BLL.Service.Implementation
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        private readonly ILogger<UserService> _logger;
        public UserService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IMapper mapper, ILogger<UserService> logger)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Response<UserDTO>> CreateAsync(UserCreateDTO dto, string currentUserId)
        {
            try
            {
                var user = new ApplicationUser
                {
                    FullName = dto.FullName,
                    UserName = dto.Username,
                    Email = dto.Email,
                    CreatedById = currentUserId
                };

                var result = await _userManager.CreateAsync(user, dto.Password);

                if (!result.Succeeded)
                {
                    var errors = string.Join("; ", result.Errors.Select(e => e.Description));
                    _logger.LogWarning("User creation failed: {Errors}", errors);
                    return new(null, $"User creation failed: {errors}", true, HttpStatusCode.BadRequest);
                }

                if (dto.SelectedRoles.Any())
                    await _userManager.AddToRolesAsync(user, dto.SelectedRoles);

                var mapped = _mapper.Map<UserDTO>(user);
                return new(mapped, null, false, HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating user");
                return new(null, "Could not create user.", true, HttpStatusCode.InternalServerError);
            }
        }

        public async Task<Response<UserDTO>> UpdateAsync(UserUpdateDTO dto, string currentUserId)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(dto.Id);
                if (user == null)
                    return new(null, "User not found", true, HttpStatusCode.NotFound);


                user.FullName = dto.FullName;
                user.UserName = dto.UserName;
                user.Email = dto.Email;
                user.LastUpdatedById = currentUserId;
                user.LastUpdatedOn = DateTime.UtcNow;

                var result = await _userManager.UpdateAsync(user);
                if (!result.Succeeded)
                {
                    var errors = string.Join("; ", result.Errors.Select(e => e.Description));
                    _logger.LogWarning("User update failed: {Errors}", errors);
                    return new(null, $"User update failed: {errors}", true, HttpStatusCode.BadRequest);
                }
                var currentRoles = await _userManager.GetRolesAsync(user);

                if (!currentRoles.SequenceEqual(dto.SelectedRoles))
                {
                    await _userManager.RemoveFromRolesAsync(user, currentRoles);
                    await _userManager.AddToRolesAsync(user, dto.SelectedRoles);
                }

                var mapped = _mapper.Map<UserDTO>(user);
                return new(mapped, null, false);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating user");
                return new(null, "Could not update user.", true, HttpStatusCode.InternalServerError);
            }
        }

        public async Task<Response<string>> ToggleStatusAsync(string id, string currentUserId)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user == null)
                    return new(null, "User not found", true, HttpStatusCode.NotFound);

                user.IsDeleted = !user.IsDeleted;
                user.LastUpdatedById = currentUserId;
                user.LastUpdatedOn = DateTime.UtcNow;

                var result = await _userManager.UpdateAsync(user);
                if (!result.Succeeded)
                {
                    var errors = string.Join("; ", result.Errors.Select(e => e.Description));
                    _logger.LogWarning("Toggling user status failed: {Errors}", errors);
                    return new(null, $"Toggling user status failed: {errors}", true, HttpStatusCode.BadRequest);
                }

                return new(user.LastUpdatedOn.ToString(), null, false);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error toggling user status");
                return new(null, "Could not toggle user status.", true, HttpStatusCode.InternalServerError);
            }
        }

        public async Task<Response<UserDTO>> ResetPasswordAsync(UserResetPasswordDTO dto, string currentUserId)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(dto.Id);
                if (user == null)
                    return new(null, "User not found", true, HttpStatusCode.NotFound);

                var currentPasswordHash = user.PasswordHash;
                await _userManager.RemovePasswordAsync(user);

                var result = await _userManager.AddPasswordAsync(user, dto.Password);
                if (!result.Succeeded)
                {
                    user.PasswordHash = currentPasswordHash;
                    await _userManager.UpdateAsync(user);

                    var errors = string.Join("; ", result.Errors.Select(e => e.Description));
                    _logger.LogWarning("Password reset failed: {Errors}", errors);
                    return new(null, $"Password reset failed: {errors}", true, HttpStatusCode.BadRequest);
                }

                user.LastUpdatedById = currentUserId;
                user.LastUpdatedOn = DateTime.UtcNow;
                await _userManager.UpdateAsync(user);

                var mapped = _mapper.Map<UserDTO>(user);
                return new(mapped, null, false);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error resetting user password");
                return new(null, "Could not reset user password.", true, HttpStatusCode.InternalServerError);
            }
        }

        public async Task<Response<IEnumerable<UserDTO>>> GetAllAsync()
        {
            try
            {
                var users = await _userManager.Users.ToListAsync();
                var mappedUsers = _mapper.Map<IEnumerable<UserDTO>>(users);
                return new(mappedUsers, null, false);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching users");
                return new(null, "Could not load users.", true, HttpStatusCode.InternalServerError);
            }
        }

        public async Task<Response<IEnumerable<RoleDTO>>> GetRolesAsync()
        {
            try
            {
                var roles = await _roleManager.Roles.Select(r => r.Name!).ToListAsync();
                return new(roles.Select(name => new RoleDTO(name)), null, false);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching roles");
                return new(null, "Could not load roles.", true, HttpStatusCode.InternalServerError);
            }
        }

        public async Task<Response<UserUpdateDTO>> GetForEditAsync(string id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user == null)
                    return new(null, "User not found", true, HttpStatusCode.NotFound);

                var dto = new UserUpdateDTO
                (
                    Id: user.Id,
                    FullName: user.FullName,
                    UserName: user.UserName!,
                    Email: user.Email!,
                    SelectedRoles: (await _userManager.GetRolesAsync(user)).ToList()
                );

                return new(dto, null, false);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching user for edit");
                return new(null, "Could not load user for edit.", true, HttpStatusCode.InternalServerError);
            }
        }

        public async Task<Response<UserResetPasswordDTO>> GetForResetPasswordAsync(string id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user == null)
                    return new(null, "User not found", true, HttpStatusCode.NotFound);

                var dto = new UserResetPasswordDTO
                (
                    Id: user.Id,
                    Password: string.Empty
                );

                return new(dto, null, false);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching user for password reset");
                return new(null, "Could not load user for password reset.", true, HttpStatusCode.InternalServerError);
            }
        }

        public async Task<bool> IsUserNameAvailableAsync(string userName, string? userId)
        {
            var user = await _userManager.FindByNameAsync(userName);
            return user == null || user.Id == userId;
        }

        public async Task<bool> IsEmailAvailableAsync(string email, string? userId)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return user == null || user.Id == userId;
        }
    }
}
