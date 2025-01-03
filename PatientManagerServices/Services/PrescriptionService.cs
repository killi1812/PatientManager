using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PatientManagerServices.Extras;
using PatientManagerServices.Models;

namespace PatientManagerServices.Services;

public interface IPrescriptionService
{
    public Task<Prescription> Get(Guid guid);
    public Task Delete(Guid guid);
    public Task<Prescription> Create(Guid medicalHistoryGuid, Guid? illnessGuid, Prescription prescription);
    public Task<Prescription> Update(Guid guid, Prescription prescription);
    public Task<List<Prescription>> GetAll(Guid MedicalHistoryGuid);
    public Task<List<Prescription>> GetForIllness(Guid IllnessGuid);
}

public class PrescriptionService : IPrescriptionService
{
    private readonly PmDbContext _context;
    private readonly IMapper _mapper;

    public PrescriptionService(PmDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Prescription> Get(Guid guid)
    {
        var prescription = await _context.Prescriptions
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Guid == guid);

        if (prescription == null)
            throw new NotFoundException($"Prescription with guid: {guid} not found");

        return prescription;
    }

    public async Task Delete(Guid guid)
    {
        var prescription = await _context.Prescriptions
            .FirstOrDefaultAsync(p => p.Guid == guid);
        if (prescription == null)
            throw new NotFoundException($"Prescription with guid: {guid} not found");

        _context.Remove(prescription);
        await _context.SaveChangesAsync();
    }

    public async Task<Prescription> Create(Guid medicalHistoryGuid, Guid? illnessGuid, Prescription prescription)
    {
        var medicalHistory = await _context.MedicalHistories.FirstOrDefaultAsync(mh => mh.Guid == medicalHistoryGuid);
        if (medicalHistory == null)
            throw new NotFoundException($"Medical history with guid {medicalHistoryGuid} was not found");

        prescription.MedicalHistoryId = medicalHistory.Id;

        if (illnessGuid.HasValue)
        {
            var illness = await _context.Illnesses.FirstOrDefaultAsync(i => i.Guid == illnessGuid.Value);
            if (illness == null)
                throw new NotFoundException($"Illness with guid {illnessGuid.Value} was not found");
            prescription.IllnessId = illness.Id;
        }

        await _context.Prescriptions.AddAsync(prescription);
        await _context.SaveChangesAsync();
        return prescription;
    }

    public async Task<Prescription> Update(Guid guid, Prescription prescription)
    {
        var pOld = await _context.Prescriptions
            .FirstOrDefaultAsync(p => p.Guid == guid);
        if (pOld == null)
            throw new NotFoundException($"Prescription with guid: {guid} not found");

         _ = _mapper.Map( prescription,pOld);
        await _context.SaveChangesAsync();

        return await _context.Prescriptions.AsNoTracking().FirstOrDefaultAsync(p => p.Guid == guid) ??
               throw new NotFoundException($"Prescription with guid: {guid} not found");
    }

    public async Task<List<Prescription>> GetAll(Guid MedicalHistoryGuid)
    {
        var prescriptions = await _context.Prescriptions
            .AsNoTracking()
            .Where(p => p.MedicalHistory.Guid == MedicalHistoryGuid)
            .Include(p => p.MedicalHistory)
            .Include(p => p.Illness)
            .ToListAsync();

        return prescriptions;
    }

    public async Task<List<Prescription>> GetForIllness(Guid IllnessGuid)
    {
        var prescriptions = await _context.Prescriptions
            .AsNoTracking()
            .Where(p => p.Illness.Guid == IllnessGuid)
            .Include(p => p.MedicalHistory)
            .Include(p => p.Illness)
            .ToListAsync();
        return prescriptions;
    }
}