namespace Libro.PL.ViewModels.Author
{
    public class AuthorFormViewModel
    {
        public int Id { get; set; }
        [MaxLength(100, ErrorMessage = Errors.MaxLength), Display(Name = "Author")]
        [Remote("AllowItem", null!, AdditionalFields = "Id", ErrorMessage = Errors.Duplicated)]

        //[UniqueName(typeof(AuthorService))]
        public string Name { get; set; } = null!;
    }
}
