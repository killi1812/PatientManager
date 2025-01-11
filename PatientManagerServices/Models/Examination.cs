using System.ComponentModel.DataAnnotations.Schema;
using PatientManagerServices.Enums;

namespace PatientManagerServices.Models;

public class Examination
{
    public int Id { get; set; }
    public Guid Guid { get; set; } = System.Guid.NewGuid();
    public DateTime ExaminationTime { get; set; }
    public ExaminationType Type { get; set; }
    [ForeignKey("MedicalHistory")]
    public int MedicalHistoryId { get; set; }
    public MedicalHistory MedicalHistory { get; set; }
    [ForeignKey("Illness")]
    public int? IllnessId { get; set; }
    public Illness? Illness { get; set; }
    
    public List<File> Files { get;set ; }
}