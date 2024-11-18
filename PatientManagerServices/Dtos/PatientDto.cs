namespace PatientManagerServices.Dtos;

public class PatientDto
{
    public string Guid { get; set; } 
    public string Name { get; set; }
    public string Surname { get; set; }
    public string MedicalHistoryGuid { get; set; }
}