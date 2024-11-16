using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PatientManagerServices.Dtos;
using PatientManagerServices.Models;
using PatientManagerServices.Services;

namespace PatientManagerApp.Controllers;

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

        [HttpGet("[action]/{guid}")]
        public async Task<ExaminationDto> GetExamination([FromRoute] string guid)
        {
                var examination = await _examinationService.GetExamination(Guid.Parse(guid));
                var dto = _mapper.Map<ExaminationDto>(examination);
                return dto;
        }

        [HttpGet("[action]/{guid}")]
        public async Task<List<ExaminationDto>> GetAllExaminations([FromRoute] string guid)
        {
                var examinations = await _examinationService.GetAllExaminations(Guid.Parse(guid));
                var dto = _mapper.Map<List<ExaminationDto>>(examinations);
                return dto;
        }
    
        [HttpGet("[action]/{guid}")]
        public async Task<List<ExaminationDto>> GetExaminationForIllness([FromRoute] string guid)
        {
                var examinations = await _examinationService.GetExaminationsForIllness(Guid.Parse(guid));
                var dto = _mapper.Map<List<ExaminationDto>>(examinations);
                return dto;
        }
    
        [HttpDelete("[action]/{guid}")]
        public async Task DeleteExamination([FromRoute] string guid)
        {
                await _examinationService.DeleteExamination(Guid.Parse(guid));
        }
    
        [HttpPost("[action]")]
        public async Task<ExaminationDto> CreateExamination([FromForm] NewExaminationDto dto)
        {
                var ex =  await _examinationService.CreateExamination(Guid.Parse(dto.MedicalHistoryGuid),
                        dto.IllnessGuid == null ? null : Guid.Parse(dto.IllnessGuid),
                        _mapper.Map<Examination>(dto));
                var exDto = _mapper.Map<ExaminationDto>(ex);
                return exDto;
        }
    
        [HttpPut("[action]/{guid}")]
        public async Task<ExaminationDto> UpdateExaminations([FromRoute] string guid, [FromForm] NewExaminationDto dto)
        {
                var ex = await _examinationService.UpdateExamination(Guid.Parse(guid), dto);
                var exDto = _mapper.Map<ExaminationDto>(ex);
                return exDto;
        }
}