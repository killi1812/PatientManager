using PatientManagerServices.Models;

namespace PatientManagerServices.Services;

public interface IPatientServices
{
    Task<Patient> GetPatient(Guid guid);
    Task<Patient> CreatePatient(Patient newPatient);
    Task DeletePatient(Guid guid);
    Task<Patient> UpdatePatient(Guid guid, Patient newPatient);
}

public class PatientServices : IPatientServices
{
    public Task<Patient> GetPatient(Guid guid)
    {
        throw new NotImplementedException();
    }

    public Task<Patient> CreatePatient(Patient newPatient)
    {
        throw new NotImplementedException();
    }

    public Task DeletePatient(Guid guid)
    {
        throw new NotImplementedException();
    }

    public Task<Patient> UpdatePatient(Guid guid, Patient newPatient)
    {
        throw new NotImplementedException();
    }
}