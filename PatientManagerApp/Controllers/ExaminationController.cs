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

    public ExaminationController(IMapper mapper, IExaminationService examinationService)
    {
        _mapper = mapper;
        _examinationService = examinationService;
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
    public async Task<ExaminationDto> UpdateExaminations([FromRoute] string guid, [FromForm] NewExaminationDto dto)
    {
        var ex = await _examinationService.Update(Guid.Parse(guid), dto);
        var exDto = _mapper.Map<ExaminationDto>(ex);
        return exDto;
    }
}