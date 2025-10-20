using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Net;
using YayinEviApi.Application.Abstractions.Services;
using YayinEviApi.Application.DTOs.ProjectDtos;
using YayinEviApi.Application.DTOs.User;
using YayinEviApi.Application.Repositories.ProjectR;
using YayinEviApi.Application.RequestParameters;
using YayinEviApi.Domain.Entities.ProjectE;
using YayinEviApi.Domain.Entities.WarehouseE;
using YayinEviApi.Domain.Enum;
using YayinEviApi.Infrastructure.Operations;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace YayinEviApi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")]
    public class ProjectController : ControllerBase
    {
        readonly IProjectReadRepository _projectReadRepository;
        readonly IProjectWriteRepository _projectWriteRepository;
        private IUserService _userService;
        readonly CreateUser _user;
        private Expression<Func<Project, bool>>? _projectFilterExpression;

        public ProjectController(IProjectWriteRepository projectWriteRepository, IProjectReadRepository projectReadRepository, IUserService userService)
        {
            _projectWriteRepository = projectWriteRepository;
            _projectReadRepository = projectReadRepository;
            _userService = userService;
            _user = _userService.GetUser().Result;
            _projectFilterExpression = x => x.Id != null;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] Pagination pagination)
        {
            var ssjs = pagination.State.GetEnum<State>();
            _projectFilterExpression = pagination.State == "Tümü" ? x => x.Id != null: pagination.State != null ? x => x.State == pagination.State.GetEnum<State>() : x => x.Id != null;
            var totalProjectCount = _projectReadRepository.GetAll(false).Where(_projectFilterExpression).Count();

            var projects = _projectReadRepository.Table.Where(_projectFilterExpression).Select(x => new
            {
                prj = x,
                work = x.Work,
                agency = x.Work.Author.Agency,
                author = x.Work.Author,
                category = x.Work.Category,
                path = x.Work.PublishFiles.FirstOrDefault(p => p.Showcase).Path
            }).ToList().Select(x => new ProjectDto
            {
                Id = x.prj.Id.ToString(),
                Code = x.prj.Code,
                State = x.prj.State != null ? x.prj.State.toName() : "",
                AgencyId = x.agency.Id.ToString(),
                AgencyName = x.agency.Name,
                WorkId = x.prj.WorkId.ToString(),
                WorkCode = x.work.Code,
                WorkName = x.work.Name,
                ImagePath = x.path,
                AuthorId = x.author.Id.ToString(),
                AuthorNameAndSurnmae = x.author.Name + " " + x.author.Surname,
                Bandrol = x.work.Bandrol,
                Barcode = x.work.Barcode,
                CategoryId = x.work.CategoryId.ToString(),
                CategoryName = x.category.Name,
                CertificateNumber = x.work.CertificateNumber,
                Description = x.work.Description,
                ISBN = x.prj.Work.isbn,
                WorkOrginalName = x.work.WorkOrginalName,
                WorkLanguage = x.prj.Work.Language,
                Subject = x.work.Subject,
                CreatedDate = x.prj.CreatedDate,
                UpdatedDate = x.prj.UpdatedDate,
                CreatingUserId = x.prj.CreatingUserId,
                UpdatingUserId = x.prj.UpdatingUserId,
            }).Select(x => x)?.Skip(pagination.Page * pagination.Size).Take(pagination.Size);
            return Ok(new { totalProjectCount, projects });

        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var projects =await _projectReadRepository.Table.Select(x => new
            {
                prj = x,
                work = x.Work,
                agency = x.Work.Author.Agency,
                author = x.Work.Author,
                path = x.Work.PublishFiles.FirstOrDefault(p => p.Showcase).Path
            }).Select(x => new ProjectDto
            {
                Id = x.prj.Id.ToString(),
                Code = x.prj.Code,
                State = x.prj.State != null ? x.prj.State.toName() : "",
                AgencyId = x.agency.Id.ToString(),
                AgencyName = x.agency.Name,
                WorkId = x.prj.WorkId.ToString(),
                WorkCode = x.work.Code,
                WorkName = x.work.Name,
                ImagePath = x.path,
                AuthorId = x.author.Id.ToString(),
                AuthorNameAndSurnmae = x.prj.Work.Author.Name + " " + x.prj.Work.Author.Surname,
                Bandrol = x.work.Bandrol,
                Barcode = x.work.Barcode,
                CategoryId = x.work.CategoryId.ToString(),
                CategoryName = x.work.Category.Name,
                CertificateNumber = x.work.CertificateNumber,
                Description = x.work.Description,
                ISBN = x.prj.Work.isbn,
                WorkOrginalName = x.work.WorkOrginalName,
                WorkLanguage = x.prj.Work.Language,
                Subject = x.work.Subject,
                CreatedDate = x.prj.CreatedDate,
                UpdatedDate = x.prj.UpdatedDate,
                CreatingUserId=x.prj.CreatingUserId,
                UpdatingUserId=x.prj.UpdatingUserId,
            }).FirstOrDefaultAsync(x=>x.Id==id);

            return Ok(projects);
        }

        [HttpPost]
        public async Task<IActionResult> Add(ProjectDto project)
        {
            Project _project = new()
            {
                Code = project.Code,
                WorkId = Guid.Parse(project.WorkId),
                State = project.State.GetEnum<State>(),
                CreatingUserId = _user.UserId,
            };
            await _projectWriteRepository.AddAsync(_project);
            await _projectWriteRepository.SaveAsync();
           
            project.Id=_project.Id.ToString();
            
            return Ok(project);

        }

        [HttpPut]
        public async Task<IActionResult> Edit(ProjectDto project)
        {
            Project _project = new()
            {
                Id=Guid.Parse(project.Id),
                Code = project.Code,
                WorkId = Guid.Parse(project.WorkId),
                State = project.State.GetEnum<State>(),
                CreatingUserId = project.CreatingUserId,
                UpdatingUserId = _user.UserId,
            };
            _projectWriteRepository.Update(_project);
            await _projectWriteRepository.SaveAsync();

            return Ok(project);
            //return StatusCode((int)HttpStatusCode.Created);

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
