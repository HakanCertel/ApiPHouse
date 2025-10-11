using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using YayinEviApi.Application.Services;

namespace YayinEviApi.Infrastructure.Services
{
    public class FileService : IFileService
    {
        readonly IWebHostEnvironment _webHostEnvironment;
        public FileService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<bool> CopyFileAsync(string filePath, IFormFile file)
        {
            try
            {
                await using FileStream fileStream = new(filePath, FileMode.Create, FileAccess.Write, FileShare.None, 1024 * 1024, useAsync: false);
                await file.CopyToAsync(fileStream);
                await fileStream.FlushAsync();
                return true;
            }
            catch (Exception ex)
            {
                //todo log
                throw ex;
            }
        }

        //public async Task<string> FileRenameAsync(string fileName)
        //{
            
        //}

        public async Task<List<(string filename, string path)>> UploadAsync(string path, IFormFileCollection fileCollection)
        {
            string uploadPath=Path.Combine(_webHostEnvironment.WebRootPath, path);

            if (Directory.Exists(uploadPath)) {
                Directory.CreateDirectory(uploadPath);
            }
            List<(string filename, string path)> datas = new();
            List<bool> results=new();

            foreach (IFormFile file in fileCollection) { 
            
                //string fileNewName=await FileRenameAsync(file.FileName);

                bool result=await CopyFileAsync($"{ uploadPath}\\{file.FileName}",file);

                datas.Add((file.FileName, $"{path}\\{file.FileName}"));
                results.Add(result);
            
            }
            if(results.TrueForAll(x => x.Equals(true)))
                return datas;
            
            return null;


        }
    }
}
