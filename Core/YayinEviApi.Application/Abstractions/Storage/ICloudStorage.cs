using Microsoft.AspNetCore.Http;

namespace YayinEviApi.Application.Abstractions.Storage
{
    public  interface ICloudStorage
    {
        Task<List<(string filename, string pathOrContainerName)>> UploadCloudAsync(string pathOrContainerName, IFormFileCollection fileCollection);
    }
}
