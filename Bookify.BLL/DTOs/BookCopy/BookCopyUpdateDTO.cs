namespace Bookify.BLL.DTOs.BookCopy
{
    public class BookCopyUpdateDTO
    {
        public int Id { get; set; }
        public bool IsAvailableForRental { get; set; }
        public int EditionNumber { get; set; }
        public string CreatedBy { get; set; } = null!;
    }
}
