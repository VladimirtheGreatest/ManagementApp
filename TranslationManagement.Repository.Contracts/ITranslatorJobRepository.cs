using System.Linq;
using System.Threading.Tasks;
using TranslationManagement.Data.Entities;

namespace TranslationManagement.Repository.Contracts
{
    public interface ITranslatorJobRepository
    {
        IQueryable<TranslationJob> GetTranslatorJobs();
        Task<TranslationJob> CreateTranslationJobAsync(TranslationJob translatorJob);
        Task<TranslationJob> GetTranslatorJobByIdOrNullForUpdateAsync(int translatorJobId);
    }
}
