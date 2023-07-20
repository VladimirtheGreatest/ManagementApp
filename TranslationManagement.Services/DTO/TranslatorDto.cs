using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TranslationManagement.Services.DTO
{
    public class TranslatorDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string HourlyRate { get; set; }
        public string CreditCardNumber { get; set; }
        public string Status { get; set; }
    }
}
