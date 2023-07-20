using System.Collections.Generic;
using System.Threading.Tasks;
using TranslationManagement.Data.Entities;
using TranslationManagement.Services.DTO;

namespace TranslationManagement.Services.Contracts
{
    public interface ITranslatorManagementService
    {
        Task<bool> AddTranslator(CreateTranslatorRequestDto translator);
        Task<List<TranslatorDto>> GetTranslators();
        Task<List<TranslatorDto>> GetTranslatorsByName(string name);
        Task UpdateTranslatorStatus(int Translator, int newStatus);
    }
}