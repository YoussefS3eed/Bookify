namespace Bookify.BLL.Dtos.Author
{
    public class CreateAuthorDto
    {
        [MaxLength(100), Required]
        public string Name { get; set; } = null!;
    }
}
