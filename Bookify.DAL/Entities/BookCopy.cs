using Bookify.DAL.Entities.Base;

namespace Bookify.DAL.Entities
{
    public class BookCopy : BaseEntity
    {
        private BookCopy() { }
        public BookCopy(int bookId)
        {
            BookId = bookId;
        }
        public BookCopy(int bookId, int editionNumber, bool isAvailableForRental, string createdBy)
        {
            BookId = bookId;
            EditionNumber = editionNumber;
            IsAvailableForRental = isAvailableForRental;
            CreatedBy = createdBy;
        }
        public int Id { get; private set; }
        public int BookId { get; private set; }
        public Book? Book { get; private set; }
        public bool IsAvailableForRental { get; private set; }
        public int EditionNumber { get; private set; }
        public int SerialNumber { get; private set; }

        public void Update(bool isAvailableForRental, int editionNumber, string updatedBy = "System")
        {
            IsAvailableForRental = isAvailableForRental;
            EditionNumber = editionNumber;
            UpdatedOn = DateTime.UtcNow;
            UpdatedBy = updatedBy;
        }
        public void ToggleStatus(string deletedBy)
        {
            IsDeleted = !IsDeleted;
            UpdatedOn = DateTime.UtcNow;
            UpdatedBy = deletedBy;
        }
    }
}
