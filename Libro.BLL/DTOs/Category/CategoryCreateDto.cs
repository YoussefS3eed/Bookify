namespace Libro.BLL.DTOs.Category
{
    public class CategoryCreateDto
    {
        [MaxLength(100), Required]
        public string Name { get; set; } = null!;
    }
}
