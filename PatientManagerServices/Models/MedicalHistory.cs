using System.ComponentModel.DataAnnotations.Schema;

namespace PatientManagerServices.Models;

public class MedicalHistory
{
    public int Id { get; set; }
    public Guid Guid { get; set; } = System.Guid.NewGuid();
    [ForeignKey("Patient")]
    public int? PatientId { get; set; }
    public Patient? Patient { get; set; }
     
    public List<Illness> PastIlnesses { get; set; }
}