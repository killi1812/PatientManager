namespace PatientManagerServices.Models;

public class Doctor
{
       public int Id { get; set; }
       public Guid Guid { get; set; } = System.Guid.NewGuid();
       public string Name { get; set; }
       public string Surname { get; set; }
       public string Password { get; set; }
       public string Email { get; set; }
       public string Phone { get; set; }
       public List<Patient> Patients { get; set; } 
}