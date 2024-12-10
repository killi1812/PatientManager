using System.Runtime.InteropServices;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PatientManagerServices.Extras;
using PatientManagerServices.Models;

namespace PatientManagerServices.Services;

public interface IPatientService
{
    Task<List<Patient>> GetAll(string q, int page, int n);
    Task<Patient> Get(Guid guid);
    Task<Patient> Create(Patient newPatient);
    Task Delete(Guid guid);
    Task<Patient> Update(Guid guid, Patient newPatient);
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

    public async Task<List<Patient>> GetAll(string q, int page, int n)
    {
        if (page < 1)
            throw new Exception("Page can't be less then 1");
        
        IQueryable<Patient> qPatients = _context.Patients
            .AsNoTracking()
            .Include(p => p.MedicalHistory)
            .OrderBy(p => p.NameNormalized);
    
        if (!String.IsNullOrEmpty(q))
        {
            var tmp = qPatients.Where(p => p.Mbo.Contains(q));
            if (!tmp.Any())
                qPatients = qPatients.Where(p => p.NameNormalized.Contains(q.ToLower()));
            else
                qPatients = tmp;
        }
        return await qPatients
            .Skip(n * (page - 1))
            .Take(n)
            .ToListAsync();
    }

    public async Task<Patient> Get(Guid guid)
    {
        var patient = await _context.Patients
            .AsNoTracking()
            .Include(p => p.MedicalHistory)
            .FirstOrDefaultAsync(p => p.Guid == guid);
        if (patient == null) throw new NotFoundException($"Patient with guid {guid} was not found");
        return patient;
    }

    public async Task<Patient> Create(Patient newPatient)
    {
        var mh = new MedicalHistory();
        await _context.MedicalHistories.AddAsync(mh);

        newPatient.MedicalHistory = mh;
        await _context.Patients.AddAsync(newPatient);

        await _context.SaveChangesAsync();

        return await _context.Patients.AsNoTracking().FirstOrDefaultAsync(p => p.Guid == newPatient.Guid) ??
               throw new NotFoundException($"Patient with guid {newPatient.Guid} was not found");
    }

    public async Task Delete(Guid guid)
    {
        var patient = await _context.Patients
            .FirstOrDefaultAsync(p => p.Guid == guid);
        if (patient == null) throw new NotFoundException($"Patient with guid {guid} was not found");
        _context.Patients.Remove(patient);
        await _context.SaveChangesAsync();
    }

    public async Task<Patient> Update(Guid guid, Patient newPatient)
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