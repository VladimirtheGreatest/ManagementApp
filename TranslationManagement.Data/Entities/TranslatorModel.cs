using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TranslationManagement.Data.Entities
{
    public class TranslatorModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string HourlyRate { get; set; }
        public string CreditCardNumber { get; set; }
        public int? TranslatorStatusId { get; set; }
        public TranslatorStatus TranslatorStatus { get; set; }
        public ICollection<TranslationJob> TranslatorJobs { get; set; }
    }
}
