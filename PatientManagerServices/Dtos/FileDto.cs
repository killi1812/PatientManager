using Microsoft.AspNetCore.Http;

namespace PatientManagerServices.Dtos;

public class FileDto
{
    public IFormFile File { get; set; }
}