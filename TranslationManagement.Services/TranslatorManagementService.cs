using AutoMapper;
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
    public class TranslatorManagementService : ITranslatorManagementService
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;
        public TranslatorManagementService(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        public async Task<List<TranslatorDto>> GetTranslators()
        {
            var entities = await _repository.Translators.GetTranslators().AsNoTracking().ToListAsync();

            return entities.MapEntitiesWithDto<TranslatorModel, TranslatorDto>(_mapper);
        }

        public async Task<List<TranslatorDto>> GetTranslatorsByName(string name)
        {
            var entities = await _repository.Translators.GetTranslators().AsNoTracking().Where(x => x.Name == name)
                .Include(t => t.TranslatorStatus).Select(t => new TranslatorModel
            {
                Id = t.Id,
                Name = t.Name,
                CreditCardNumber = t.CreditCardNumber,
                HourlyRate = t.HourlyRate,
                TranslatorStatus = t.TranslatorStatus,
            }).ToListAsync();
            return entities.MapEntitiesWithDto<TranslatorModel, TranslatorDto>(_mapper);
        }

        public async Task<bool> AddTranslator(CreateTranslatorRequestDto translator)
        {
            var translationStatus = await _repository.TranslatorsStatuses.GetTranslationStatusByIdAsync(translator.Status);
            if (translationStatus == null) { throw new ArgumentException($"unknown status id : {translator.Status}"); }

            var created = await _repository.Translators.CreateTranslatorAsync(_mapper.Map<TranslatorModel>(translator));
            return created != null;
        }


        public async Task UpdateTranslatorStatus(int Translator, int newStatus)
        {
            var translationStatus = await _repository.TranslatorsStatuses.GetTranslatorStatuses().FirstOrDefaultAsync(s => s.Id == newStatus);
            if (translationStatus == null) { throw new ArgumentException($"unknown status id : {newStatus}"); }

            var translator = await _repository.Translators.GetTranslatorByIdOrNullForUpdateAsync(Translator);
            if (translator == null) { throw new Exception($"unknown translator id: {Translator}"); }

            translator.TranslatorStatus = translationStatus;
            await _repository.SaveAsync();
        }
    }
}
