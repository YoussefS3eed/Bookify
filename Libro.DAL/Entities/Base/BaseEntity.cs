namespace Libro.DAL.Entities.Base
{
    public class BaseEntity
    {
        public DateTime CreatedOn { get; protected set; }
        public DateTime? DeletedOn { get; protected set; }
        public DateTime? UpdatedOn { get; protected set; }
        public string CreatedBy { get; protected set; } = null!;
        public string? DeletedBy { get; protected set; }
        public string? UpdatedBy { get; protected set; }
        public bool IsDeleted { get; protected set; }
    }
}
