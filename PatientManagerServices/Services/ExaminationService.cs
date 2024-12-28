using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PatientManagerServices.Dtos;
using PatientManagerServices.Extras;
using PatientManagerServices.Models;

namespace PatientManagerServices.Services;

public interface IExaminationService
{
    Task<Examination> Create(Guid medicalHistoryGuid, Guid? illnesGuid, Examination newExamination);
    Task<Examination> Get(Guid guid);
    Task<Examination> Update(Guid guid, ExaminationDto newExamination);
    Task Delete(Guid guid);
    Task<List<Examination>> GetForIllness(Guid illnessGuid);
    Task<List<Examination>> GetAll(Guid medicalHistoryGuid);
}

public class ExaminationService : IExaminationService
{
    private readonly PmDbContext _context;
    private readonly IMapper _mapper;

    public ExaminationService(PmDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Examination> Create(Guid medicalHistoryGuid, Guid? illnesGuid,
        Examination newExamination)
    {
        var medicalHistory = await _context.MedicalHistories.FirstOrDefaultAsync(mh => mh.Guid == medicalHistoryGuid);
        if (medicalHistory == null)
            throw new NotFoundException($"Medical history with guid {medicalHistoryGuid} was not found");

        newExamination.MedicalHistoryId = medicalHistory.Id;
        //newExamination.ExaminationTime = DateTime.UtcNow;

        if (illnesGuid.HasValue)
        {
            var illness = await _context.Illnesses.FirstOrDefaultAsync(i => i.Guid == illnesGuid);
            if (illness == null)
                throw new NotFoundException($"illness history with guid {medicalHistoryGuid} was not found");

            newExamination.IllnessId = illness.Id;
        }

        await _context.Examinations.AddAsync(newExamination);
        await _context.SaveChangesAsync();

        return await _context.Examinations.FirstOrDefaultAsync(e => e.Guid == newExamination.Guid) ??
               throw new NotFoundException($"Examination with guid {newExamination.Guid} was not found");
    }

    public async Task<Examination> Get(Guid guid)
    {
        var examination = await _context.Examinations
            .AsNoTracking()
            .Include(e => e.Illness)
            .FirstOrDefaultAsync(e => e.Guid == guid);
        if (examination == null)
            throw new NotFoundException($"examination with guid: {guid} not found");

        return examination;
    }

    public async Task<Examination> Update(Guid guid, ExaminationDto newExamination)
    {
        var examination = await _context.Examinations.FirstOrDefaultAsync(e => e.Guid == guid);
        if (examination == null)
            throw new NotFoundException($"examination with guid: {guid} not found");

        _mapper.Map(newExamination, examination);
        await _context.SaveChangesAsync();
        
        return await _context.Examinations.FirstOrDefaultAsync(e => e.Guid == guid)
               ?? throw new NotFoundException($"examination with guid: {guid} not found");
    }

    public async Task Delete(Guid guid)
    {
        var examination = await _context.Examinations.FirstOrDefaultAsync(e => e.Guid == guid);
        if (examination == null)
            throw new NotFoundException($"examination with guid: {guid} not found");

        _context.Examinations.Remove(examination);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Examination>> GetForIllness(Guid illnessGuid)
    {
        var examinations = await _context.Examinations
            .AsNoTracking()
            .Where(e => e.Illness.Guid == illnessGuid)
            .Include(e => e.MedicalHistory)
            .ToListAsync();

        return examinations;
    }

    public async Task<List<Examination>> GetAll(Guid medicalHistoryGuid)
    {
        var examinations = await _context.Examinations.Where(e => e.MedicalHistory.Guid == medicalHistoryGuid)
            .AsNoTracking()
            .Include(e => e.MedicalHistory)
            .Include(e => e.Illness)
            .ToListAsync();

        return examinations;
    }
}