using System.Reactive.Linq;
using Microsoft.AspNetCore.Http;
using Minio;
using Minio.ApiEndpoints;
using Minio.DataModel;
using Minio.DataModel.Args;
using Minio.Exceptions;

namespace PatientManagerServices.Services;

public interface IMinioService
{
    public Task<string> UploadFileAsync(string bucketName, IFormFile objectName, string filePath);

    public Task<ObjectStat> DownloadFileAsync(string testBucket, string fileName, string filePath);

    Task<IEnumerable<string>> GetListFilesAsync(string testBucket);
}

public class MinioService : IMinioService
{
    private readonly IMinioClient _minioClient;

    public MinioService(IMinioClient minioClient)
    {
        _minioClient = minioClient;
    }
    public async Task<string> UploadFileAsync(string bucketName, IFormFile file, string filePath)
    {
        var newFileName = $"{Guid.NewGuid()}.{file.FileName.Split(".")[1]}";
            // Make a bucket on the server, if not already present.
            var beArgs = new BucketExistsArgs()
                .WithBucket(bucketName);
            bool found = await _minioClient.BucketExistsAsync(beArgs).ConfigureAwait(false);
            if (!found)
            {
                var mbArgs = new MakeBucketArgs()
                    .WithBucket(bucketName);
                await _minioClient.MakeBucketAsync(mbArgs).ConfigureAwait(false);
            }
            // Upload a file to bucket.
            var putObjectArgs = new PutObjectArgs()
                .WithBucket(bucketName)
                .WithObject(newFileName)
                .WithFileName(filePath)
                .WithContentType(file.ContentType);
            await _minioClient.PutObjectAsync(putObjectArgs).ConfigureAwait(false);
            return newFileName;
    }

    public async Task<ObjectStat> DownloadFileAsync(string testBucket, string objectName, string filePath)
    {
            var statObjectArgs = new StatObjectArgs()
                .WithBucket(testBucket)
                .WithObject(objectName);
            var rez = await _minioClient.StatObjectAsync(statObjectArgs);

            var getObjectArgs = new GetObjectArgs()
                .WithBucket(testBucket)
                .WithObject(objectName)
                .WithFile(filePath);
            await _minioClient.GetObjectAsync(getObjectArgs);
            return rez;
    }

    public Task<IEnumerable<string>> GetListFilesAsync(string testBucket)
    {
        try
        {
            ListObjectsArgs args = new ListObjectsArgs()
                .WithBucket(testBucket);
            return Task.FromResult(_minioClient.ListObjectsAsync(args).Select(i => i.Key).ToEnumerable());
        }
        catch (MinioException e)
        {
            Console.WriteLine($"Error Listing Objects: {e.Message}");
        }
        return Task.FromResult<IEnumerable<string>>(new List<string>());
    }
}
