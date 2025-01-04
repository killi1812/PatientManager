using System.Globalization;
using System.Net;
using System.Text;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PatientManagerServices.Models;
using CsvHelper;
using CsvHelper.Configuration;
using PatientManagerServices.Dtos;
using PatientManagerServices.Extras;

namespace PatientManagerServices.Services;

public interface IMedicalHistoryService
{
    Task<string> ExportMedicalHistoryToCsv(Guid patientGuid);
}

public class MedicalHistoryService : IMedicalHistoryService
{
    private readonly PmDbContext _context;
    private readonly IMapper _mapper;
    private const string path = "./tmp";

    public MedicalHistoryService(PmDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<string> ExportMedicalHistoryToCsv(Guid patientGuid)
    {
        var patient = await _context.Patients
            .Include(p => p.MedicalHistory)
            .FirstOrDefaultAsync(p => p.Guid == patientGuid);
        if (patient == null)
            throw new NotFoundException($"Patient with guid: {patientGuid} not found");

        var medicalHistory = await _context.MedicalHistories
            .Include(mh => mh.PastIlnesses)
            .ThenInclude(i => i.Examinations)
            .Include(mh => mh.PastIlnesses)
            .ThenInclude(i => i.Prescriptions)
            .Include(mh => mh.Examinations)
            .FirstOrDefaultAsync(mh => mh.Guid == patient.MedicalHistory.Guid);
        if (medicalHistory == null)
            throw new NotFoundException($"Medical history with guid: {patientGuid} not found");

        var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Delimiter = ",",
            Encoding = Encoding.UTF8
        };
        var name =
            $"{patient.NameNormalized.Replace(' ', '_')}_MedicalHistory_{DateTime.Now.ToShortDateString().Replace('/', '-')}.csv";
        var filePath = Path.Combine(path, name);

        await using var writer = new StreamWriter(filePath);
        await using var csv = new CsvWriter(writer, csvConfig);

        var dto = _mapper.Map<MedicalHistoryDto>(medicalHistory);
        List<object> data = new List<object>();
        foreach (var examination in dto.Examinations.OrderBy(e => e.Illness?.Start))
        {
            if (examination.Illness != null)

                foreach (var prescription in examination.Illness.Prescriptions)
                {
                    data.Add(new
                    {
                        illness = examination.Illness.Name,
                        start = examination.Illness.Start,
                        end = examination.Illness.End,
                        examinationType = examination.Type.ToString(),
                        examinationTime = examination.ExaminationTime,
                        prescriptionName = prescription.Name,
                        prescriptionDate = prescription.Date,
                    });
                }
        }

        foreach (var illness in dto.Illnesss)
        {
            data.Add(new
            {
                illness = illness.Name,
                start = illness.Start,
                end = illness.End,
            });
        }

        await csv.WriteRecordsAsync(data);

        return filePath;
    }
}