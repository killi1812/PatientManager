using System.Runtime.InteropServices;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PatientManagerServices.Extras;
using PatientManagerServices.Models;

namespace PatientManagerServices.Services;

public interface IPatientService
{
    Task<List<Patient>> GetPatients(string q, int page, int n);
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

    public async Task<List<Patient>> GetPatients(string q, int page, int n)
    {
        if (page < 1)
            throw new Exception("Page can't be less then 1");

        IQueryable<Patient> qPatients = _context.Patients
            .AsNoTracking()
            .OrderBy(p => p.Surname)
            .ThenBy(p => p.Name);

        if (!String.IsNullOrEmpty(q))
        {
            qPatients = qPatients .Where(p => (p.Surname + " "+ p.Name).ToLower().Contains(q.ToLower()));
        }

        return await qPatients
            .Skip(n * (page - 1))
            .Take(n)
            .ToListAsync();
    }

    public async Task<Patient> GetPatient(Guid guid)
    {
        var patient = await _context.Patients
            .AsNoTracking()
            .Include(p => p.MedicalHistory)
            .FirstOrDefaultAsync(p => p.Guid == guid);
        if (patient == null) throw new NotFoundException($"Patient with guid {guid} was not found");
        return patient;
    }

    public async Task<Patient> CreatePatient(Patient newPatient)
    {
        var mh = new MedicalHistory();
        await _context.MedicalHistories.AddAsync(mh);

        newPatient.MedicalHistory = mh;
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
        var patient = await _context.Patients
            .FirstOrDefaultAsync(p => p.Guid == guid);
        if (patient == null) throw new NotFoundException($"Patient with guid {guid} was not found");
        _mapper.Map(newPatient, patient);
        await _context.SaveChangesAsync();

        return await _context.Patients.AsNoTracking().FirstOrDefaultAsync(p => p.Guid == guid) ??
               throw new NotFoundException($"Patient with guid {guid} was not found");
    }
}