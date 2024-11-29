using PatientManagerServices.Migrations;

namespace PatientManagerServices.Services;

public interface IDoctorService
{
    public Task<Doctor> Create(Doctor doctor);
    public Task<Doctor> Update(Guid guid, Doctor doctor);
    public Task Delete(Guid guid);
    public Task<Doctor> Get(Guid guid);
    public Task<string> Login(string email, string password);
}

public class DoctorService: IDoctorService
{
    public Task<Doctor> Create(Doctor doctor)
    {
        throw new NotImplementedException();
    }

    public Task<Doctor> Update(Guid guid, Doctor doctor)
    {
        throw new NotImplementedException();
    }

    public Task Delete(Guid guid)
    {
        throw new NotImplementedException();
    }

    public Task<Doctor> Get(Guid guid)
    {
        throw new NotImplementedException();
    }

    public Task<string> Login(string email, string password)
    {
        throw new NotImplementedException();
    }
}