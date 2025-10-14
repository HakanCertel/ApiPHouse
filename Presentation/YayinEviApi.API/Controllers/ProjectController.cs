using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using YayinEviApi.Application.DTOs.ProjectDtos;
using YayinEviApi.Application.Repositories.ProjectR;
using YayinEviApi.Application.RequestParameters;
using YayinEviApi.Domain.Enum;
using YayinEviApi.Infrastructure.Operations;

namespace YayinEviApi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = "Admin")]
    public class ProjectController : ControllerBase
    {
        readonly IProjectReadRepository _projectReadRepository;
        readonly IProjectWriteRepository _projectWriteRepository;

        public ProjectController(IProjectWriteRepository projectWriteRepository, IProjectReadRepository projectReadRepository)
        {
            _projectWriteRepository = projectWriteRepository;
            _projectReadRepository = projectReadRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] Pagination pagination)
        {
            var totalProjectCount = _projectReadRepository.GetAll(false).Count();

            var projects = _projectReadRepository.Table.Select(x => new
            {
                prj=x,
                path=x.Work.PublishFiles.FirstOrDefault(p=>p.Showcase).Path
            }).Select(x => new ProjectDto
            {
                Id = x.prj.Id.ToString(),
                Code = x.prj.Code,
                ContractStartDate = x.prj.ContractStartDate,
                ContractFinishDate = x.prj.ContractFinishDate,
                ContractPrice = x.prj.ContractPrice,
                State = x.prj.State != null ? x.prj.State.toName() : "",
                AgencyId = x.prj.AgencyId.ToString(),
                AgencyName = x.prj.Agency.Name,
                WorkId = x.prj.WorkId.ToString(),
                WorkCode=x.prj.Work.Code,
                WorkName = x.prj.Work.Name,
                ImagePath=x.path,
                AuthorId = x.prj.Work.AuthorId.ToString(),
                AuthorNameAndSurnmae = x.prj.Work.Author.Name + " " + x.prj.Work.Author.Surname,
                Bandrol = x.prj.Work.Bandrol,
                Barcode = x.prj.Work.Barcode,
                CategoryId = x.prj.Work.CategoryId.ToString(),
                CategoryName = x.prj.Work.Category.Name,
                CertificateNumber = x.prj.Work.CertificateNumber,
                Description = x.prj.Work.Description,
                //FirstPrintingDate = x.prj.Work.FirstPrintingDate,
                ISBN = x.prj.Work.isbn,
                //LastPrintingQuantity = x.prj.Work.LastPrintingQuantity,
                //LasttPrintingDate = x.prj.Work.LasttPrintingDate,
                //PrintingHouse = x.prj.Work.PrintingHouse.ToString(),
                //ProjectName = x.prj.Work.ProjectName.ToString(),
                //StockQuantity = x.prj.Work.StockQuantity,
                //NameTranslating = x.prj.Work.NameTranslating,
                //NameDrawing = x.prj.Work.NameDrawing,
                //NameReading = x.prj.Work.NameReading,
                //NameReducting = x.prj.Work.NameReducting,
                //NameTypeSetting = x.prj.Work.NameTypeSetting,
                WorkOrginalName = x.prj.Work.WorkOrginalName,
                WorkLanguage = x.prj.Work.Language,
                Subject = x.prj.Work.Subject,
                CreatedDate = x.prj.CreatedDate,
                UpdatedDate = x.prj.UpdatedDate,
            })
                .Select(x => x)?.Skip(pagination.Page * pagination.Size).Take(pagination.Size);
            return Ok(new { totalProjectCount, projects });

        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var projects =await _projectReadRepository.Table.Select(x => new
            {
                prj = x,
                path = x.Work.PublishFiles.FirstOrDefault(p => p.Showcase).Path
            }).Select(x => new ProjectDto
            {
                Id = x.prj.Id.ToString(),
                Code = x.prj.Code,
                ContractStartDate = x.prj.ContractStartDate,
                ContractFinishDate = x.prj.ContractFinishDate,
                ContractPrice = x.prj.ContractPrice,
                State = x.prj.State != null ? x.prj.State.toName() : "",
                AgencyId = x.prj.AgencyId.ToString(),
                AgencyName = x.prj.Agency.Name,
                WorkId = x.prj.WorkId.ToString(),
                WorkCode = x.prj.Work.Code,
                WorkName = x.prj.Work.Name,
                ImagePath = x.path,
                AuthorId = x.prj.Work.AuthorId.ToString(),
                AuthorNameAndSurnmae = x.prj.Work.Author.Name + " " + x.prj.Work.Author.Surname,
                Bandrol = x.prj.Work.Bandrol,
                Barcode = x.prj.Work.Barcode,
                CategoryId = x.prj.Work.CategoryId.ToString(),
                CategoryName = x.prj.Work.Category.Name,
                CertificateNumber = x.prj.Work.CertificateNumber,
                Description = x.prj.Work.Description,
                //FirstPrintingDate = x.prj.Work.FirstPrintingDate,
                ISBN = x.prj.Work.isbn,
                //LastPrintingQuantity = x.prj.Work.LastPrintingQuantity,
                //LasttPrintingDate = x.prj.Work.LasttPrintingDate,
                //PrintingHouse = x.prj.Work.PrintingHouse.ToString(),
                //ProjectName = x.prj.Work.ProjectName.ToString(),
                //StockQuantity = x.prj.Work.StockQuantity,
                //NameTranslating = x.prj.Work.NameTranslating,
                //NameDrawing = x.prj.Work.NameDrawing,
                //NameReading = x.prj.Work.NameReading,
                //NameReducting = x.prj.Work.NameReducting,
                //NameTypeSetting = x.prj.Work.NameTypeSetting,
                WorkOrginalName = x.prj.Work.WorkOrginalName,
                WorkLanguage = x.prj.Work.Language,
                Subject = x.prj.Work.Subject,
                CreatedDate = x.prj.CreatedDate,
                UpdatedDate = x.prj.UpdatedDate,
            }).FirstOrDefaultAsync(x=>x.Id==id);

            return Ok(projects);
        }

        [HttpPost]
        public async Task<IActionResult> Add(ProjectDto project)
        {
            await _projectWriteRepository.AddAsync(new()
            {
                AgencyId=Guid.Parse( project.AgencyId ),
                Code = project.Code,
                WorkId = Guid.Parse( project.WorkId ),
                State=project.State.GetEnum<State>(),
                ContractFinishDate=project.ContractFinishDate,
                ContractPrice=project.ContractPrice,
                ContractStartDate=project.ContractStartDate,
            });
            await _projectWriteRepository.SaveAsync();

            //var item = await _projectReadRepository.GetAll().OrderBy(x => x.CreatedDate).LastAsync();
           // return Ok(item);
            return StatusCode((int)HttpStatusCode.Created);

        }

        [HttpPut]
        public async Task<IActionResult> Edit(ProjectDto project)
        {
            _projectWriteRepository.Update(new()
            {
                Id=Guid.Parse(project.Id),
                AgencyId = Guid.Parse(project.AgencyId),
                Code = project.Code,
                WorkId = Guid.Parse(project.WorkId),
                State = project.State.GetEnum<State>(),
                ContractFinishDate = project.ContractFinishDate,
                ContractPrice = project.ContractPrice,
                ContractStartDate = project.ContractStartDate,
                
            });
            await _projectWriteRepository.SaveAsync();

            return StatusCode((int)HttpStatusCode.Created);

        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _projectWriteRepository.RemoveAsync(id);
            await _projectWriteRepository.SaveAsync();
            return Ok();
        }

    }
}
