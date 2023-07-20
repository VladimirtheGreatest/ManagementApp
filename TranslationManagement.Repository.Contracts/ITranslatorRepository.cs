using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TranslationManagement.Data.Entities;

namespace TranslationManagement.Repository.Contracts
{
    public interface ITranslatorRepository
    {
        IQueryable<TranslatorModel> GetTranslators();
        Task<TranslatorModel> CreateTranslatorAsync(TranslatorModel translatorModel);
        Task<TranslatorModel> GetTranslatorByIdOrNullForUpdateAsync(int translatorId);
    }
}
