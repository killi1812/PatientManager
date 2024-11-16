using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PatientManagerServices.Models;

public class Patient
{
    public int Id { get; set; }
    public Guid Guid { get; set; } = System.Guid.NewGuid();
    public string Name { get; set; }
    public string Surname { get; set; }
    [ForeignKey("MedicalHistory")]
    public int MedicalHistoryId { get; set; }
    public MedicalHistory MedicalHistory { get; set; }
}