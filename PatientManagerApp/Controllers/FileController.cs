using Microsoft.AspNetCore.Mvc;
using PatientManagerServices.Dtos;
using PatientManagerServices.Services;

namespace PatientManagerApp.Controllers;

[Route("api/[controller]")]
public class FileController : ControllerBase
{
    private readonly IMinioService _minioService;

    public FileController(IMinioService minioService)
    {
        _minioService = minioService;
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> UploadFile([FromForm] FileDto file)
    {
        var filePath = Path.GetTempFileName();

        await using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.File.CopyToAsync(stream);
        }

        await _minioService.UploadFileAsync("test-bucker", file.File.FileName, filePath);

        return Ok($"File {file.File.FileName} uploaded successfully.");
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> DownloadFile(string fileName)
    {
        var filePath = Path.GetTempFileName();
        await _minioService.DownloadFileAsync("test-bucker", fileName, filePath);
        return PhysicalFile(filePath, "application/octet-stream", fileName);
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> ListFiles()
    {
        return Ok(await _minioService.GetListFilesAsync("test-bucker"));
    }
}