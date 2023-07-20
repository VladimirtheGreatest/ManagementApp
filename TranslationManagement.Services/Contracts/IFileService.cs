using Microsoft.AspNetCore.Http;
using TranslationManagement.Services.DTO;

namespace TranslationManagement.Services.Contracts
{
    public interface IFileService
    {
        T ProcessFile<T>(IFormFile file, T dto, string customer) where T : CreateTranslatorJobRequestDto;
    }
}