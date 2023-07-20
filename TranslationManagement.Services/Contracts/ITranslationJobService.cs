using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TranslationManagement.Services.DTO;

namespace TranslationManagement.Services.Contracts
{
    public interface ITranslationJobService
    {
        Task<List<TranslatorJobDto>> GetJobs();
        Task<int> AddJob(CreateTranslatorJobRequestDto translationJob);
        Task UpdateTranslatorJobStatus(int translationJobId, int newStatus);
        CreateTranslatorJobRequestDto ProcessJobFile(IFormFile file, string customer);
        Task AssignTranslatorJob(int translatorId, int jobId);
        Task<List<TranslatorJobDto>> GetTranslatorJobsById(int translatorId);
    }
}
