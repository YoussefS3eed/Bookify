namespace Libro.BLL.DTOs.Book
{
    public class CreateBookDTO
    {
        [MaxLength(500), Required]
        public string Title { get; set; } = null!;
        [Required]
        public int AuthorId { get; set; }
        [MaxLength(200), Required]
        public string Publisher { get; set; } = null!;
        [Required]
        public DateTime PublishingDate { get; set; }
        public string? ImageUrl { get; set; }
        public string? ImageThumbnailUrl { get; set; }
        public string? ImagePublicId { get; set; }
        [MaxLength(50), Required]
        public string Hall { get; set; } = null!;
        public bool IsAvailableForRental { get; set; }
        [MaxLength(5000), Required]
        public string Description { get; set; } = null!;
        public List<int?> CategoryIds { get; set; } = new();
        [Required]
        public string CreatedBy { get; set; } = null!;
    }
}