using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PatientManagerServices.Dtos;
using PatientManagerServices.Models;
using PatientManagerServices.Services;

namespace PatientManagerApp.Controllers;

[Route("api/[controller]")]
public class IllnessController : ControllerBase
{
    private readonly IIllnessService _illnessService;
    private readonly IMapper _mapper;

    public IllnessController(IIllnessService illnessService, IMapper mapper)
    {
        _illnessService = illnessService;
        _mapper = mapper;
    }

    [HttpGet("[action]/{guid}")]
    public async Task<IActionResult> GetIlness([FromRoute] string guid)
    {
        var illness = await _illnessService.GetIllness(Guid.Parse(guid));
        var dto = _mapper.Map<IllnessDto>(illness);
        return Ok(dto);
    }

    [HttpGet("[action]/{medicalHistoryGuid}")]
    public async Task<IActionResult> GetIlnessesForPatients([FromRoute] string medicalHistoryGuid)
    {
        var illnesses = await _illnessService.GetPatientIllnesses(Guid.Parse(medicalHistoryGuid));
        var dtos = _mapper.Map<List<IllnessDto>>(illnesses);
        return Ok(dtos);
    }

    [HttpDelete("[action]/{guid}")]
    public async Task DeleteIllness([FromRoute] string guid)
    {
        await _illnessService.DeleteIllness(Guid.Parse(guid));
    }

    [HttpPost("[action]/{medicalHistoryGuid}")]
    public async Task<IActionResult> CreateIllness([FromRoute] string medicalHistoryGuid,
        [FromForm] IllnessDto illnessDto)
    {
        var illness =
            await _illnessService.CreateIllness(Guid.Parse(medicalHistoryGuid), _mapper.Map<Illness>(illnessDto));
        var dto = _mapper.Map<IllnessDto>(illness);
        return Ok(dto);
    }

    [HttpPut("[action]/{guid}")]
    public async Task<IActionResult> UpdateIllness([FromRoute] string guid, [FromForm] IllnessDto illnessDto)
    {
        var illness = await _illnessService.UpdateIllness(Guid.Parse(guid), _mapper.Map<Illness>(illnessDto));
        var dto = _mapper.Map<IllnessDto>(illness);
        return Ok(dto);
    }

    [HttpPut("[action]/{guid}")]
    public async Task<IActionResult> EndIllness([FromRoute] string guid)
    {
        
        throw new NotImplementedException();
        var illness = await _illnessService.GetIllness(Guid.Parse(guid));
        var dto = _mapper.Map<IllnessDto>(illness);
        return Ok(dto);
    }
}