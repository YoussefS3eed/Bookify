using Libro.BLL.ModelVM.Category.Validation;
using System.ComponentModel.DataAnnotations;
//using Microsoft.AspNetCore.Mvc;

namespace Libro.BLL.ModelVM.Category
{
    public class CategoryFormVM
    {
        public int Id { get; set; }
        [MaxLength(100, ErrorMessage = "Max length Can not be more than {1} character!")]
        //[Remote("AllowItem", null!, AdditionalFields = "Id", ErrorMessage = "Category with the same name is already exists!")]
        // TODO: Enhance For Globalization
        [UniqueCategoryName]
        public string Name { get; set; } = null!;
    }
}
