namespace Libro.PL.ViewModels.Book
{
    public class BookFormViewModel
    {
        public int Id { get; set; }
        [MaxLength(500, ErrorMessage = "Max length Can not be more than {1} character!"), Display(Name = "Title")]
        public string Title { get; set; } = null!;
        [Display(Name = "Author")]
        public int AuthorId { get; set; }
        public IEnumerable<DAL.Entities.Author>? Authors { get; set; }
        [MaxLength(200, ErrorMessage = "Max length Can not be more than {1} character!"), Display(Name = "Publisher")]
        public string Publisher { get; set; } = null!;
        [Display(Name = "Publishing Date")]
        public DateTime PublishingDate { get; set; }
        //[]
        public string? ImageUrl { get; set; }
        [MaxLength(50, ErrorMessage = "Max length Can not be more than {1} character!"), Display(Name = "Hall")]

        public string Hall { get;set; } = null!;
        public bool IsAvailableForRental{ get; set; }
        public string Description { get; set; } = null!;
        //public ICollection<BookCategory> Categories { get; set; } = new List<BookCategory>();
    }
}
