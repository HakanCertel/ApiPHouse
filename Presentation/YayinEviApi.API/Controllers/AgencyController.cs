using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using YayinEviApi.Application.Abstractions.Storage;
using YayinEviApi.Application.DTOs.AgencyDto;
using YayinEviApi.Application.Features.Commands.AgencyFile.ChangeShowcaseImage;
using YayinEviApi.Application.Features.Queries.AgencyFile.GetAgencyFile;
using YayinEviApi.Application.Features.Queries.AuthorFile.GetAuthorFile;
using YayinEviApi.Application.Repositories.AgencyFileR;
using YayinEviApi.Application.Repositories.AgencyR;
using YayinEviApi.Application.Repositories.IAuthorR;
using YayinEviApi.Application.Repositories.IAuthotFileR;
using YayinEviApi.Application.Repositories.IWorkR;
using YayinEviApi.Application.RequestParameters;
using YayinEviApi.Domain.Entities;
using YayinEviApi.Domain.Entities.AgencyE;
using YayinEviApi.Infrastructure.Enums;
using YayinEviApi.Infrastructure.Operations;
using YayinEviApi.Persistence.Repositories.AuthotFileR;

namespace YayinEviApi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")]
    public class AgencyController : ControllerBase
    {
        readonly IMediator _mediator;
        private readonly IStorageService _storageService;
        readonly IAgencyWriteRepository _agencyWriteRepository;
        readonly IAgencyReadRepository _agencyReadRepository;
        private readonly IAgencyFileWriteRepository _agencyFileWriteRepository;
        private readonly IAgencyFileReadRepository _agencyFileReadRepository;
        readonly IWorkWriteRepository _workWriteRepository;
        readonly IWorkReadRepository _workReadRepository;
        readonly IAuthorReadRepository _authorReadRepository;
        readonly IAuthorWriteRepository _authorWriteRepository;

        public AgencyController(IAgencyWriteRepository agencyWriteRepository, IAgencyReadRepository agencyReadRepository, IMediator mediator, IAgencyFileWriteRepository agencyFileWriteRepository, IAgencyFileReadRepository agencyFileReadRepository, IStorageService storageService, IWorkWriteRepository workWriteRepository, IWorkReadRepository workReadRepository)
        {
            _agencyWriteRepository = agencyWriteRepository;
            _agencyReadRepository = agencyReadRepository;
            _mediator = mediator;
            _agencyFileWriteRepository = agencyFileWriteRepository;
            _agencyFileReadRepository = agencyFileReadRepository;
            _storageService = storageService;
            _workWriteRepository = workWriteRepository;
            _workReadRepository = workReadRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] Pagination pagination) 
        {
            var totalAgencyCount = _agencyReadRepository.GetAll(false).Count();

            var agency = _agencyReadRepository.Table.Include(x => x.AgencyFiles).Select( x => new AgencyS
            {
                Id = x.Id.ToString(),
                Name = x.Name,
                Address = x.Address,
                Code = x.Code,
                City = x.City,
                Country = x.Country,
                Description = x.Description,
                LocalOrForeing= x.LocalOrForeing!=null? x.LocalOrForeing.toName():"",
                State = x.State != null ? x.State.toName() : "",
                TaxDeparmant=x.TaxDeparmant,
                TaxNumber=x.TaxNumber,
                Town=x.Town,
                CreatedDate = x.CreatedDate,
                UpdatedDate = x.UpdatedDate,
                AgencyFiles = x.AgencyFiles.ToList(),
            }).Select(x => x).Skip(pagination.Page * pagination.Size).Take(pagination.Size).ToList();
            
            return Ok(new { totalAgencyCount, agency });
        }

        [HttpGet("[action]/{id}")]
        [Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> GetById(string id)
        {
            var ageny = await _agencyReadRepository.Table.Include(x => x.AgencyFiles).Select(x => new AgencyS
            {
                Id = x.Id.ToString(),
                Code = x.Code ?? "",
                Name = x.Name ?? "",
                Address = x.Address ?? "",
                Description = x.Description,
                TaxNumber = x.TaxNumber,
                Town = x.Town,
                TaxDeparmant = x.TaxDeparmant,
                City = x.City,
                Country = x.Country,
                LocalOrForeing = x.LocalOrForeing != null ? x.LocalOrForeing.toName() : "",
                State = x.State != null ? x.State.toName() : "",
                AgencyFiles = x.AgencyFiles.ToList(),
                Mail=x.Mail,
                PhoneNumber_1=x.PhoneNumber_1,
                PhoneNumber_2=x.PhoneNumber_2,
                ResponsibleName = x.ResponsibleName,
                WebSite = x.WebSite,
            }).FirstOrDefaultAsync(x => x.Id == id);

            return Ok(ageny);
        }
        [HttpGet("[action]/{id}")]
        [Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> DetailGetById(string id)
        {
            //var work = _workReadRepository.Select(x => x.AgencyId.ToString() == id, x => new WorkForAgency
            //{
            //    WorkId = x.Id.ToString(),
            //    WorkName = x.Name,
            //    WorkImagePath = x.PublishFiles.Where(p => p.Showcase).FirstOrDefault().Path
            //});
            //var author = _authorReadRepository.Select(a => a.AgencyId.ToString() == id, a => new AuthorForAgency
            //{
            //    AuthorId = a.Id.ToString(),
            //    AuthorNameSurname = a.Name + " " + a.Surname,
            //    AuthorImagePath = a.AuthorFiles.Where(f => f.Showcase).FirstOrDefault().Path
            //});
            var ageny = await _agencyReadRepository.Table.Include(x => x.AgencyFiles).Select(x => new
            {
                agcy = x,
                work = x.Works.Select(w => new WorkForAgency
                {
                    WorkId = w.Id.ToString(),
                    WorkName = w.Name,
                    WorkImagePath = w.PublishFiles.Where(p => p.Showcase).FirstOrDefault().Path
                }),
                author = x.Authors.Select(a => new AuthorForAgency
                {
                    AuthorId = a.Id.ToString(),
                    AuthorNameSurname = a.Name+" "+a.Surname,
                    AuthorImagePath = a.AuthorFiles.Where(f => f.Showcase).FirstOrDefault().Path
                })
            }).Select(x => new AgencyDetail
            {
                Id = x.agcy.Id.ToString(),
                Code = x.agcy.Code ?? "",
                Name = x.agcy.Name ?? "",
                Address = x.agcy.Address ?? "",
                Description = x.agcy.Description,
                TaxNumber = x.agcy.TaxNumber,
                Town = x.agcy.Town,
                TaxDeparmant = x.agcy.TaxDeparmant,
                City = x.agcy.City,
                Country = x.agcy.Country,
                LocalOrForeing = x.agcy.LocalOrForeing != null ? x.agcy.LocalOrForeing.toName() : "",
                State = x.agcy.State != null ? x.agcy.State.toName() : "",
                AgencyFiles = x.agcy.AgencyFiles,
                ResponsibleName = x.agcy.ResponsibleName,
                Mail=x.agcy.Mail,
                PhoneNumber_1=x.agcy.PhoneNumber_1,
                PhoneNumber_2=x.agcy.PhoneNumber_2,
                WebSite=x.agcy.WebSite,
                AuthorListForAgency = x.author,
                WorkListForAgency=x.work
                
            }).FirstOrDefaultAsync(x => x.Id == id);

            return Ok(ageny);
        }

        [HttpPost()]
        public async Task<IActionResult> CreateAgency(AgencyS agency)
        {
            Agency agncy = new Agency
            {
                Code = agency.Code,
                Name = agency.Name,
                Country = agency.Country,
                City = agency.City,
                Town = agency.Town,
                Address = agency.Address,
                LocalOrForeing = agency.LocalOrForeing != null ? agency.LocalOrForeing.GetEnum<LocalOrForeing>() : LocalOrForeing.Local,
                State = agency.State != null ? agency.State?.GetEnum<CurrentState>() : CurrentState.Active,
                TaxDeparmant = agency.TaxDeparmant,
                TaxNumber = agency.TaxNumber,
                Description = agency.Description,
                PhoneNumber_1 = agency.PhoneNumber_1,
                PhoneNumber_2 = agency.PhoneNumber_2,
                Mail = agency.Mail,
                ResponsibleName = agency.ResponsibleName,
                WebSite = agency.WebSite,
            };
            await _agencyWriteRepository.AddAsync(agncy);
            await _agencyWriteRepository.SaveAsync();
            return Ok(agncy);
            //return StatusCode((int)HttpStatusCode.Created);
        }
        [HttpPut]
        public async Task<IActionResult> Edit(AgencyS agency)
        {
            _agencyWriteRepository.Update(new()
            {
                Id =Guid.Parse(agency.Id),
                Code = agency.Code,
                Name = agency.Name,
                Country = agency.Country,
                City = agency.City,
                Town = agency.Town,
                Address = agency.Address,
                Description=agency.Description,
                LocalOrForeing=agency.LocalOrForeing.GetEnum<LocalOrForeing>(),
                State=agency.State.GetEnum<CurrentState>(),
                TaxDeparmant=agency.TaxNumber,
                TaxNumber=agency.TaxNumber,
                Mail=agency.Mail,
                PhoneNumber_1=agency.PhoneNumber_1,
                PhoneNumber_2=agency.PhoneNumber_2,
                ResponsibleName=agency.ResponsibleName,
                WebSite=agency.WebSite,
               
            });
            await _agencyWriteRepository.SaveAsync();

            return StatusCode((int)HttpStatusCode.Created);

        }
        [HttpDelete("{Id}")]
        [Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> Delete(string id)
        {
            await _agencyWriteRepository.RemoveAsync(id);
            await _agencyWriteRepository.SaveAsync();
            return Ok();
        }

        [HttpPost("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> Upload(string id)
        {
            //todo FileService sınıfında FileRenameAsync() metodu ile dosya adının aynı olmaması için bir yapı kuruldu video 28 tekrar izleyip yapıyı oluştur
            //var datas = await _storageService.UploadAsync("files", Request.Form.Files); AZURE için
            //var datas = await _fileService.UploadAsync("resorce/product-images", Request.Form.Files);

            var datas = await _storageService.UploadAsync($@"resorce\author-File", Request.Form.Files);

            Agency agency = await _agencyReadRepository.GetByIdAsync(id);
            
            var isActive = _agencyFileReadRepository.Select(x => x.EntityId == id, x => x).Any(a => a.IsActive);
            
            await _agencyFileWriteRepository.AddRangeAsync(datas.Select(d => new AgencyFile
            {
                FileName = d.filename,
                Path = d.pathOrContainerName,
                IsActive = !isActive,
                EntityId = id,
                Storage = _storageService.StorageName,
                Agencies = new List<Agency>() { agency }
            }).ToList());

            await _agencyFileWriteRepository.SaveAsync();
            return Ok();

        }

        [HttpGet("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> GetFiles([FromQuery] GetAgencyFileQueryRequest getAgencyFileQueryRequest)
        {
            List<GetAgencyFileQueryResponse> response = await _mediator.Send(getAgencyFileQueryRequest);
            return Ok(response);
        }
        
        [HttpGet("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> ChangeShowcaseImage([FromQuery] ChangeShowcaseImageCommandRequest changeShowcaseImageCommandRequest)
        {
            ChangeShowcaseImageCommandResponse response = await _mediator.Send(changeShowcaseImageCommandRequest);
            return Ok(response);
        }
    }
}
