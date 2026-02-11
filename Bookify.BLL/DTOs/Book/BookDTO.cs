namespace Bookify.BLL.DTOs.Book
{
    public class BookDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public int AuthorId { get; set; }
        public string AuthorName { get; set; } = null!; // TODO: Check if you want to remove it (Get Edit ✅, Get Create ✅ , Post Edit, Post Create ✅)
        public string Publisher { get; set; } = null!;
        public DateTime PublishingDate { get; set; }
        public string? ImageUrl { get; set; }
        public string? ImageThumbnailUrl { get; set; }
        public string? ImagePublicId { get; set; }
        public string Hall { get; set; } = null!;
        public bool IsAvailableForRental { get; set; }
        public string Description { get; set; } = null!;
        public IEnumerable<int?> CategoryIds { get; set; } = null!;
        public IEnumerable<string?> CategoryNames { get; set; } = null!;
        public IEnumerable<BookCopyDTO> Copies { get; set; } = null!;
        public bool IsDeleted { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}