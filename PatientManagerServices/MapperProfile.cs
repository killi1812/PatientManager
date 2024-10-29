using AutoMapper;
using PatientManagerServices.Dtos;
using PatientManagerServices.Models;

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
    }
}