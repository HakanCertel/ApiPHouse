using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace YayinEviApi.Application.Abstractions.Storage
{
    public interface IStorage
    {
        Task<List<(string filename, string pathOrContainerName)>> UploadAsync(string pathOrContainerName, IFormFileCollection fileCollection);
        Task<FileObject> DownloadFile(string fullPath, string fileName);
        Task<ZipFileObjects> DownloadFileInZip(List<string> filePathList);
        Task DeleteAsync(string pathOrContainerName, string fileName);

        List<string> GetFiles(string pathOrContainerName);

        bool hasFile(string pathOrContainerName,string fileName);
    }

    public class FileObject
    {
        public FileStream FileStream { get; set; }
        public string ContenetType { get; set; }
        public string FileName { get; set; }
    }
    public class ZipFileObjects
    {
        public MemoryStream MemoryStream { get; set; }
        public string ContenetType { get; set; }
        public string FileName { get; set; }
    }
}
