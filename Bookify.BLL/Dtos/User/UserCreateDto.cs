namespace Bookify.BLL.Dtos.User
{
    public record UserCreateDto
    (
        string FullName,
        string Username,
        string Email,
        string Password,
        IEnumerable<string> SelectedRoles
    );
}
