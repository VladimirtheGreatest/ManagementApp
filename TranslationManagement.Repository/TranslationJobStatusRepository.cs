using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TranslationManagement.Data;
using TranslationManagement.Data.Entities;
using TranslationManagement.Repository.Contracts;

namespace TranslationManagement.Repository
{
    public class TranslationJobStatusRepository : RepositoryBase<AppDbContext, TranslationJobStatus>, ITranslationJobStatusRepository
    {
        public TranslationJobStatusRepository(AppDbContext repositoryContext) : base(repositoryContext)
        {
                
        }
        public IQueryable<TranslationJobStatus> GetTranslationJobStatuses()
        {
            return _context.TranslatorJobStatuses.AsQueryable();
        }

        public async Task<TranslationJobStatus> GetTranslationJobStatusByIdAsync(int id) =>
             await GetTranslationJobStatuses().AsNoTracking().FirstOrDefaultAsync(s => s.Id == id);
    }
}
