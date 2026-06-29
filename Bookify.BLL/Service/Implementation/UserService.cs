using Bookify.BLL.Dtos.Role;
using Bookify.BLL.Dtos.User;
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

        public async Task<Result<UserDto>> CreateAsync(UserCreateDto dto, string currentUserId)
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
                _logger.LogWarning("User creation failed for {Username}: {Errors}", dto.Username, errors);
                return new Error("User.CreationFailed", $"User creation failed: {errors}", HttpStatusCode.BadRequest);
            }

            if (dto.SelectedRoles.Any())
                await _userManager.AddToRolesAsync(user, dto.SelectedRoles);

            var mapped = _mapper.Map<UserDto>(user);
            return mapped;
        }

        public async Task<Result<UserDto>> UpdateAsync(UserUpdateDto dto, string currentUserId)
        {
            var user = await _userManager.FindByIdAsync(dto.Id);
            if (user == null)
                return new Error("User.NotFound", $"User with ID '{dto.Id}' was not found.", HttpStatusCode.NotFound);

            user.FullName = dto.FullName;
            user.UserName = dto.UserName;
            user.Email = dto.Email;
            user.LastUpdatedById = currentUserId;
            user.LastUpdatedOn = DateTime.UtcNow;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                var errors = string.Join("; ", result.Errors.Select(e => e.Description));
                _logger.LogWarning("User update failed for ID {Id}: {Errors}", dto.Id, errors);
                return new Error("User.UpdateFailed", $"User update failed: {errors}", HttpStatusCode.BadRequest);
            }
            var currentRoles = await _userManager.GetRolesAsync(user);

            if (!currentRoles.SequenceEqual(dto.SelectedRoles))
            {
                await _userManager.RemoveFromRolesAsync(user, currentRoles);
                await _userManager.AddToRolesAsync(user, dto.SelectedRoles);
            }

            var mapped = _mapper.Map<UserDto>(user);
            return mapped;
        }

        public async Task<Result<string>> ToggleStatusAsync(string id, string currentUserId)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return new Error("User.NotFound", $"User with ID '{id}' was not found.", HttpStatusCode.NotFound);

            user.IsDeleted = !user.IsDeleted;
            user.LastUpdatedById = currentUserId;
            user.LastUpdatedOn = DateTime.UtcNow;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                var errors = string.Join("; ", result.Errors.Select(e => e.Description));
                _logger.LogWarning("Toggling user status failed for ID {Id}: {Errors}", id, errors);
                return new Error("User.ToggleStatusFailed", $"Toggling user status failed: {errors}", HttpStatusCode.BadRequest);
            }

            return user.LastUpdatedOn.ToString();
        }

        public async Task<Result<UserDto>> ResetPasswordAsync(UserResetPasswordDto dto, string currentUserId)
        {
            var user = await _userManager.FindByIdAsync(dto.Id);
            if (user == null)
                return new Error("User.NotFound", $"User with ID '{dto.Id}' was not found.", HttpStatusCode.NotFound);

            var currentPasswordHash = user.PasswordHash;
            await _userManager.RemovePasswordAsync(user);

            var result = await _userManager.AddPasswordAsync(user, dto.Password);
            if (!result.Succeeded)
            {
                user.PasswordHash = currentPasswordHash;
                await _userManager.UpdateAsync(user);

                var errors = string.Join("; ", result.Errors.Select(e => e.Description));
                _logger.LogWarning("Password reset failed for ID {Id}: {Errors}", dto.Id, errors);
                return new Error("User.ResetPasswordFailed", $"Password reset failed: {errors}", HttpStatusCode.BadRequest);
            }

            user.LastUpdatedById = currentUserId;
            user.LastUpdatedOn = DateTime.UtcNow;
            await _userManager.UpdateAsync(user);

            var mapped = _mapper.Map<UserDto>(user);
            return mapped;
        }

        public async Task<Result<IEnumerable<UserDto>>> GetAllAsync()
        {
            var users = await _userManager.Users.ToListAsync();
            var mappedUsers = _mapper.Map<IEnumerable<UserDto>>(users);
            return Result.Success(mappedUsers);
        }

        public async Task<Result<IEnumerable<RoleDto>>> GetRolesAsync()
        {
            var roles = await _roleManager.Roles.Select(r => r.Name!).ToListAsync();
            var dtos = roles.Select(name => new RoleDto(name));
            return Result.Success(dtos);
        }

        public async Task<Result<UserUpdateDto>> GetForEditAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return new Error("User.NotFound", $"User with ID '{id}' was not found.", HttpStatusCode.NotFound);

            var dto = new UserUpdateDto
            (
                Id: user.Id,
                FullName: user.FullName,
                UserName: user.UserName!,
                Email: user.Email!,
                SelectedRoles: (await _userManager.GetRolesAsync(user)).ToList()
            );

            return dto;
        }

        public async Task<Result<UserResetPasswordDto>> GetForResetPasswordAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return new Error("User.NotFound", $"User with ID '{id}' was not found.", HttpStatusCode.NotFound);

            var dto = new UserResetPasswordDto
            (
                Id: user.Id,
                Password: string.Empty
            );

            return dto;
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
