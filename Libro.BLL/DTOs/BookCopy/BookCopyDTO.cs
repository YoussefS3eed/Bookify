namespace Libro.BLL.DTOs.BookCopy
{
    public class BookCopyDTO
    {
        public int Id { get; set; }
        public string? BookTitle { get; set; }
        public BookDTO Book { get; set; } = null!;
        public bool IsAvailableForRental { get; set; }
        public int EditionNumber { get; set; }
        public int SerialNumber { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
