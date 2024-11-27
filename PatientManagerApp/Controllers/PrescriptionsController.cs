using Microsoft.AspNetCore.Mvc;
using PatientManagerServices.Dtos;
using PatientManagerServices.Services;
using PatientManagerServices.Models;
using AutoMapper;

namespace PatientManagerApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PrescriptionsController : ControllerBase
{
    private readonly IPrescriptionService _prescriptionService;
    private readonly IMapper _mapper;

    public PrescriptionsController(IPrescriptionService prescriptionService, IMapper mapper)
    {
        _prescriptionService = prescriptionService;
        _mapper = mapper;
    }

    [HttpGet("{guid}")]
    public async Task<IActionResult> Get(Guid guid)
    {
        var prescription = await _prescriptionService.Get(guid);
        var prescriptionDto = _mapper.Map<PrescriptionDto>(prescription);
        return Ok(prescriptionDto);
    }

    [HttpGet("medicalHistory/{medicalHistoryGuid}")]
    public async Task<IActionResult> GetAll(Guid medicalHistoryGuid)
    {
        var prescriptions = await _prescriptionService.GetAll(medicalHistoryGuid);
        var prescriptionsDto = _mapper.Map<List<PrescriptionDto>>(prescriptions);
        return Ok(prescriptionsDto);
    }

    [HttpGet("illness/{illnessGuid}")]
    public async Task<IActionResult> GetForIllness(Guid illnessGuid)
    {
        var prescriptions = await _prescriptionService.GetForIllness(illnessGuid);
        var prescriptionsDto = _mapper.Map<List<PrescriptionDto>>(prescriptions);
        return Ok(prescriptionsDto);
    }

    [HttpDelete("{guid}")]
    public async Task<IActionResult> Delete(Guid guid)
    {
        await _prescriptionService.Delete(guid);
        return NoContent();
    }

    [HttpPut("{guid}")]
    public async Task<IActionResult> Update(Guid guid, [FromForm] PrescriptionDto prescriptionDto)
    {
        var prescription = _mapper.Map<Prescription>(prescriptionDto);
        var updatedPrescription = await _prescriptionService.Update(guid, prescription);
        var updatedPrescriptionDto = _mapper.Map<PrescriptionDto>(updatedPrescription);
        return Ok(updatedPrescriptionDto);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] PrescriptionDto prescriptionDto)
    {
        var createdPrescription = await _prescriptionService.Create(Guid.Parse(prescriptionDto.MedicalHistoryGuid),
            prescriptionDto.IllnessGuid == null ?null: Guid.Parse(prescriptionDto.IllnessGuid),
            _mapper.Map<Prescription>(prescriptionDto));
        var createdPrescriptionDto = _mapper.Map<PrescriptionDto>(createdPrescription);
        return CreatedAtAction(nameof(Get), new { guid = createdPrescriptionDto.Guid }, createdPrescriptionDto);
    }
}