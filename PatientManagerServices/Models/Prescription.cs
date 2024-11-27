using System.ComponentModel.DataAnnotations.Schema;

namespace PatientManagerServices.Models;

public class Prescription
{
    public int Id { get; set; }
    public Guid Guid { get; set; } = System.Guid.NewGuid();
    public string Name { get; set; }
    public DateOnly Date { get; set; }
    [ForeignKey("MedicalHistory")]
    public int MedicalHistoryId { get; set; }
    public MedicalHistory MedicalHistory { get; set; }
    [ForeignKey("Illness")] 
    public int? IllnessId { get; set; }
    public Illness? Illness { get; set; }
}