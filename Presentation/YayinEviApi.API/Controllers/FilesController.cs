using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YayinEviApi.Application.Abstractions.Storage;

namespace ETicaretAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        readonly IConfiguration _configuration;
        private readonly IStorageService _storageService;

        public FilesController(IConfiguration configuration, IStorageService storageService)
        {
            _configuration = configuration;
            _storageService = storageService;
        }

        [HttpGet("[action]")]
        public IActionResult GetBaseStorageUrl()
        {
            return Ok(new
            {
                Url = _configuration["BaseStorageUrl"]
            });
        }
        [HttpGet("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FileStreamResult))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DownloadFile(string fullPath, string fileName)
        {
            var obj = _storageService.DownloadFile(fullPath, fileName).Result;

            return File(
                obj.FileStream,
                obj.ContenetType,
                // İndirilecek dosyanın adını belirtir
                obj.FileName
            );
        }

        [HttpGet("download/zip")]
        public async Task<IActionResult> DownloadFilesAsZip([FromQuery] List<string> filePaths)
        {
            if (filePaths == null || !filePaths.Any())
            {
                return BadRequest("İndirilecek dosya yolu belirtilmedi.");
            }
            var obj = _storageService.DownloadFileInZip(filePaths).Result;

            return File(obj.MemoryStream,obj.ContenetType,obj.FileName);
        }

    }
}
