using System.ComponentModel.DataAnnotations.Schema;

namespace PatientManagerServices.Models;

public class Illness
{
    public int Id { get; set; }
    public Guid Guid { get; set; } = System.Guid.NewGuid();
    public string Name { get; set; }
    public DateOnly Start { get; set; }
    public DateOnly? End { get; set; }
    [ForeignKey("MedicalHistory")]
    public int MedicalHistoryId { get; set;}
    public MedicalHistory MedicalHistory { get; set; }
    public List<Examination> Examinations { get; set; }
}