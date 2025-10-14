using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YayinEviApi.Application.Abstractions.Storage;

namespace YayinEviApi.Infrastructure.Services.Storage
{
    public class StorageService : IStorageService
    {
        readonly IStorage _storage;

        public StorageService(IStorage storage)
        {
            _storage = storage;
        }

        public string StorageName { get => _storage.GetType().Name; }

        public async Task DeleteAsync(string pathOrContainerName, string fileName)
            =>await _storage.DeleteAsync(pathOrContainerName, fileName);

        public Task<bool> DownloadFile(string fullPath, string fileName)
            =>_storage.DownloadFile(fullPath, fileName);

        public List<string> GetFiles(string pathOrContainerName)
            =>_storage.GetFiles(pathOrContainerName);

        public bool hasFile(string pathOrContainerName, string fileName)
            =>_storage.hasFile(pathOrContainerName, fileName);

        public Task<List<(string filename, string pathOrContainerName)>> UploadAsync(string pathOrContainerName, IFormFileCollection fileCollection)
            =>_storage.UploadAsync(pathOrContainerName, fileCollection);
        public Task<List<(string filename, string pathOrContainerName)>> UploadCloudAsync(string pathOrContainerName, IFormFileCollection fileCollection)
            => _storage.UploadAsync(pathOrContainerName, fileCollection);
    }
}
