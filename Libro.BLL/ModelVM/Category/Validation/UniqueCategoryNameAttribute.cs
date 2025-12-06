using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libro.BLL.ModelVM.Category.Validation
{
    public class UniqueCategoryNameAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var service = (ICategoryService)validationContext.GetService(typeof(ICategoryService))!;

            if (value is string name && !string.IsNullOrWhiteSpace(name))
            {
                // Synchronously wait for the async method (since IsValid is not async)
                if (service != null && service.NameExistsAsync(name).GetAwaiter().GetResult())
                {
                    return new ValidationResult("Category name already exists");
                }
            }

            return ValidationResult.Success;
        }
    }
}
