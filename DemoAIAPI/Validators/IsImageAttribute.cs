using System.ComponentModel.DataAnnotations;

namespace DemoAIAPI.Validators
{
    public class IsImageAttribute: ValidationAttribute
    {
        private readonly string[] autorizedFiles = [
            "image/jpeg",
            "image/png",
            "image/webp",
        ];

        public override bool IsValid(object? value)
        {
            List<IFormFile>? files = value as List<IFormFile>;
            if (files == null)
            {
                return true;
            }
            ErrorMessage = "Le type d'un des fichier est invalide";
            return files.All(f => autorizedFiles.Contains(f.ContentType));
        }
    }
}
