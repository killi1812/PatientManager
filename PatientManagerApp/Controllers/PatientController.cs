using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PatientManagerServices.Dtos;
using PatientManagerServices.Models;
using PatientManagerServices.Services;

namespace PatientManagerApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PatientController : ControllerBase
{
    private readonly IPatientService _patientService;
    private readonly IMapper _mapper;

    public PatientController(IPatientService patientService, IMapper mapper)
    {
        _patientService = patientService;
        _mapper = mapper;
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> GetPatients([FromQuery] string q = null, [FromQuery] int page = 1,
        [FromQuery] int n = 10)
    {
        var patients = await _patientService.GetPatients(q, page, n);
        var dtos = _mapper.Map<List<PatientDto>>(patients);
        return Ok(dtos);
    }

    [HttpGet("[action]/{guid}")]
    public async Task<IActionResult> GetPatient([FromRoute] string guid)
    {
        var patient = await _patientService.GetPatient(Guid.Parse(guid));
        var dto = _mapper.Map<PatientDto>(patient);
        return Ok(dto);
    }

    [HttpDelete("[action]/{guid}")]
    public async Task DeletePatient([FromRoute] string guid)
    {
        await _patientService.DeletePatient(Guid.Parse(guid));
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> CreatePatient([FromForm] PatientDto patientDto)
    {
        var patient = await _patientService.CreatePatient(_mapper.Map<Patient>(patientDto));
        var dto = _mapper.Map<PatientDto>(patient);
        return Ok(dto);
    }

    [HttpPut("[action]/{guid}")]
    public async Task<IActionResult> UpdatePatient([FromRoute] string guid, [FromForm] PatientDto patientDto)
    {
        var patient = await _patientService.UpdatePatient(Guid.Parse(guid), _mapper.Map<Patient>(patientDto));
        var dto = _mapper.Map<PatientDto>(patient);
        return Ok(dto);
    }
}