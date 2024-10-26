using PatientManagerServices.Models.Enums;

namespace PatientManagerServices.Models;

public class Examination
{
    public int Id { get; set; }
    public Guid Guid { get; set; } = System.Guid.NewGuid();

    public DateTime ExaminationTime { get; set; }
    public ExaminationType Type { get; set; }

    public int PatientId { get; set; }
    public Patient Patient { get; set; }

    public int? IllnessId { get; set; }
    public Illness? Illness { get; set; }
}