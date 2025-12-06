namespace Libro.DAL.Entities
{
    public class Category
    {
        protected Category()
        {
            CreatedBy = "Admin";
        }

        public Category(string name, string createToUser)
        {
            Name = name;
            CreatedBy = createToUser;
            IsDeleted = false;
        }
        public int Id { get; private set; }
        public string Name { get; private set; } = null!;
        public DateTime CreatedOn { get; private set; }
        public DateTime? DeletedOn { get; private set; }
        public DateTime? UpdatedOn { get; private set; }
        public string CreatedBy { get; private set; }
        public string? DeletedBy { get; private set; }
        public string? UpdatedBy { get; private set; }
        public bool IsDeleted { get; private set; }
        public bool Update(string name, string userModified)
        {
            if (!string.IsNullOrEmpty(userModified) && Name != name)
            {
                Name = name;
                UpdatedOn = DateTime.UtcNow;
                UpdatedBy = userModified;
                return true;
            }
            return false;
        }
        public bool ToggleStatus(string deleteUser)
        {
            if (!string.IsNullOrEmpty(deleteUser))
            {
                IsDeleted = !IsDeleted;
                DeletedBy = deleteUser;
                DeletedOn = DateTime.UtcNow;
                UpdatedOn = DateTime.UtcNow;
                return true;
            }
            return false;
        }
    }
}
