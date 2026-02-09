namespace Bookify.PL.ViewModels.Author
{
    public class AuthorViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime CreatedOn { get; private set; }
        public DateTime? UpdatedOn { get; private set; }
        public bool IsDeleted { get; private set; }
    }
}
