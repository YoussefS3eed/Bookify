namespace Bookify.BLL.Dtos.Category
{
    public class CategoryCreateDto
    {
        [MaxLength(100), Required]
        public string Name { get; set; } = null!;
    }
}
