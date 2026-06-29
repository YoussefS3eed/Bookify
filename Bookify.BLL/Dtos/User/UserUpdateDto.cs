namespace Bookify.BLL.Dtos.User
{
    public record UserUpdateDto
    (
        string Id,
        string FullName,
        string UserName,
        string Email,
        IEnumerable<string> SelectedRoles
    );
}
