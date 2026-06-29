namespace Bookify.BLL.Dtos.User
{
    public record UserDto
    (
        string Id,
        string FullName,
        string Username,
        string Email,
        bool IsDeleted,
        DateTime CreatedOn,
        DateTime? LastUpdatedOn
    );
}
