using System.ComponentModel.DataAnnotations;

namespace TranslationManagement.Services.DTO
{
    public class CreateTranslatorRequestDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string HourlyRate { get; set; }
        [Required]
        public string CreditCardNumber { get; set; }
        [Required]
        public int Status { get; set; }
    }
}
