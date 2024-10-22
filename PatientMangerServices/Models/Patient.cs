namespace PatientMangerServices.Models;

public class Patient
{
    public int Id;
    public Guid Guid { get; set; } = System.Guid.NewGuid();
    public string Name { get; set; }
    public string Surname { get; set; }
}