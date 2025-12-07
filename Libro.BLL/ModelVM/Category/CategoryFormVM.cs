namespace Libro.BLL.ModelVM.Category
{
    public class CategoryFormVM
    {
        public int Id { get; set; }
        [MaxLength(100, ErrorMessage = "Max length Can not be more than {1} character!"), Display(Name = "category")]
        //[Remote("AllowItem", null!, AdditionalFields = "Id", ErrorMessage = "Category with the same name is already exists!")]
        [UniqueName(typeof(CategoryService))]
        public string Name { get; set; } = null!;
    }
}
