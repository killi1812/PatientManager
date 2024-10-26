namespace PatientManagerServices.Models;

public class Illness
{
    public int Id { get; set; }
    public Guid Guid { get; set; } = System.Guid.NewGuid();
    public string Name { get; set; }
    public DateOnly Start { get; set; }
    public DateOnly? End { get; set; }
    public List<Examination> Examinations { get; set; }
}