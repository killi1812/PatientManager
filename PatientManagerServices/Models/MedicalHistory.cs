using System.ComponentModel.DataAnnotations.Schema;

namespace PatientManagerServices.Models;

public class MedicalHistory
{
    public int Id { get; set; }
    public Guid Guid { get; set; } = System.Guid.NewGuid();
    public List<Illness> PastIlnesses { get; set; }
    public List<Examination> Examinations { get; set; }
    public List<Prescription> Prescriptions { get; set; }
}