namespace Bookify.BLL.Dtos.Author
{
    public class UpdateAuthorDto
    {
        public int Id { get; set; }
        [MaxLength(100), Required]
        public string Name { get; set; } = null!;
    }
}