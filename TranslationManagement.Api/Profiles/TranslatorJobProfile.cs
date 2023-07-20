using AutoMapper;
using System.Reflection.Metadata;
using TranslationManagement.Data.Entities;
using TranslationManagement.Services.DTO;

namespace TranslationManagement.Api.Profiles
{
    public class TranslatorJobProfile : Profile
    {
        public TranslatorJobProfile()
        {
            CreateMap<TranslationJob, TranslatorJobDto>()
            .ForMember(d => d.Status, opt => opt.MapFrom(src => src.TranslationJobStatus != null ? src.TranslationJobStatus.Status : string.Empty));

            CreateMap<CreateTranslatorJobRequestDto, TranslationJob>()
            .ForMember(d => d.TranslationJobStatusId, opt => opt.MapFrom(src => src.Status))
            .ForPath(d => d.TranslatorId, opt => opt.Ignore());
        }
    }
}
