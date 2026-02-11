namespace Bookify.BLL.DTOs.User
{
    public record UserCreateDTO
    (
        string FullName,
        string Username,
        string Email,
        string Password,
        IEnumerable<string> SelectedRoles
    );
}
