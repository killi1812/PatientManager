using AutoMapper;
using Microsoft.EntityFrameworkCore.Design.Internal;
using PatientManagerServices.Dtos;
using PatientManagerServices.Enums;
using PatientManagerServices.Models;

namespace PatientManagerServices;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<Patient, Patient>()
            .ForMember(dest => dest.Guid, opt => opt.Ignore())
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.MedicalHistoryId, opt => opt.Ignore())
            .ForMember(dest => dest.MedicalHistory, opt => opt.Ignore());
        CreateMap<Patient, PatientDto>()
            .ForMember(dest => dest.Guid, opt => opt.MapFrom(src => src.Guid.ToString()))
            .ForMember(dest => dest.MedicalHistoryGuid, opt => opt.MapFrom(src => src.MedicalHistory.Guid));
        CreateMap<PatientDto, Patient>()
            .ForMember(dest => dest.Guid, opt => opt.Ignore())
            .ForMember(dest => dest.NameNormalized,
                opt => opt.MapFrom(src => (src.Surname + " " + src.Name).ToLower()));

        CreateMap<Illness, Illness>()
            .ForMember(dest => dest.Guid, opt => opt.Ignore())
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.MedicalHistory, opt => opt.Ignore())
            .ForMember(dest => dest.MedicalHistoryId, opt => opt.Ignore())
            .ForMember(dest => dest.Examinations, opt => opt.Ignore());
        CreateMap<Illness, IllnessDto>()
            .ForMember(dest => dest.Guid, opt => opt.MapFrom(src => src.Guid.ToString()))
            .ForMember(dest => dest.MedicalHistoryGuid, opt => opt.MapFrom(src => src.MedicalHistory.Guid));
        CreateMap<IllnessDto, Illness>()
            .ForMember(dest => dest.Guid, opt => opt.Ignore())
            .ForMember(dest => dest.Examinations, opt => opt.Ignore())
            .ForMember(dest => dest.Start, opt => opt.MapFrom(src => DateOnly.Parse(src.Start)))
            .ForMember(dest => dest.End, opt =>
            {
                opt.PreCondition(src => src.End != null);
                opt.MapFrom(src => DateOnly.Parse(src.End!));
            });

        CreateMap<Examination, Examination>()
            .ForMember(dest => dest.Guid, opt => opt.Ignore())
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.MedicalHistory, opt => opt.Ignore())
            .ForMember(dest => dest.MedicalHistoryId, opt => opt.Ignore());
        CreateMap<Examination, ExaminationDto>()
            .ForMember(dest => dest.Guid, opt => opt.MapFrom(src => src.Guid.ToString()));
        CreateMap<ExaminationDto, Examination>()
            .ForMember(dest => dest.Guid, opt => opt.Ignore());
        CreateMap<NewExaminationDto, Examination>()
            .ForMember(dest => dest.Guid, opt => opt.Ignore())
            //TODO this mapps time wrong but saves to db successfuly so yay
            .ForMember(dest => dest.ExaminationTime,
                opt => opt.MapFrom(src => DateTime.Parse(src.ExaminationTime).ToUniversalTime()))
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => (ExaminationType)src.Type));

        CreateMap<MedicalHistory, MedicalHistoryDto>()
            .ForMember(dest => dest.Guid, opt => opt.MapFrom(mh => mh.Guid.ToString()));

        CreateMap<Prescription, Prescription>()
            .ForMember(dest => dest.Guid, opt => opt.Ignore())
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            //TODO check if needs to be ignored  
            .ForMember(dest => dest.MedicalHistory, opt => opt.Ignore())
            .ForMember(dest => dest.MedicalHistoryId, opt => opt.Ignore())
            .ForMember(dest => dest.Illness, opt => opt.Ignore())
            .ForMember(dest => dest.IllnessId, opt => opt.Ignore());
        CreateMap<Prescription, PrescriptionDto>()
            .ForMember(dest => dest.Guid, opt => opt.MapFrom(src => src.Guid.ToString()))
            .ForMember(dest => dest.MedicalHistoryGuid, opt => opt.MapFrom(src => src.MedicalHistory.Guid.ToString()))
            .ForMember(dest => dest.IllnessGuid, opt => opt.MapFrom(src => src.Illness.Guid.ToString()));

        CreateMap<PrescriptionDto, Prescription>()
            .ForMember(dest => dest.Guid, opt => opt.Ignore())
            .ForMember(dest => dest.Date, opt => opt.MapFrom(src => DateOnly.Parse(src.Date)));

        CreateMap<Doctor, Doctor>()
            .ForMember(dest => dest.Guid, opt => opt.Ignore())
            .ForMember(dest => dest.Id, opt => opt.Ignore());
        CreateMap<Doctor, DoctorDto>()
            .ForMember(dest => dest.Guid, opt => opt.MapFrom(src => src.Guid.ToString()));
        CreateMap<DoctorDto, Doctor>()
            .ForMember(dest => dest.Guid, opt => opt.Ignore());

        CreateMap<Doctor, DoctorDetailsDto>()
            .ForMember(dest => dest.Guid, opt => opt.MapFrom(src => src.Guid.ToString()));
        CreateMap<DoctorDetailsDto, Doctor>()
            .ForMember(dest => dest.Guid, opt => opt.Ignore())
            .ForMember(dest => dest.Patients, opt => opt.Ignore());

        CreateMap<NewDoctorDto, Doctor>()
            .ForMember(dest => dest.Guid, opt => opt.Ignore())
            .ForMember(dest => dest.Password, opt => opt.Ignore());
    }
}