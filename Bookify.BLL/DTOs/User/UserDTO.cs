namespace Bookify.BLL.DTOs.User
{
    public record UserDTO
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
