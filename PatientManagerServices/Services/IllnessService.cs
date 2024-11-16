using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PatientManagerServices.Extras;
using PatientManagerServices.Models;

namespace PatientManagerServices.Services;

public interface IIllnessService
{
    public Task<List<Illness>> GetPatientIllnesses(Guid medicalHistoryGuid);
    public Task<Illness> GetIllness(Guid guid);
    public Task<Illness> CreateIllness(Guid medicalHistoryGuid, Illness illness);
    public Task DeleteIllness(Guid guid);
    public Task<Illness> UpdateIllness(Guid guid, Illness illness);
}

public class IllnessService : IIllnessService
{
    private readonly PmDbContext _context;
    private readonly IMapper _mapper;

    public IllnessService(PmDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<Illness>> GetPatientIllnesses(Guid medicalHistoryGuid)
    {
        var illnesses = await _context.MedicalHistories
            . AsNoTracking()
            .Where(mh => mh.Guid == medicalHistoryGuid)
            .SelectMany(mh => mh.PastIlnesses)
            .Include(il => il.MedicalHistory)
            .Include(il => il.Examinations)
            .ToListAsync();
        
        if (illnesses == null)
            throw new NotFoundException($"Medical history with guid: {medicalHistoryGuid} not found");
        
        return illnesses;
    }
    
    public async Task<Illness> GetIllness(Guid guid)
    {
        var illness = await _context.Illnesses
            .AsNoTracking()
            .FirstOrDefaultAsync(i => i.Guid == guid);
        if (illness == null)
            throw new NotFoundException($"Illness with guid {guid} was not found");
        return illness;
    }

    public async Task<Illness> CreateIllness(Guid medicalHistoryGuid, Illness illness)
    {
        var medicalHistory = await _context.MedicalHistories.FirstOrDefaultAsync(mh => mh.Guid == medicalHistoryGuid);
        if (medicalHistory == null)
            throw new NotFoundException($"Medical history with guid {medicalHistoryGuid} was not found");
        await _context.Illnesses.AddAsync(illness);
        illness.MedicalHistoryId = medicalHistory.Id;
        await _context.SaveChangesAsync();
        return await _context.Illnesses
                   .AsNoTracking()
                   .FirstOrDefaultAsync(i => i.Guid == illness.Guid) ??
               throw new NotFoundException($"Illness with guid {illness.Guid} was not found");
    }

    public async Task DeleteIllness(Guid guid)
    {
        var illness = await _context.Illnesses .FirstOrDefaultAsync(i => i.Guid == guid);
        if (illness == null)
            throw new NotFoundException($"Illness with guid {guid} was not found");
        
        _context.Illnesses.Remove(illness);
        await _context.SaveChangesAsync();
    }

    public async Task<Illness> UpdateIllness(Guid guid, Illness newIllness)
    {
        var illness = await _context.Illnesses.FirstOrDefaultAsync(i => i.Guid == guid);
        if (illness == null)
            throw new NotFoundException($"Illness with guid {guid} was not found");
        illness = _mapper.Map(newIllness, illness);
        _context.Illnesses.Update(illness);
        await _context.SaveChangesAsync();
        return illness;
    }
}