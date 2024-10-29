using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PatientManagerServices.Dtos;
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
}