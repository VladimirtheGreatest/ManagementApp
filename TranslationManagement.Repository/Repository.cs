using System.Threading.Tasks;
using TranslationManagement.Data;
using TranslationManagement.Repository.Contracts;

namespace TranslationManagement.Repository
{
    public class Repository : IRepository
    {
        private AppDbContext _repositoryContext;

        private TranslatorJobRepository _translationJobRepository;

        private TranslatorRepository _translatorRepository;

        private TranslatorStatusRepository _translatorStatusesRepository;

        private TranslationJobStatusRepository _translationJobStatusesRepository;

        public Repository(AppDbContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }

        public ITranslatorJobRepository TranslationJobs
        {
            get
            {
                if (_translationJobRepository is null)
                    _translationJobRepository = new TranslatorJobRepository(_repositoryContext);

                return _translationJobRepository;
            }
        }

        public ITranslatorRepository Translators
        {
            get
            {
                if (_translatorRepository is null)
                    _translatorRepository = new TranslatorRepository(_repositoryContext);

                return _translatorRepository;
            }
        }

        public ITranslatorStatusRepository TranslatorsStatuses
        {
            get
            {
                if (_translatorStatusesRepository is null)
                    _translatorStatusesRepository = new TranslatorStatusRepository(_repositoryContext);

                return _translatorStatusesRepository;
            }
        }

        public ITranslationJobStatusRepository TranslationJobStatuses
        {
            get
            {
                if (_translationJobStatusesRepository is null)
                    _translationJobStatusesRepository = new TranslationJobStatusRepository(_repositoryContext);

                return _translationJobStatusesRepository;
            }
        }


        public async Task SaveAsync() => await _repositoryContext.SaveChangesAsync(true);
    }
}
