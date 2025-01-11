using System.ComponentModel.DataAnnotations.Schema;

namespace PatientManagerServices.Models;

public class File
{
    public int id { get; set; }
    public string FileName { get; set; }
    public string FileGuid { get; set; }
    
    [ForeignKey("Examination")]
    public int ExaminationId { get; set; }
    public Examination Examination { get; set; }
}