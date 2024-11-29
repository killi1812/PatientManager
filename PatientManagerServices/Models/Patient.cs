using System.ComponentModel.DataAnnotations.Schema;

namespace PatientManagerServices.Models;

public class Patient
{
    public int Id { get; set; }
    public Guid Guid { get; set; } = System.Guid.NewGuid();
    public string Name { get; set; }
    public string Surname { get; set; }
    public string NameNormalized { get; set; }
    public string Mbo { get; set; }
    [ForeignKey("MedicalHistory")]
    public int MedicalHistoryId { get; set; }
    public MedicalHistory MedicalHistory { get; set; }
    
    [ForeignKey("Doctor")]
    public int? DoctorId { get; set; }
    public Doctor? Doctor { get; set; }
}