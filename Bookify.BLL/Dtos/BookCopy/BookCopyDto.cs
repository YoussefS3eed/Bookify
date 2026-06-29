namespace Bookify.BLL.Dtos.BookCopy
{
    public class BookCopyDto
    {
        public int Id { get; set; }
        public string? BookTitle { get; set; }
        public BookDto Book { get; set; } = null!;
        public bool IsAvailableForRental { get; set; }
        public int EditionNumber { get; set; }
        public int SerialNumber { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
