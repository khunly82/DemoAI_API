using DemoAIAPI.Validators;
using System.ComponentModel.DataAnnotations;

namespace DemoAIAPI.DTO
{
    public class FilesDTO
    {
        [Required]
        // 5mo max
        [MaxSize(5 * 1024 * 1024)]
        // verifier le type
        [IsImage]
        public List<IFormFile> Files { get; set; } = null!;
    }
}
