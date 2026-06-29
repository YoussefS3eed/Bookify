namespace Bookify.BLL.Dtos.BookCopy
{
    public class BookCopyUpdateDto
    {
        public int Id { get; set; }
        public bool IsAvailableForRental { get; set; }
        public int EditionNumber { get; set; }
        public string CreatedBy { get; set; } = null!;
    }
}
