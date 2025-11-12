using DemoAIAPI.DTO;
using DemoAIAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoAIAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController(
        OpenAIService openAIService
    ) : ControllerBase
    {
        [HttpPost]
        [Produces<Chat>()]
        public IActionResult ReadFiles([FromForm]FilesDTO dto)
        {
            var invoice = openAIService.ExtractDataFromFiles(dto.Files);
            return Ok(invoice);
        }

        //[HttpPost]
        //public IActionResult SaveData() 
        //{ 
        //    return Ok();
        //}
    }

}

class Chat
{
    public int Id { get; set; }
    public string Nom { get; set; }
}