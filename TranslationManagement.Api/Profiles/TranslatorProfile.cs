using AutoMapper;
using TranslationManagement.Data.Entities;
using TranslationManagement.Services.DTO;

namespace TranslationManagement.Api.Profiles
{
    public class TranslatorProfile : Profile
    {
        public TranslatorProfile()
        {
            CreateMap<TranslatorModel, TranslatorDto>()
            .ForMember(d => d.Status, opt => opt.MapFrom(src => src.TranslatorStatus != null ? src.TranslatorStatus.Status : string.Empty));

            CreateMap<CreateTranslatorRequestDto, TranslatorModel>()
            .ForMember(d => d.TranslatorStatusId, opt => opt.MapFrom(src => src.Status));
        }
    }
}
