namespace Bookify.BLL.DTOs.BookCopy
{
    public class CreateBookCopyDTO
    {
        public int BookId { get; set; }
        public bool IsAvailableForRental { get; set; }
        public int EditionNumber { get; private set; }
        public string CreatedBy { get; set; } = null!;
    }
}
