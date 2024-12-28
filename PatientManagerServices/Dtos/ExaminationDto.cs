namespace PatientManagerServices.Dtos;

public class ExaminationDto
{
    public string Guid { get; set; }
    public string ExaminationTime { get; set; }
    public int Type { get; set; }
    public IllnessDto? Illness { get; set; }
}

