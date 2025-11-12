using System.ComponentModel.DataAnnotations;

namespace DemoAIAPI.Validators
{
    public class MaxSizeAttribute(int bytes): ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            List<IFormFile>? files = value as List<IFormFile>;
            if(files == null)
            {
                return true;
            }
            ErrorMessage = "Un des fichiers est trop grand";
            return files.All(f => f.Length < bytes);
        }
    }
}
