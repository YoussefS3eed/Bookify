using Bookify.BLL.Common.Abstraction;

namespace Bookify.BLL.Common.Validation
{
    public class UniqueNameAttribute : ValidationAttribute
    {
        private readonly Type _validatorType;

        public UniqueNameAttribute(Type validatorType)
        {
            if (!typeof(IUniqueNameValidator).IsAssignableFrom(validatorType))
                throw new ArgumentException("Validator type must implement IUniqueNameValidator");

            _validatorType = validatorType;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is not string name || string.IsNullOrWhiteSpace(name))
                return ValidationResult.Success;

            // If DI Is not exist
            var validator = validationContext.GetService(_validatorType) as IUniqueNameValidator;

            if (validator is null)
                throw new InvalidOperationException(
                    $"Service {_validatorType.Name} is not registered in the DI container.");


            var exists = validator.NameExistsAsync(name).GetAwaiter().GetResult();

            if (exists)
                return new ValidationResult($"{name} already exists");

            return ValidationResult.Success;
        }
    }
}



