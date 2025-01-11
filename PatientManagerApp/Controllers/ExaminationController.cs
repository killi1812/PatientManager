using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PatientManagerServices.Dtos;
using PatientManagerServices.Models;
using PatientManagerServices.Services;

namespace PatientManagerApp.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ExaminationController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IExaminationService _examinationService;
    private readonly IMinioService _minioService;
    private const string BucketName = "examinations";

    public ExaminationController(IMapper mapper, IExaminationService examinationService, IMinioService minioService)
    {
        _mapper = mapper;
        _examinationService = examinationService;
        _minioService = minioService;
    }

    [HttpGet("{guid}")]
    public async Task<ExaminationDto> GetExamination([FromRoute] string guid)
    {
        var examination = await _examinationService.Get(Guid.Parse(guid));
        var dto = _mapper.Map<ExaminationDto>(examination);
        return dto;
    }

    [HttpGet("[action]/{medicalHistoryGuid}")]
    public async Task<List<ExaminationDto>> GetAllExaminations([FromRoute] string medicalHistoryGuid)
    {
        var examinations = await _examinationService.GetAll(Guid.Parse(medicalHistoryGuid));
        var dto = _mapper.Map<List<ExaminationDto>>(examinations);
        return dto;
    }

    [HttpGet("[action]/{illnessGuid}")]
    public async Task<List<ExaminationDto>> GetExaminationForIllness([FromRoute] string illnessGuid)
    {
        var examinations = await _examinationService.GetForIllness(Guid.Parse(illnessGuid));
        var dto = _mapper.Map<List<ExaminationDto>>(examinations);
        return dto;
    }

    [HttpDelete("{guid}")]
    public async Task<IActionResult> DeleteExamination([FromRoute] string guid)
    {
        await _examinationService.Delete(Guid.Parse(guid));
        return NoContent();
    }

    [HttpPost("[action]")]
    public async Task<ExaminationDto> CreateExamination([FromForm] NewExaminationDto dto)
    {
        var ex = await _examinationService.Create(Guid.Parse(dto.MedicalHistoryGuid),
            dto.IllnessGuid == null ? null : Guid.Parse(dto.IllnessGuid),
            _mapper.Map<Examination>(dto));
        var exDto = _mapper.Map<ExaminationDto>(ex);
        return exDto;
    }

    [HttpPut("{guid}")]
    public async Task<ExaminationDto> UpdateExaminations([FromRoute] string guid, [FromForm] ExaminationDto dto)
    {
        var ex = await _examinationService.Update(Guid.Parse(guid), dto);
        var exDto = _mapper.Map<ExaminationDto>(ex);
        return exDto;
    }


    [HttpPost("[action]/{guid}")]
    public async Task<IActionResult> Upload(string guid, [FromForm] FileDto file)
    {
        var filePath = Path.GetTempFileName();
        Console.WriteLine(guid);

        await using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.File.CopyToAsync(stream);
        }

        var newFileName = $"{Guid.NewGuid()}.{file.File.FileName.Split(".")[1]}";
        await _minioService.UploadFileAsync(BucketName, newFileName, filePath);

        return Ok($"File {file.File.FileName} uploaded successfully.");
    }

    [HttpGet("[action]/{guid}")]
    public async Task<IActionResult> DownloadFile(string guid)
    {
        string fileName = "";
        var filePath = Path.GetTempFileName();
        await _minioService.DownloadFileAsync(BucketName, fileName, filePath);
        return PhysicalFile(filePath, "application/octet-stream", fileName);
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> ListFiles()
    {
        return Ok(await _minioService.GetListFilesAsync(BucketName));
    }
}