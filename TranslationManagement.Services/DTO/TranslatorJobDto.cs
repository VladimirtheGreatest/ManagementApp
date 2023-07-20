using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TranslationManagement.Services.DTO
{
    public class TranslatorJobDto
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string OriginalContent { get; set; }
        public string TranslatedContent { get; set; }
        public double Price { get; set; }
        public string Status { get; set; }
    }
}
