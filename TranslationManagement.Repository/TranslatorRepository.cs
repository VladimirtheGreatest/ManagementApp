using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using TranslationManagement.Data;
using TranslationManagement.Data.Entities;
using TranslationManagement.Repository.Contracts;

namespace TranslationManagement.Repository
{
    public class TranslatorRepository : RepositoryBase<AppDbContext, TranslatorModel>, ITranslatorRepository
    {
        public TranslatorRepository(AppDbContext repositoryContext) : base(repositoryContext)
        {

        }

        public IQueryable<TranslatorModel> GetTranslators()
        { 
           return _context.Translators.AsQueryable();
        }

        public async Task<TranslatorModel> CreateTranslatorAsync(TranslatorModel translatorModel)
        {
            return await ExecuteWithExceptionHandling(async () =>
            {
                await Add(translatorModel);
                await _context.SaveChangesAsync();
                int translatorId = (int)translatorModel.Id;
                return await _context.Translators.AsNoTracking().FirstOrDefaultAsync(x => x.Id == translatorId);
            });
        }

        public async Task<TranslatorModel> GetTranslatorByIdOrNullForUpdateAsync(int translatorId)
        {
            return await ExecuteWithExceptionHandling(async () =>
            {
                return await _context.Translators.Include(t => t.TranslatorStatus).FirstOrDefaultAsync(x => x.Id == translatorId);
            });
        }
    }
}
