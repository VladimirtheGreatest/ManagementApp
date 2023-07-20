using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TranslationManagement.Services.DTO
{
    public class CreateTranslatorJobRequestDto
    {
        public CreateTranslatorJobRequestDto()
        {
            Status = 1; //default is new for this request
        }

        [Required]
        public string CustomerName { get; set; }
        [Required]
        public string OriginalContent { get; set; }
        [Required]
        public string TranslatedContent { get; set; }
        [JsonIgnore]
        public double Price { get; set; }
        [Required]
        public int Status { get; set; }
    }
}
