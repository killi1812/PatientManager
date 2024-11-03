using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PatientManagerServices.Extras;
using PatientManagerServices.Models;

namespace PatientManagerServices.Services;

public interface IExaminationService
{
    Task<Examination> CreateExamination(Guid medicalHistoryGuid, Examination newExamination);
    Task<Examination> CreateExamination(Guid medicalHistoryGuid, Guid illnesGuid, Examination newExamination);
    Task<Examination> GetExamination(Guid guid);
    Task<Examination> UpdateExamination(Guid guid, Examination newExamination);
    Task DeleteExamination(Guid guid);
    Task<List<Examination>> GetExaminations(Guid illnessGuid);
    Task<List<Examination>> GetAllExaminations(Guid medicalHistoryGuid);
    
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

    public async Task<Examination> CreateExamination(Guid medicalHistoryGuid, Examination newExamination)
    {
        var medicalHistory = await _context.MedicalHistories.FirstOrDefaultAsync(mh => mh.Guid == medicalHistoryGuid);
        if (medicalHistory == null)
            throw new NotFoundException($"Medical history with guid {medicalHistoryGuid} was not found");

        newExamination.MedicalHistoryId = medicalHistory.Id;
        await _context.Examinations.AddAsync(newExamination);
        await _context.SaveChangesAsync();
        return await _context.Examinations.FirstOrDefaultAsync(e => e.Guid == newExamination.Guid) ??
               throw new NotFoundException($"Examination with guid {newExamination.Guid} was not found");
    }

    public async Task<Examination> CreateExamination(Guid medicalHistoryGuid, Guid illnesGuid, Examination newExamination)
    {
         var medicalHistory = await _context.MedicalHistories.FirstOrDefaultAsync(mh => mh.Guid == medicalHistoryGuid);
         if (medicalHistory == null)
             throw new NotFoundException($"Medical history with guid {medicalHistoryGuid} was not found");

         var illness = await _context.Illnesses.FirstOrDefaultAsync(i => i.Guid == illnesGuid);
         if (illness == null)
             throw new NotFoundException($"illness history with guid {medicalHistoryGuid} was not found");
         
         newExamination.MedicalHistoryId = medicalHistory.Id;
         newExamination.IllnessId = illness.Id;
         await _context.Examinations.AddAsync(newExamination);
         await _context.SaveChangesAsync();
         return await _context.Examinations.FirstOrDefaultAsync(e => e.Guid == newExamination.Guid) ??
                throw new NotFoundException($"Examination with guid {newExamination.Guid} was not found");       
    }

    public Task<Examination> GetExamination(Guid guid)
    {
        throw new NotImplementedException();
    }

    public Task<Examination> UpdateExamination(Guid guid, Examination newExamination)
    {
        throw new NotImplementedException();
    }

    public Task DeleteExamination(Guid guid)
    {
        throw new NotImplementedException();
    }

    public Task<List<Examination>> GetExaminations(Guid illnessGuid)
    {
        throw new NotImplementedException();
    }

    public Task<List<Examination>> GetAllExaminations(Guid medicalHistoryGuid)
    {
        throw new NotImplementedException();
    }
}