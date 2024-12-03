using Microsoft.AspNetCore.Mvc;
using PatientManagerServices.Dtos;
using PatientManagerServices.Models;
using PatientManagerServices.Services;
using AutoMapper;

namespace PatientManagerApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService _doctorService;
        private readonly IMapper _mapper;

        public DoctorController(IDoctorService doctorService, IMapper mapper)
        {
            _doctorService = doctorService;
            _mapper = mapper;
        }
    //TODO test endpoints
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] NewDoctorDto doctorDto)
        {
            var doctor = _mapper.Map<Doctor>(doctorDto);
            var createdDoctor = await _doctorService.Create(doctor, doctorDto.Password);
            var dto = _mapper.Map<DoctorDto>(createdDoctor);
            return CreatedAtAction(nameof(Get), new { guid = createdDoctor.Guid }, dto);
        }

        [HttpPut("{guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid guid, [FromForm] DoctorDetailsDto doctorDto)
        {
            var doctor = _mapper.Map<Doctor>(doctorDto);
            var updatedDoctor = await _doctorService.Update(guid, doctor);
            var dto = _mapper.Map<DoctorDetailsDto>(updatedDoctor);
            return Ok(dto);
        }

        [HttpDelete("{guid}")]
        public async Task<IActionResult> Delete(Guid guid)
        {
            await _doctorService.Delete(guid);
            return NoContent();
        }

        [HttpGet("{guid}")]
        public async Task<IActionResult> Get(Guid guid)
        {
            var doctor = await _doctorService.Get(guid);
            var dto = _mapper.Map<DoctorDetailsDto>(doctor);
            return Ok(dto);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]LoginDto dto)
        {
            var token = await _doctorService.Login(dto.Email, dto.Password);
            return Ok(new { Token = token });
        }
    }
}