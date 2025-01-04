using Microsoft.AspNetCore.Mvc;
using PatientManagerServices.Services;

namespace PatientManagerApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicalHistoryController : ControllerBase
    {
        private readonly IMedicalHistoryService _medicalHistoryService;

        public MedicalHistoryController(IMedicalHistoryService medicalHistoryService)
        {
            _medicalHistoryService = medicalHistoryService;
        }

        [HttpGet("export/{patientGuid}")]
        public async Task<IActionResult> ExportMedicalHistory(Guid patientGuid)
        {
            var filePath = await _medicalHistoryService.ExportMedicalHistoryToCsv(patientGuid);
            var fileBytes = await System.IO.File.ReadAllBytesAsync(filePath);
            var fileName = Path.GetFileName(filePath);
            return File(fileBytes, "text/csv", fileName);
        }
    }
}