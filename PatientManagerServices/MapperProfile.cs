using AutoMapper;
using PatientManagerServices.Dtos;
using PatientManagerServices.Models;
using PatientManagerServices.Models.Enums;

namespace PatientManagerServices;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<Patient, Patient>()
            .ForMember(dest => dest.Guid, opt => opt.Ignore())
            .ForMember(dest => dest.Id, opt => opt.Ignore());
        CreateMap<Patient, PatientDto>()
            .ForMember(dest => dest.Guid, opt => opt.MapFrom(src => src.Guid.ToString()));
        CreateMap<PatientDto,Patient>()
            .ForMember(dest => dest.Guid,opt => opt.Ignore());
        
        CreateMap<Illness,Illness>()
            .ForMember(dest => dest.Guid, opt => opt.Ignore())
            .ForMember(dest => dest.Id, opt => opt.Ignore());
        CreateMap<Illness, IllnessDto>()
            .ForMember(dest => dest.Guid, opt => opt.MapFrom(src => src.Guid.ToString()));
        CreateMap<IllnessDto, Illness>()
            .ForMember(dest => dest.Guid, opt => opt.Ignore());
            
        CreateMap<Examination,Examination>()
            .ForMember(dest => dest.Guid, opt => opt.Ignore())
            .ForMember(dest => dest.Id, opt => opt.Ignore());
        CreateMap<Examination,ExaminationDto>()
            .ForMember(dest => dest.Guid, opt => opt.MapFrom(src => src.Guid.ToString()));
        CreateMap<ExaminationDto, Examination>()
            .ForMember(dest => dest.Guid, opt => opt.Ignore());
        CreateMap<NewExaminationDto, Examination>()
            .ForMember(dest => dest.Guid, opt => opt.Ignore())
            .ForMember(dest => dest.ExaminationTime, opt => opt.MapFrom(src => DateTime.Parse(src.ExaminationTime)))
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => (ExaminationType)src.Type));

        CreateMap<MedicalHistory, MedicalHistoryDto>()
            .ForMember(dest => dest.Guid, opt => opt.MapFrom(mh => mh.Guid.ToString()));
        
    }
}