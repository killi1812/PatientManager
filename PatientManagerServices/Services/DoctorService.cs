using AutoMapper;
using Microsoft.EntityFrameworkCore;
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

    public DoctorService(PmDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
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
        var doctor = await _context.Doctors.SingleOrDefaultAsync(d => d.Email == email);
        if (doctor == null || !BCrypt.Net.BCrypt.Verify(doctor.Password, password))
            throw new UnauthorizedException("Invalid email or password");

        //TODO Generate and return a token or session identifier
        return "token"; // Replace with actual token generation logic
    }
}