using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TranslationManagement.Data.Entities;

namespace TranslationManagement.Repository.Contracts
{
    public interface ITranslatorStatusRepository
    {
        IQueryable<TranslatorStatus> GetTranslatorStatuses();
        Task<TranslatorStatus> GetTranslationStatusByIdAsync(int id);
    }
}
