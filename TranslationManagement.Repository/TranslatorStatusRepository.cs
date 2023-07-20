using TranslationManagement.Data.Entities;
using TranslationManagement.Data;
using TranslationManagement.Repository.Contracts;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TranslationManagement.Repository
{
    public class TranslatorStatusRepository : RepositoryBase<AppDbContext, TranslatorStatus>, ITranslatorStatusRepository
    {
        public TranslatorStatusRepository(AppDbContext repositoryContext) : base(repositoryContext)
        {
                
        }
        public IQueryable<TranslatorStatus> GetTranslatorStatuses()
        {
            return _context.TranslatorStatuses.AsQueryable();
        }

        public async Task<TranslatorStatus> GetTranslationStatusByIdAsync(int id) =>
             await GetTranslatorStatuses().AsNoTracking().FirstOrDefaultAsync(s => s.Id == id);
    }
}
