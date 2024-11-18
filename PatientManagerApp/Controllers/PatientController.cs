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
    public async Task<List<PatientDto>> GetPatients()
    {
        var patients = await _patientService.GetPatients();
        var pMapped = _mapper.Map<List<PatientDto>>(patients);
        return pMapped;
    }

    [HttpGet("[action]/{guid}")]
    public async Task<IActionResult> GetPatient([FromRoute] string guid)
    {
        var patient = await _patientService.GetPatient(Guid.Parse(guid));
        var pMapped = _mapper.Map<PatientDto>(patient);
        return Ok(pMapped);
    }

    [HttpDelete("[action]/{guid}")]
    public async Task DeletePatient([FromRoute] string guid)
    {
        await _patientService.DeletePatient(Guid.Parse(guid));
    }

    [HttpPost("[action]")]
    public async Task<PatientDto> CreatePatient([FromForm] PatientDto patientDto)
    {
        var patient = await _patientService.CreatePatient(_mapper.Map<Patient>(patientDto));
        var pMapped = _mapper.Map<PatientDto>(patient);
        return pMapped;
    }

    [HttpPut("[action]/{guid}")]
    public async Task<PatientDto> UpdatePatient([FromRoute] string guid, [FromForm] PatientDto patientDto)
    {
        var patient = await _patientService.UpdatePatient(Guid.Parse(guid), _mapper.Map<Patient>(patientDto));
        var pMapped = _mapper.Map<PatientDto>(patient);
        return pMapped;
    }
}