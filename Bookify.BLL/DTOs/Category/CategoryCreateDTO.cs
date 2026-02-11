namespace Bookify.BLL.DTOs.Category
{
    public class CategoryCreateDTO
    {
        [MaxLength(100), Required]
        public string Name { get; set; } = null!;
    }
}
