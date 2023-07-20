using System.Threading.Tasks;

namespace TranslationManagement.Repository.Contracts
{
    public interface IRepository
    {
        ITranslatorJobRepository TranslationJobs { get; }
        ITranslatorRepository Translators { get; }
        ITranslatorStatusRepository TranslatorsStatuses { get; }
        ITranslationJobStatusRepository TranslationJobStatuses { get; }
        Task SaveAsync();
    }
}
