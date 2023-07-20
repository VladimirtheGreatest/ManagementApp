using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TranslationManagement.Data.Entities;
using TranslationManagement.Data;
using TranslationManagement.Repository.Contracts;
using Microsoft.EntityFrameworkCore;

namespace TranslationManagement.Repository
{
    public class TranslatorJobRepository : RepositoryBase<AppDbContext, TranslationJob>, ITranslatorJobRepository
    {
        public TranslatorJobRepository(AppDbContext repositoryContext) : base(repositoryContext)
        {

        }

        public IQueryable<TranslationJob> GetTranslatorJobs()
        {
            return _context.TranslationJobs.AsQueryable();
        }

        public async Task<TranslationJob> CreateTranslationJobAsync(TranslationJob translatorJob)
        {
            return await ExecuteWithExceptionHandling(async () =>
            {
                await Add(translatorJob);
                await _context.SaveChangesAsync();
                int translatorJobId = (int)translatorJob.Id;
                return await _context.TranslationJobs.AsNoTracking().FirstOrDefaultAsync(x => x.Id == translatorJobId);
            });
        }
        public async Task<TranslationJob> GetTranslatorJobByIdOrNullForUpdateAsync(int translatorJobId)
        {
            return await ExecuteWithExceptionHandling(async () =>
            {
                return await _context.TranslationJobs.Include(t => t.TranslationJobStatus).Include(t => t.Translator).FirstOrDefaultAsync(x => x.Id == translatorJobId);
            });
        }
    }
}
