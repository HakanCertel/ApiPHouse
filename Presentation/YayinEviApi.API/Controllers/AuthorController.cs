using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using YayinEviApi.Application.Abstractions.Services;
using YayinEviApi.Application.Abstractions.Storage;
using YayinEviApi.Application.DTOs.AuthorDto;
using YayinEviApi.Application.DTOs.User;
using YayinEviApi.Application.Features.Commands.AuthorFile.ChangeShowcaseImage;
using YayinEviApi.Application.Features.Queries.AuthorFile.GetAuthorFile;
using YayinEviApi.Application.Repositories.IAuthorR;
using YayinEviApi.Application.Repositories.IAuthotFileR;
using YayinEviApi.Application.RequestParameters;
using YayinEviApi.Domain.Entities;
using YayinEviApi.Domain.Enum;
using YayinEviApi.Infrastructure.Operations;

namespace YayinEviApi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")]
    public class AuthorController : ControllerBase
    {
        readonly IMediator _mediator;
        private IUserService _userService;
        readonly CreateUser _user;
        readonly IAuthorReadRepository _authorReadRepository;
        readonly IAuthorWriteRepository _authorWriteRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IStorageService _storageService;
        private readonly IAuthorFileWriteRepository _authorFileWriteRepository;
        private readonly IAuthorFileReadRepository _authorFileReadRepository;
        public AuthorController(IAuthorReadRepository authorReadRepository, IAuthorWriteRepository authorWriteRepository, IWebHostEnvironment webHostEnvironment, IStorageService storageService, IAuthorFileWriteRepository authorFileWriteRepository, IAuthorFileReadRepository authorFileReadRepository, IMediator mediator, IUserService userService)
        {
            _authorReadRepository = authorReadRepository;
            _authorWriteRepository = authorWriteRepository;
            _webHostEnvironment = webHostEnvironment;
            _storageService = storageService;
            _authorFileWriteRepository = authorFileWriteRepository;
            _authorFileReadRepository = authorFileReadRepository;
            _mediator = mediator;
            _userService = userService;
            _user = _userService.GetUser().Result;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] Pagination pagination)
        {
            var totalAuthorCount = _authorReadRepository.GetAll(false).Count();

            var authorss = _authorReadRepository.Table.Select(x => new
            {
                author = x,
                agency=x.Agency,
                creatingUsername = _userService.GetUser(x.CreatingUserId).Result.NameSurname,
                updatingUsername = _userService.GetUser(x.UpdatingUserId).Result.NameSurname,
            }).Select(x => new AuthorS
            {
                Id = x.author.Id.ToString(),
                Name = x.author.Name,
                AgencyId= x.author.AgencyId.ToString(),
                AgencyName=x.agency.Name,
                Address = x.author.Address,
                Code = x.author.Code,
                Email = x.author.Email,
                Gender = x.author.Gender == 0 ? "Kadın" : x.author.Gender.toName(),
                Language = x.author.Language,
                PhoneNumber = x.author.PhoneNumber,
                Surname = x.author.Surname,
                CreatedDate = x.author.CreatedDate,
                UpdatedDate = x.author.UpdatedDate,
                CreatingUserId = x.author.CreatingUserId,
                //CreatingUserNameSurname = x.creatingUsername,
                UpdatingUserId = x.author.UpdatingUserId,
                //UpdatingUserNameSurname = x.updatingUsername,
            });
            //foreach (var item in authorss)
            //{
            //    item.CreatingUserNameSurname = _userService.GetUser(item.CreatingUserId).Result.NameSurname;
            //    item.UpdatingUserNameSurname = _userService.GetUser(item.UpdatingUserId).Result.NameSurname;
            //}
            var authors=authorss.Select(x => x).Skip(pagination.Page * pagination.Size).Take(pagination.Size);
           
            return Ok(new { totalAuthorCount, authors });

        }

        [HttpPost]
        public async Task<IActionResult> Add(AuthorS author)
        {
            Author au = new Author
            {
                Code = author.Code,
                Name = author.Name,
                Surname = author.Surname,
                Address = author.Address,
                Email = author.Email,
                Language = author.Language,
                PhoneNumber = author.PhoneNumber,
                Gender = author.Gender.GetEnum<Gender>(),
                CreatingUserId = _user.UserId,
                AgencyId = author.AgencyId != null && author.AgencyId != "" ? Guid.Parse(author.AgencyId) : null,
            };
            await _authorWriteRepository.AddAsync(au);
           
            await _authorWriteRepository.SaveAsync();
            return Ok(au);
            //return StatusCode((int)HttpStatusCode.Created);

        }
        [HttpPut]
        public async Task<IActionResult> Edit(AuthorS author)
        {
            _authorWriteRepository.Update(new()
            {
                Id=Guid.Parse( author.Id),
                AgencyId = author.AgencyId != null && author.AgencyId != "" ? Guid.Parse(author.AgencyId) : null,
                Code = author.Code,
                Name = author.Name,
                Surname = author.Surname,
                Address = author.Address,
                Email = author.Email,
                Language = author.Language,
                PhoneNumber = author.PhoneNumber,
                Gender = author.Gender.GetEnum<Gender>(),
                UpdatingUserId=_user.UserId,
                CreatingUserId=author.CreatingUserId,
                CreatedDate=author.CreatedDate,
                UpdatedDate=DateTime.Now,
            });
            await _authorWriteRepository.SaveAsync();

            return StatusCode((int)HttpStatusCode.Created);

        }

        [HttpGet("[action]/{id}")]
        [Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> GetAuthorById(string id)
        {
            var author = await _authorReadRepository.Table.Include(w => w.AuthorFiles).Select(x => new AuthorS
            {
                Id = x.Id.ToString(),
                AgencyId=x.AgencyId.ToString(),
                AgencyName=x.Agency.Name,
                Code = x.Code ?? "",
                Name = x.Name ?? "",
                Surname = x.Surname ?? "",
                Address = x.Address ?? "",
                Email = x.Email ?? "",
                Language = x.Language ?? "",
                PhoneNumber = x.PhoneNumber ?? "",
                Gender = x.Gender!=null?x.Gender.toName() : "",
                AuthorFiles=x.AuthorFiles,
            }).FirstOrDefaultAsync(w => w.Id == id);
         
            return Ok(author);
        }

        [HttpDelete("{Id}")]
        [Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> Delete(string id)
        {
            await _authorWriteRepository.RemoveAsync(id);
            await _authorWriteRepository.SaveAsync();
            return Ok();
        }

        [HttpPost("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> Upload(string id)
        {
            //todo FileService sınıfında FileRenameAsync() metodu ile dosya adının aynı olmaması için bir yapı kuruldu video 28 tekrar izleyip yapıyı oluştur
            //var datas = await _storageService.UploadAsync("files", Request.Form.Files); AZURE için
            //var datas = await _fileService.UploadAsync("resorce/product-images", Request.Form.Files);

            var datas = await _storageService.UploadCloudAsync($@"resorce\author-File", Request.Form.Files);

            //var datas = await _storageService.UploadCloudAsync($@"/app/uploads/author", Request.Form.Files);

            Author author = await _authorReadRepository.GetByIdAsync(id);
           
            var isActive = _authorFileReadRepository.Select(x=>x.EntityId==id,x=>x).Any(a => a.IsActive);
           
            await _authorFileWriteRepository.AddRangeAsync(datas.Select(d => new AuthorFile
            {
                FileName = d.filename,
                Path = d.pathOrContainerName,
                IsActive=!isActive,
                EntityId = id,
                Storage = _storageService.StorageName,
                Authors = new List<Author>() { author }
            }).ToList());

            await _authorFileWriteRepository.SaveAsync();
            return Ok();

        }
        [HttpGet("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> GetFiles([FromQuery] GetAuthorFileQueryRequest getAuthorFileQueryRequest)
        {
            List<GetAuthorFileQueryResponse> response = await _mediator.Send(getAuthorFileQueryRequest);
            return Ok(response);
        }
        [HttpGet("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> ChangeShowcaseImage([FromQuery] ChangeShowcaseImageCommandRequest changeShowcaseImageCommandRequest)
        {
            ChangeShowcaseImageCommandResponse response = await _mediator.Send(changeShowcaseImageCommandRequest);
            return Ok(response);
        }
        public async Task<IActionResult> DeleteFile(string id)
        {
            await _authorFileWriteRepository.RemoveAsync(id);
            await _authorFileWriteRepository.SaveAsync();
            return Ok();
        }
    }
}
