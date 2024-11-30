namespace PatientManagerServices.Dtos;

public class DoctorDetailsDto
{
    public string Guid { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public List<PatientDto> Patients { get; set; }
}