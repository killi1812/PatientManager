using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using PatientManagerServices.Extras;
using PatientManagerServices.Models;

namespace PatientManagerServices.Services;

public interface IDoctorService
{
    public Task<Doctor> Create(Doctor doctor, string password);
    public Task<Doctor> Update(Guid guid, Doctor doctor);
    public Task Delete(Guid guid);
    public Task<Doctor> Get(Guid guid);
    public Task<string> Login(string email, string password);
}

public class DoctorService : IDoctorService
{
    private readonly PmDbContext _context;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration; 

    public DoctorService(PmDbContext context, IMapper mapper,IServiceProvider serviceProvider)
    {
        _context = context;
        _mapper = mapper;
        _configuration = serviceProvider.GetRequiredService<IConfiguration>();
    }

    public async Task<Doctor> Create(Doctor doctor, string password)
    {
        doctor.Password = BCrypt.Net.BCrypt.HashPassword(password);
        _context.Doctors.Add(doctor);
        await _context.SaveChangesAsync();
        return doctor;
    }

    public async Task<Doctor> Update(Guid guid, Doctor doctor)
    {
        var existingDoctor = await _context.Doctors.FirstOrDefaultAsync(d => d.Guid == guid);
        if (existingDoctor == null)
            throw new NotFoundException($"Doctor with guid {guid} not found");

        _mapper.Map(doctor, existingDoctor);
        ;

        await _context.SaveChangesAsync();
        return existingDoctor;
    }

    public async Task Delete(Guid guid)
    {
        var doctor = await _context.Doctors.FindAsync(guid);
        if (doctor == null)
        {
            throw new KeyNotFoundException("Doctor not found");
        }

        _context.Doctors.Remove(doctor);
        await _context.SaveChangesAsync();
    }

    public async Task<Doctor> Get(Guid guid)
    {
        var doctor = await _context.Doctors.FindAsync(guid);
        if (doctor == null)
        {
            throw new KeyNotFoundException("Doctor not found");
        }

        return doctor;
    }

    public async Task<string> Login(string email, string password)
    {
        var doctor = await _context.Doctors.FirstOrDefaultAsync(u => u.Email == email);
        if (doctor == null)
            throw new NotFoundException($"Doctor with email {email} not found");

        var result = BCrypt.Net.BCrypt.Verify(password, doctor.Password);
        if (!result)
            throw new UnauthorizedException($"Wrong password for doctor: {email}");

        var tokenHandler = new JwtSecurityTokenHandler();
        byte[] key = Encoding.ASCII.GetBytes(_configuration["key"]);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new("guid", doctor.Guid.ToString())
            }),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}