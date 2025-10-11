using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YayinEviApi.Application.Services
{
    public interface IFileService
    {
        Task<List<(string filename,string path)>> UploadAsync(string filePath,IFormFileCollection fileCollection);
        Task<bool> CopyFileAsync(string path,IFormFile file);
    }
}
