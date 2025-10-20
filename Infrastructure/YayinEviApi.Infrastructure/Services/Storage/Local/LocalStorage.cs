using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YayinEviApi.Application.Abstractions.Storage;
using YayinEviApi.Application.Abstractions.Storage.Local;

namespace YayinEviApi.Infrastructure.Services.Storage.Local
{
    public class LocalStorage : Storages, ILocalStorage
    {
        readonly IWebHostEnvironment _webHostEnvironment;

        public LocalStorage(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task DeleteAsync(string pathOrContainerName, string fileName)
            => File.Delete($"{pathOrContainerName}\\{fileName}");

        public async Task<FileObject> DownloadFile(string fullPath,string fileName)
        {
            string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, fullPath);
            
            if (!File.Exists(uploadPath)) {
                //return StatusCodes.Status404NotFound
            }
            // 2. MIME (Content) tipini belirle (Tarayıcının dosya tipini bilmesi için)
            // Varsayılan bir Mime tipi sağlayıcı kullanıyoruz
            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(fullPath, out var contentType))
            {
                // Bilinmeyen tipler için varsayılan değer
                contentType = "application/octet-stream";
            }
            // 3. Dosya akışını (stream) oluştur ve FileStreamResult ile döndür
            var fileStream = new FileStream(uploadPath, FileMode.Open, FileAccess.Read);

            // Tarayıcıya dosyanın indirilmesi gerektiğini söyler
            var _fileName = Path.GetFileName(uploadPath);

            return new FileObject() { FileStream = fileStream,ContenetType=contentType, FileName = _fileName };
           
        }

        public async Task<ZipFileObjects> DownloadFileInZip(List<string> filePathList)
        {
            // 💡 Render'daki Kalıcı Diskinizin kök yolu
            string rootPath = _webHostEnvironment.WebRootPath;

            // Bellek üzerinde bir akış (stream) oluşturuyoruz
            var memoryStream = new MemoryStream();
           
            using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
            {
                foreach (var relativePath in filePathList)
                {
                    // Tam fiziksel yolu oluştur
                    var fullPath = Path.Combine(rootPath, relativePath.TrimStart('/'));

                    if (!System.IO.File.Exists(fullPath))
                    {
                        // Dosya yoksa loglayın ve atlayın, hata vermeyin
                        // Alternatif olarak, 404 döndürebilirsiniz.
                        Console.WriteLine($"Dosya bulunamadı: {fullPath}. Atlaniyor.");
                        continue;
                    }

                    // ZIP arşivi içindeki dosya adını belirle
                    var fileNameInZip = Path.GetFileName(fullPath);

                    // ZIP arşivi içinde yeni bir girdi (entry) oluştur
                    var entry = archive.CreateEntry(fileNameInZip);

                    // Dosyayı diske yazmak yerine, doğrudan bellek akışına kopyala
                    using (var entryStream = entry.Open())
                    using (var fileStream = new FileStream(fullPath, FileMode.Open, FileAccess.Read))
                    {
                        await fileStream.CopyToAsync(entryStream);
                    }
                }

                // Bellek akışının başlangıcına dön
                memoryStream.Seek(0, SeekOrigin.Begin);

                // ZIP dosyasının adı
                var zipFileName = $"indirilenProjeDosyaları-{DateTime.Now:yyyyMMdd}.zip";

                // Yanıtı 'application/zip' MIME tipiyle döndür

                return new ZipFileObjects()
                {
                    MemoryStream = memoryStream,
                    ContenetType = "application/zip",
                    FileName = zipFileName
                };

            }
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
            //string uploadPath = Path.Combine(_webHostEnvironment.ContentRootPath, path);

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
            string uploadPath = Path.Combine(Path.GetTempFileName(), path);

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
