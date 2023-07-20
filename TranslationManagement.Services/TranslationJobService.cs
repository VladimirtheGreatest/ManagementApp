using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TranslationManagement.Data.Entities;
using TranslationManagement.Repository.Contracts;
using TranslationManagement.Services.Contracts;
using TranslationManagement.Services.DTO;
using TranslationManagement.Services.Extensions;

namespace TranslationManagement.Services
{
    public class TranslationJobService : ITranslationJobService
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;
        private readonly IPricingService _pricingService;
        private readonly IFileService _fileService;
        private const int CertifiedTranslator = 2;//should be in config
        public TranslationJobService(IRepository repository, IMapper mapper, IPricingService pricingService, IFileService fileService)
        {
            _repository = repository;
            _mapper = mapper;
            _pricingService = pricingService;
            _fileService = fileService;
        }
        public async Task<List<TranslatorJobDto>> GetJobs()
        {
            var entities = await _repository.TranslationJobs.GetTranslatorJobs().AsNoTracking().ToListAsync();
            return entities.MapEntitiesWithDto<TranslationJob, TranslatorJobDto>(_mapper);
        }

        public async Task<int> AddJob(CreateTranslatorJobRequestDto translationJob)
        {
            translationJob.Price = _pricingService.CalculatePrice(translationJob.OriginalContent.Length);
            var translationJobStatus = await _repository.TranslationJobStatuses.GetTranslationJobStatusByIdAsync(translationJob.Status);
            if (translationJobStatus == null) { throw new ArgumentException($"unknown status id : {translationJob.Status}"); }

            var created = await _repository.TranslationJobs.CreateTranslationJobAsync(_mapper.Map<TranslationJob>(translationJob));
            return created.Id;
        }

        public CreateTranslatorJobRequestDto ProcessJobFile(IFormFile file, string customer)
        {
            var translationJob = _fileService.ProcessFile(file, new CreateTranslatorJobRequestDto(), customer);
            translationJob.Price = _pricingService.CalculatePrice(translationJob.OriginalContent.Length);
            return translationJob;
        }

        public async Task UpdateTranslatorJobStatus(int translationJobId, int newStatus)
        {
            var translationJobStatus = await _repository.TranslationJobStatuses.GetTranslationJobStatuses().FirstOrDefaultAsync(s => s.Id == newStatus);
            if (translationJobStatus == null) { throw new ArgumentException($"unknown status id : {newStatus}"); }

            var translationJob = await _repository.TranslationJobs.GetTranslatorJobByIdOrNullForUpdateAsync(translationJobId);
            if (translationJob == null) { throw new Exception($"unknown translation job id: {translationJobId}"); }

            if (translationJob.Translator == null) { throw new Exception($"this job needs to be assigned to translator first"); }
            //additional requirement only certified translator can do the job
            if (translationJob.Translator.TranslatorStatusId != CertifiedTranslator) { throw new Exception($"only certified translators can do the job"); }

            translationJob.TranslationJobStatus = translationJobStatus;
            await _repository.SaveAsync();
        }

        public async Task AssignTranslatorJob(int translatorId, int jobId)
        {
            var job = await _repository.TranslationJobs.GetTranslatorJobs().Include(t => t.Translator).FirstOrDefaultAsync(s => s.Id == jobId);
            if (job == null) { throw new Exception($"job {jobId}, does not exists"); }

            if (job.Translator != null) { throw new Exception($"job {jobId}, is already assigned to translator with id {translatorId}"); }

            var translator = await _repository.Translators.GetTranslatorByIdOrNullForUpdateAsync(translatorId);
            if (translator == null) { throw new Exception($"you cannot assign translator that doesnt exist"); }

            job.TranslatorId = translatorId;
            await _repository.SaveAsync();
        }

        public async Task<List<TranslatorJobDto>> GetTranslatorJobsById(int translatorId)
        {
            var entities =  await _repository.TranslationJobs.GetTranslatorJobs().Where(x => x.TranslatorId == translatorId).AsNoTracking().ToListAsync();
            return entities.MapEntitiesWithDto<TranslationJob, TranslatorJobDto>(_mapper);
        }
    }
}
