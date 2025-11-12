using DemoAIAPI.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Schema.Generation;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;




namespace DemoAIAPI.Services
{
    public class OpenAIService(HttpClient client, IConfiguration config)
    {
        public Invoice ExtractDataFromFiles(List<IFormFile> files)
        {
            var schema = new JSchemaGenerator().Generate(typeof(Invoice)).ToString();

            // se connecter à Open AI et extraire les données
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + config.GetValue<string>("OpenAIKEY"));
            var result = client.PostAsJsonAsync("https://api.openai.com/v1/chat/completions", new {
                model = "gpt-4o",
                messages = new object[]
                {
                    new { 
                        role = "system", 
                        content = "Your job is to extract data from images that represent an invoice. In this invoice you should find the supplier reference, the VAT number and the lines of the invoice. Each line should contain the ean, the description, the quantity and the unit price" 
                    },
                    new
                    {
                        role = "user",
                        content = files.Select(f => 
                            new
                            {
                                type = "image_url",
                                image_url = new { url = FileToBytes(f), detail = "high", }
                            }
                        ).ToArray()
                        
                    }
                },
                response_format = new
                {
                    type = "json_schema",
                    json_schema = new
                    {
                        name = "invoiceSchema",
                        schema = JsonObject.Parse(schema)
                    }
                }
            }).Result.Content.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<Invoice>(JsonObject.Parse(result)["choices"][0]["message"]["content"].ToString());
        }

        private string FileToBytes(IFormFile file)
        {
            using MemoryStream stream = new MemoryStream();
            file.OpenReadStream().CopyTo(stream);
            return $"data:{file.ContentType};base64,{Convert.ToBase64String(stream.ToArray())}";
        }
    }
}
