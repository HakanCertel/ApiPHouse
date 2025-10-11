using Microsoft.AspNetCore.Http;

namespace YayinEviApi.Application.Abstractions.Storage
{
    public interface IStorage
    {
        Task<List<(string filename, string pathOrContainerName)>> UploadAsync(string pathOrContainerName, IFormFileCollection fileCollection);
        Task<bool> DownloadFile(string fullPath, string fileName);
        Task DeleteAsync(string pathOrContainerName, string fileName);

        List<string> GetFiles(string pathOrContainerName);

        bool hasFile(string pathOrContainerName,string fileName);
    }
}
