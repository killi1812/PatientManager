namespace PatientManagerServices.Dtos;

public class MedicalHistoryDto 
{
    public string Guid { get; set; }
    public List<IllnessDto> Illnesss { get; set; }
    public List<ExaminationDto> Examinations { get; set; }
}
