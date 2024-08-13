using System.ComponentModel.DataAnnotations;

namespace CatalogService.WebApi.Attrtibutes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public class FileSizeAttribute : ValidationAttribute
    {
        private readonly long _maxFileSize;

        public FileSizeAttribute(long maxFileSize)
        {
            _maxFileSize = maxFileSize;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is IFormFile file)
            {
                if (file.Length > _maxFileSize)
                {
                    return new ValidationResult($"File size cannot exceed {_maxFileSize / (1024 * 1024)} MB.");
                }
            }

            return ValidationResult.Success;
        }
    }
}
