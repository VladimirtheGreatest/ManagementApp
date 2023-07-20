using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TranslationManagement.Data.Entities
{
    public class TranslationJob
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string OriginalContent { get; set; }
        public string TranslatedContent { get; set; }
        public double Price { get; set; }
        public int? TranslationJobStatusId { get; set; }
        public TranslationJobStatus TranslationJobStatus { get; set; }
        public int? TranslatorId { get; set; }
        public TranslatorModel Translator { get; set; }
    }
}
