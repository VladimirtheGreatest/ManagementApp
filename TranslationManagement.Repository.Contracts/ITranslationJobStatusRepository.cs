using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TranslationManagement.Data.Entities;

namespace TranslationManagement.Repository.Contracts
{
    public interface ITranslationJobStatusRepository
    {
        IQueryable<TranslationJobStatus> GetTranslationJobStatuses();
        Task<TranslationJobStatus> GetTranslationJobStatusByIdAsync(int id);
    }
}
