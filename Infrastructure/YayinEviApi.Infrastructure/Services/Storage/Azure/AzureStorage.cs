using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using YayinEviApi.Application.Abstractions.Storage;
using YayinEviApi.Application.Abstractions.Storage.Azure;

namespace YayinEviApi.Infrastructure.Services.Storage.Azure
{
    public class AzureStorage :Storages, IAzureStorage
    {
        readonly BlobServiceClient _blobServiceClient;
        BlobContainerClient _blobContainerClient;

        public AzureStorage(IConfiguration configuration)
        {
            _blobServiceClient = new(configuration["Storage:Azure"]);
        }
        public async Task DeleteAsync(string containerName, string fileName)
        {
            _blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            BlobClient blobClient = _blobContainerClient.GetBlobClient(fileName);
            await blobClient.DeleteAsync();
        }

        public Task<FileObject> DownloadFile(string fullPath, string fileName)
        {
            throw new NotImplementedException();
        }

        public Task<ZipFileObjects> DownloadFileInZip(List<string> filePathList)
        {
            throw new NotImplementedException();
        }

        public List<string> GetFiles(string containerName)
        {
            _blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            return _blobContainerClient.GetBlobs().Select(b=>b.Name).ToList();
        }

        public bool hasFile(string containerName, string fileName)
        {
            _blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            return _blobContainerClient.GetBlobs().Any(b => b.Name==fileName);
        }

        public async Task<List<(string filename, string pathOrContainerName)>> UploadAsync(string containerName, IFormFileCollection fileCollection)
        {
            _blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            await _blobContainerClient.CreateIfNotExistsAsync();
            await _blobContainerClient.SetAccessPolicyAsync(PublicAccessType.BlobContainer);

            List<(string filename, string pathOrContainerName)> datas = new();
            foreach (IFormFile file in fileCollection) {
               //todo AzureStorage sınıfı çalışyor mu , filenameasync metodu çalışıyor mu kontrol edilecek
               string fileNewName=await FileRenameAsync(containerName, file.Name, hasFile);

               BlobClient blobClient= _blobContainerClient.GetBlobClient(fileNewName);
                await blobClient.UploadAsync(file.OpenReadStream());
                datas.Add((fileNewName, $"{containerName}/{fileNewName}"));
            }
            return datas;
        }
    }
}
