using System.ComponentModel.DataAnnotations;

namespace CatalogService.WebApi.Attrtibutes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public class FileTypeAttribute : ValidationAttribute
    {
        private readonly string[] _allowedTypes;

        public FileTypeAttribute(string allowedTypes)
        {
            _allowedTypes = allowedTypes.Split(',');
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is IFormFile file)
            {
                var isValidType = _allowedTypes.Any(type => file.ContentType.Equals(type.Trim(), StringComparison.OrdinalIgnoreCase));
                if (!isValidType)
                {
                    return new ValidationResult($"Only {string.Join(", ", _allowedTypes)} files are allowed.");
                }
            }

            return ValidationResult.Success;
        }
    }
}
