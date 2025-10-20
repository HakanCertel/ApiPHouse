using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using YayinEviApi.Application.Abstractions.Storage;
using YayinEviApi.Application.DTOs.WorkDtos;
using YayinEviApi.Application.Features.Commands.WorkFiles.ChangeShowcaseImage;
using YayinEviApi.Application.Features.Commands.WorkFiles.RemoveWorkFile;
using YayinEviApi.Application.Features.Queries.GetWorkFiles.GetWorkImages;
using YayinEviApi.Application.Repositories;
using YayinEviApi.Application.Repositories.IWorkR;
using YayinEviApi.Application.RequestParameters;
using YayinEviApi.Domain.Entities;
using YayinEviApi.Domain.Entities.Identity;
using YayinEviApi.Domain.Entities.WorkE;
namespace YayinEviApi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")]
    public class WorkController : ControllerBase
    {
        readonly IWorkReadRepository _workReadRepository;
        readonly IWorkWriteRepository _workWriteRepository;
        readonly IProccessForWorkWriteRepository _proccessForWorkWriteRepository;
        readonly IProccessForWorkReadRepository _proccessForWorkReadRepository;
        readonly UserManager<AppUser> _userManager;
        readonly IPublishFileReadRepository _publishFileReadRepository;
        readonly IPublishFileWriteRepository _publishFileWriteRepository;
        readonly IMediator _mediator;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IStorageService _storageService;


        public WorkController(IWorkWriteRepository workWriteRepository, IWorkReadRepository workReadRepository, IProccessForWorkWriteRepository proccessForWorkWriteRepository, IProccessForWorkReadRepository proccessForWorkReadRepository, UserManager<AppUser> userManager, IWebHostEnvironment webHostEnvironment, IStorageService storageService, IPublishFileReadRepository publishFileReadRepository, IPublishFileWriteRepository publishFileWriteRepository, IMediator mediator)
        {
            _workWriteRepository = workWriteRepository;
            _workReadRepository = workReadRepository;
            _proccessForWorkWriteRepository = proccessForWorkWriteRepository;
            _proccessForWorkReadRepository = proccessForWorkReadRepository;
            _userManager = userManager;
            _webHostEnvironment = webHostEnvironment;
            _storageService = storageService;
            _publishFileReadRepository = publishFileReadRepository;
            _publishFileWriteRepository = publishFileWriteRepository;
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] Pagination pagination)
        {
            var totalWorksCount = _workReadRepository.GetAll(false).Count();

            var works = _workReadRepository.Table.Include(x=>x.PublishFiles).Select(x => new WorkL
            {
                Id = x.Id.ToString(),
                Name = x.Name,
                Code = x.Code,
                AuthorId = x.AuthorId.ToString(),
                AuthorNameAndSurnmae = x.Author.Name+" "+x.Author.Surname,
                AgencyId = x.AgencyId.ToString(),
                AgencyName=x.Agency.Name,
                Bandrol=x.Bandrol,
                Barcode=x.Barcode,
                CategoryId=x.CategoryId.ToString(),
                CategoryName=x.Category.Name,
                CertificateNumber=x.CertificateNumber,
                Description=x.Description,
                ISBN=x.isbn,
                WorkOrginalName=x.WorkOrginalName,
                CreatedDate = x.CreatedDate,
                Language = x.Language,
                UpdatedDate = x.UpdatedDate,
                PublishFiles=x.PublishFiles,
            }).Select(x => x)?.Skip(pagination.Page * pagination.Size).Take(pagination.Size);
           
            return Ok(new { totalWorksCount, works });

        }
        
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var newWork =await  _workReadRepository.Table.Include(w => w.PublishFiles).Select(x=> new WorkL
            {
                Id = x.Id.ToString(),
                Name = x.Name,
                Code = x.Code,
                AuthorId = x.AuthorId.ToString(),
                AuthorNameAndSurnmae = x.Author.Name+x.Author.Surname,
                AgencyId = x.AgencyId.ToString(),
                AgencyName=x.Agency.Name,
                Bandrol = x.Bandrol,
                Barcode = x.Barcode,
                CategoryId = x.CategoryId.ToString(),
                CategoryName=x.Category.Name,
                CertificateNumber = x.CertificateNumber,
                Description = x.Description,
                ISBN = x.isbn,
                WorkOrginalName = x.WorkOrginalName,
                Language = x.Language,
                Subject = x.Subject,
                PublishFiles=x.PublishFiles,
            }).FirstOrDefaultAsync(w=>w.Id==id);

            return Ok(newWork);
        }
        
        [HttpPost]
        public async Task<IActionResult> Add(WorkS works)
        {
            Work work = new Work
            {
                Name = works.Name,
                Code = works.Code,
                AuthorId = works.AuthorId != null && works.AuthorId != "" ? Guid.Parse(works.AuthorId) : null,
                AgencyId = works.AgencyId != null && works.AgencyId != "" ? Guid.Parse(works.AgencyId) : null,
                Bandrol = works.Bandrol,
                Barcode = works.Barcode,
                CategoryId = works.CategoryId != null && works.CategoryId != "" ? Guid.Parse(works.CategoryId) : null,
                CertificateNumber = works.CertificateNumber,
                Description = works.Description,
                isbn = works.ISBN,
                WorkOrginalName = works.WorkOrginalName,
                Language = works.Language,
            };
            await _workWriteRepository.AddAsync(work);
            await _workWriteRepository.SaveAsync();
            
            //var item =await _workReadRepository.GetAll().OrderBy(x=>x.CreatedDate).LastAsync();
            return Ok(work);
            //return StatusCode((int)HttpStatusCode.Created);

        }
        
        [HttpPut]
        public async Task<IActionResult> Edit(WorkL work)
        {
            _workWriteRepository.Update(new()
            {
                Id =Guid.Parse( work.Id),
                Name = work.Name,
                Code = work.Code,
                AuthorId = work.AuthorId != null && work.AuthorId!="" ? Guid.Parse(work.AuthorId) : null,
                AgencyId= work.AgencyId != null && work.AgencyId != "" ? Guid.Parse(work.AgencyId) : null,
                Bandrol = work.Bandrol,
                Barcode = work.Barcode,
                CategoryId = work.CategoryId != null && work.CategoryId!="" ? Guid.Parse(work.CategoryId) : null,
                CertificateNumber = work.CertificateNumber,
                Description = work.Description,
                Subject=work.Subject,
                isbn = work.ISBN,
                WorkOrginalName = work.WorkOrginalName,
                Language = work.Language,
            });
            await _workWriteRepository.SaveAsync();

            return StatusCode((int)HttpStatusCode.Created);

        }
       
        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _workWriteRepository.RemoveAsync(id);
            await _workWriteRepository.SaveAsync();
            return Ok();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddProccess(ProccessForWorkS procces)
        {
            await _proccessForWorkWriteRepository.AddAsync(new()
            {
               WorkId=Guid.Parse( procces.WorkId),
               ProccessId=Guid.Parse( procces.ProccessId),
               UserId=procces.UserId
            });
            await _proccessForWorkWriteRepository.SaveAsync();

            return StatusCode((int)HttpStatusCode.Created);

        }
        
        [HttpPut("[action]")]
        public async Task<IActionResult> EditProccessForWork(ProccessForWorkS proccess)
        {
            _proccessForWorkWriteRepository.Update(new()
            {
                Id = Guid.Parse(proccess.Id),
                ProccessId=Guid.Parse(proccess.ProccessId),
                UserId=proccess.UserId,
                WorkId=Guid.Parse(proccess.WorkId),
               
            });
            await _proccessForWorkWriteRepository.SaveAsync();

            return StatusCode((int)HttpStatusCode.Created);

        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetProccessForWork()
        {
            var work = _proccessForWorkReadRepository.Select(null,x=>new ProccessForWorkS
            {
                Id=x.Id.ToString(),
                WorkId=x.WorkId.ToString(),
                ProccessId=x.ProccessId.ToString(),
                ProccessName=x.Proccess.Name,
                UserId=x.UserId.ToString(),
                UserName=x.UserId,
            }).ToList();
           
            return Ok(work);
        }
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetProccessById(string? id)
        {
            var proccesL = _proccessForWorkReadRepository.Select(x => x.WorkId.ToString() == id,x=> new ProccessForWorkS
            {
                Id=x.Id.ToString(),
                ProccessId = x.ProccessId.ToString(),
                WorkId=x.WorkId.ToString(),
                UserId=x.UserId.ToString(),
                UserName=x.UserId,
                ProccessName=x.Proccess.Name,
            });
            return Ok(proccesL);
        }
        [HttpDelete("[action]/{Id}")]
        public async Task<IActionResult> DeleteProccesForWork(string id)
        {
            await _proccessForWorkWriteRepository.RemoveAsync(id);
            await _proccessForWorkWriteRepository.SaveAsync();
            return Ok();
        }

        [HttpPost("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> Upload(string id,string? departmentId,string? userId)
        {
            //todo FileService sınıfında FileRenameAsync() metodu ile dosya adının aynı olmaması için bir yapı kuruldu video 28 tekrar izleyip yapıyı oluştur
            //var datas = await _storageService.UploadAsync("files", Request.Form.Files); AZURE için
            //var datas = await _fileService.UploadAsync("resorce/product-images", Request.Form.Files);

            var datas = await _storageService.UploadCloudAsync($@"resorce\work-images", Request.Form.Files);
            
            var isActive = _publishFileReadRepository.Select(x => x.EntityId == id, x => x).Any(a => a.IsActive);
            Work work = await _workReadRepository.GetByIdAsync(id);

            await _publishFileWriteRepository.AddRangeAsync(datas.Select(d => new PublishFile
            {
                FileName = d.filename,
                Path = d.pathOrContainerName,
                Storage = _storageService.StorageName,
                IsActive=!isActive,
                EntityId= id,
                WorkId =id,
                UserId=userId,
                DepartmentId= departmentId,
                Works = new List<Work>() { work }
            }).ToList());

            await _publishFileWriteRepository.SaveAsync();
            return Ok();

        }
        [HttpGet("[action]")]
        public async Task<IActionResult> DownloadFile(string fullPath,string fileName)
        {
            await _storageService.DownloadFile(fullPath, fileName);
            return Ok();
        }
       
        [HttpDelete("[action]/{id}")]
        //[Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> DeleteWorkFile([FromRoute] RemoveWorkFileCommandRequest removeWorkFileCommandRequest, [FromQuery] string imageId)
        {
            //Ders sonrası not !
            //Burada RemoveProductImageCommandRequest sınıfı içerisindeki ImageId property'sini de 'FromQuery' attribute'u ile işaretleyebilirdik!

            removeWorkFileCommandRequest.ImageId = imageId;
            RemoveWorkFileCommandResponse response = await _mediator.Send(removeWorkFileCommandRequest);
            return Ok();
        }

        [HttpGet("[action]")]
        //[Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> GetPublishFile([FromQuery] GetPublishFileQueryRequest getPublishFileQueryRequest)
        {
            List<GetPublishFileQueryResponse> response = await _mediator.Send(getPublishFileQueryRequest);
            return Ok(response);
        }

        [HttpGet("[action]")]
        //[Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> ChangeShowcaseImage([FromQuery] ChangeShowcaseImageCommandRequest changeShowcaseImageCommandRequest)
        {
            ChangeShowcaseImageCommandResponse response = await _mediator.Send(changeShowcaseImageCommandRequest);
            return Ok(response);
        }


    }

}
