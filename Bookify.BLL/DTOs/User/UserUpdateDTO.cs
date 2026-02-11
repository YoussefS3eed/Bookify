namespace Bookify.BLL.DTOs.User
{
    public record UserUpdateDTO
    (
        string Id,
        string FullName,
        string UserName,
        string Email,
        IEnumerable<string> SelectedRoles
    );
}
