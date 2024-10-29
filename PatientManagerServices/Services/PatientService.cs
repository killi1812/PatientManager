using Microsoft.EntityFrameworkCore;
using PatientManagerServices.Models;

namespace PatientManagerServices.Services;

public interface IPatientService
{
    Task<List<Patient>> GetPatients(string query = null);
    Task<Patient> GetPatient(Guid guid);
    Task<Patient> CreatePatient(Patient newPatient);
    Task DeletePatient(Guid guid);
    Task<Patient> UpdatePatient(Guid guid, Patient newPatient);
}

public class PatientService : IPatientService
{

    private readonly PmDbContext _context;

    public PatientService(PmDbContext context)
    {
        _context = context;
    }

    public async Task<List<Patient>> GetPatients(string query = null)
    {
        if (String.IsNullOrEmpty(query))
        {
            var patients = await _context.Patients.ToListAsync();
            return patients;
        }

        throw new NotImplementedException();
    }

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