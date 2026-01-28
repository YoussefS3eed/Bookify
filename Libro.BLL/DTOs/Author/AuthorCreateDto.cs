namespace Libro.BLL.DTOs.Author
{
    public class AuthorCreateDto
    {
        [MaxLength(100), Required]
        public string Name { get; set; } = null!;
    }
}
