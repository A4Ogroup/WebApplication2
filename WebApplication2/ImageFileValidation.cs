using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

public class ImageFileAttribute : ValidationAttribute
{
    private readonly Regex _regex = new Regex(@"^.*\.(jpg|jpeg|png|gif|bmp|tiff|webp)$", RegexOptions.IgnoreCase);

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is IFormFile file)
        {
            if (!_regex.IsMatch(file.FileName))
            {
                return new ValidationResult("Invalid image file type.");
            }
        }

        return ValidationResult.Success;
    }
}
