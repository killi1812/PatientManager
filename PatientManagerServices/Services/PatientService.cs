using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PatientManagerServices.Extras;
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
    private readonly IMapper _mapper;

    public PatientService(PmDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
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

    public async Task<Patient> GetPatient(Guid guid)
    {
        var patient = await _context.Patients
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Guid == guid);
        if (patient == null) throw new NotFoundException($"Patient with guid {guid} was not found");
        return patient;
    }

    public async Task<Patient> CreatePatient(Patient newPatient)
    {
        await _context.Patients.AddAsync(newPatient);
        await _context.SaveChangesAsync();
        return await _context.Patients.AsNoTracking().FirstOrDefaultAsync(p => p.Guid == newPatient.Guid) ??
               throw new NotFoundException($"Patient with guid {newPatient.Guid} was not found");
    }

    public async Task DeletePatient(Guid guid)
    {
        var patient = await _context.Patients
            .FirstOrDefaultAsync(p => p.Guid == guid);
        if (patient == null) throw new NotFoundException($"Patient with guid {guid} was not found");
        _context.Patients.Remove(patient);
        await _context.SaveChangesAsync();
    }

    public async Task<Patient> UpdatePatient(Guid guid, Patient newPatient)
    {
        //TODO fix creates new patient
        var patient = await _context.Patients
            .FirstOrDefaultAsync(p => p.Guid == guid);
        if (patient == null) throw new NotFoundException($"Patient with guid {guid} was not found");
        patient = _mapper.Map<Patient>(newPatient);
        //TODO add update when new props added
        _context.Patients.Update(patient);
        await _context.SaveChangesAsync();

        return await _context.Patients.AsNoTracking().FirstOrDefaultAsync(p => p.Guid == guid) ??
               throw new NotFoundException($"Patient with guid {guid} was not found");
    }
}