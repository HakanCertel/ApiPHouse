using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YayinEviApi.Application.Abstractions.Storage.Local;

namespace YayinEviApi.Infrastructure.Services.Storage.Local
{
    public class LocalStorage :Storages, ILocalStorage
    {
        readonly IWebHostEnvironment _webHostEnvironment;

        public LocalStorage(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task DeleteAsync(string pathOrContainerName, string fileName)
            =>File.Delete($"{pathOrContainerName}\\{fileName}");

        public async Task<bool> DownloadFile(string fullPath,string fileName)
        {
            string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, fullPath);
            if (File.Exists(uploadPath)) {
                return false;
            }
            var fileBytes=File.ReadAllBytes(uploadPath);

            var fileContentResult=new FileContentResult(fileBytes, "application/octet-stream")
            {
                FileDownloadName=fileName,
            };
            return true;//fileContentResult
        }

        public List<string> GetFiles(string pathOrContainerName)
        {
            DirectoryInfo directory = new (pathOrContainerName);
            return directory.GetFiles().Select(f => f.Name).ToList();
        }

        public bool hasFile(string pathOrContainerName, string fileName)
            => File.Exists($"{pathOrContainerName}\\{fileName}");

        public async Task<List<(string filename, string pathOrContainerName)>> UploadAsync(string path, IFormFileCollection fileCollection)
        {
            string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, path);

            if (Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }
            
            List<(string filename, string path)> datas = new();
            
            foreach (IFormFile file in fileCollection)
            {

                //string fileNewName=await FileRenameAsync(file.FileName);
                //todo FileReNameAsync metodu aynı isimde olan fosyalar için 1,2,3 olarak sıralamıyor
                string fileNewName = await FileRenameAsync(path, file.Name, hasFile);


                await CopyFileAsync($"{uploadPath}\\{fileNewName}", file);

                datas.Add((fileNewName, $"{path}\\{fileNewName}"));

            }
          
            return datas;
        }

        public async Task<List<(string filename, string pathOrContainerName)>> UploadCloudAsync(string path, IFormFileCollection fileCollection)
        {
            string uploadPath = Path.Combine(_webHostEnvironment.ContentRootPath, path);

            if (Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            List<(string filename, string path)> datas = new();

            foreach (IFormFile file in fileCollection)
            {

                //string fileNewName=await FileRenameAsync(file.FileName);
                //todo FileReNameAsync metodu aynı isimde olan fosyalar için 1,2,3 olarak sıralamıyor
                string fileNewName = await FileRenameAsync(path, file.Name, hasFile);


                await CopyFileAsync($"{uploadPath}\\{fileNewName}", file);

                datas.Add((fileNewName, $"{path}\\{fileNewName}"));

            }

            return datas;
        }
        async Task<bool> CopyFileAsync(string filePath, IFormFile file)
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

    } 
}
