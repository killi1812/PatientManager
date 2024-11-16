namespace PatientManagerServices.Dtos;

public class NewExaminationDto
{
    public string MedicalHistoryGuid { get; set; }
    public string? IllnessGuid { get; set; }
    public string ExaminationTime { get; set; }
    public int Type { get; set; }
}