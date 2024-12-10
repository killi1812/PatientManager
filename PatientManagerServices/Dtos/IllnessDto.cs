namespace PatientManagerServices.Dtos;

public class IllnessDto 
{
    public string Guid { get; set; }
    public string Name { get; set; }
    public string Start { get; set; }
    public string? End { get; set; }
    //public MedicalHistoryDto  MedicalHistory { get; set; }
    public List<PrescriptionDto> Prescriptions { get; set; }
}

