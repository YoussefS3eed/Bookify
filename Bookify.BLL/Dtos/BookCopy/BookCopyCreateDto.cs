namespace Bookify.BLL.Dtos.BookCopy
{
    public class BookCopyCreateDto
    {
        public int BookId { get; set; }
        public bool IsAvailableForRental { get; set; }
        public int EditionNumber { get; private set; }
        public string CreatedBy { get; set; } = null!;
    }
}
