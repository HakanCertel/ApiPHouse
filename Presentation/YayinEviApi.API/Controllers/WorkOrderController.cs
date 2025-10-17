using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Net;
using YayinEviApi.Application.Abstractions.Services;
using YayinEviApi.Application.Abstractions.Storage;
using YayinEviApi.Application.DTOs.User;
using YayinEviApi.Application.DTOs.WorkOrderDtos;
using YayinEviApi.Application.Repositories;
using YayinEviApi.Application.Repositories.HubMessagesR;
using YayinEviApi.Application.Repositories.IWorkOrderR;
using YayinEviApi.Application.RequestParameters;
using YayinEviApi.Domain.Entities;
using YayinEviApi.Domain.Entities.Identity;
using YayinEviApi.Domain.Entities.WorkOrderE;
using YayinEviApi.Domain.Enum;
using YayinEviApi.Infrastructure.Operations;
using YayinEviApi.Persistence.Repositories.FileMamgementR;

namespace YayinEviApi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")]
    public class WorkOrderController : ControllerBase
    {
        readonly IUserService _userService;
        readonly UserManager<AppUser> _userManager;
        private readonly IStorageService _storageService;
        readonly IWorkOrderWriteRepository _workOrderWriteRepository;
        readonly IWorkOrderReadRepository _workOrderReadRepository;
        readonly IWorkAssignedUsersReadRepository _workAssignedUsersReadRepository;
        readonly IWorkAssignedUsersWriteRepository _workAssignedUsersWriteRepository;
        readonly IFileManagementReadRepository _fileManagementReadRepository;
        readonly IFileManagementWriteRepository _fileManagementWriteRepository;
        readonly IWorkOrderMessagesReadRepository _workOrderMessagesReadRepository;
        readonly IWorkOrderMessagesWriteRepository _workOrderMessagesWriteRepository;
        readonly IHubMessageWriteRepository _hubMessageWriteRepository;
        readonly CreateUser _user;
        Expression<Func<WorkOrder, bool>>? _workOrderExpression;
        public WorkOrderController(IWorkOrderReadRepository workOrderReadRepository, IWorkOrderWriteRepository workOrderWriteRepository, IUserService userService, IWorkAssignedUsersReadRepository workAssignedUsersReadRepository, IWorkAssignedUsersWriteRepository workAssignedUsersWriteRepository, IFileManagementReadRepository fileManagementReadRepository, UserManager<AppUser> userManager, IWorkOrderMessagesWriteRepository workOrderMessagesWriteRepository, IWorkOrderMessagesReadRepository workOrderMessagesReadRepository, IStorageService storageService, IFileManagementWriteRepository fileManagementWriteRepository, IHubMessageWriteRepository hubMessageWriteRepository)
        {
            _storageService = storageService;
            _fileManagementWriteRepository = fileManagementWriteRepository;
            _workOrderReadRepository = workOrderReadRepository;
            _workOrderWriteRepository = workOrderWriteRepository;
            _userService = userService;
            _workAssignedUsersReadRepository = workAssignedUsersReadRepository;
            _workAssignedUsersWriteRepository = workAssignedUsersWriteRepository;
            _fileManagementReadRepository = fileManagementReadRepository;
            _userManager = userManager;
            _workOrderMessagesWriteRepository = workOrderMessagesWriteRepository;
            _workOrderMessagesReadRepository = workOrderMessagesReadRepository;
            _hubMessageWriteRepository = hubMessageWriteRepository;

            _user = _userService.GetUser().Result;
            //_user.ImagePath = _fileManagementReadRepository.FindAsync(x => x.EntityId == _user.UserId && x.IsActive, x => x).Result?.Path;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]Pagination pagination)
        {
            var totalWorkOrderCount = _workOrderReadRepository.GetAll(false).Count();
           
            var woList = _workOrderReadRepository.Table.Select(x => new
            {
                wo = x,
                prj = x.Project,
                work = x.Project.Work,
                author=x.Project.Work.Author,
                agency=x.Project.Work.Agency,
                path = x.Project.Work.PublishFiles.FirstOrDefault(p => p.IsActive).Path,
                prc = x.Proccess,
                assignedUsers = x.WorkAssignedUsers,
                messages = x.WorkOrderMessages
            }).Select(x => new WorkOrderDto
            {
                Id = x.wo.Id.ToString(),
                WorkOrderCode = x.wo.Code,
                ProjectId = x.prj.Id.ToString(),
                ProjectCode = x.prj.Code,
                ProccessId = x.prc.Id.ToString(),
                ProccessName = x.prc.Name,
                ProccessCategoryName = x.prc.ProccessCategory.Name,
                AssignedUserId = x.wo.AssignedUserId,
                CreatingUserId = x.wo.CreatingUserId,
                UpdatingUserId = x.wo.UpdatingUserId,
                AgencyId = x.agency.Id.ToString(),
                AgencyName = x.agency.Name,
                WorkId = x.prj.WorkId.ToString(),
                WorkName = x.work.Name,
                ImagePath = x.path,
                AuthorId = x.author.Id.ToString(),
                AuthorName = x.author.Name + " " + x.author.Surname,
                Bandrol = x.work.Bandrol,
                Barcode = x.work.Barcode,
                CategoryId = x.work.CategoryId.ToString(),
                CategoryName = x.work.Category.Name,
                CertificateNumber = x.work.CertificateNumber,
                Description = x.prj.Description,
                ISBN = x.work.isbn,
                WorkOrginalName = x.work.WorkOrginalName,
                Language = x.work.Language,
                Subject = x.work.Subject,
                CreatedDate = x.prj.CreatedDate,
                UpdatedDate = x.prj.UpdatedDate,
                WorkState=x.wo.WorkState.toName(),
                
                WorkOrderMessages=x.messages,
                WorkAssignedUsers=x.assignedUsers,
            })
                .Select(x => x)?.Skip(pagination.Page * pagination.Size).Take(pagination.Size);
            return Ok(new { totalWorkOrderCount, woList });

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByProjectId(string id)
        {
            _workOrderExpression = id == null ? null : x => x.ProjectId.ToString() == id;

            var workList = _workOrderExpression == null ? _workOrderReadRepository.Table : _workOrderReadRepository.Table.Where(_workOrderExpression);
            var totalWorkOrderCount = workList.Count();
            var woList = workList.Select(x => new
            {
                wo = x,
                prj = x.Project,
                work = x.Project.Work,
                author = x.Project.Work.Author,
                agency = x.Project.Work.Agency,
                path = x.Project.Work.PublishFiles.FirstOrDefault(p => p.IsActive).Path,
                prc = x.Proccess,
                assignedUsers = x.WorkAssignedUsers,
                messages = x.WorkOrderMessages
            }).Select(x => new WorkOrderDto
            {
                Id = x.wo.Id.ToString(),
                WorkOrderCode = x.wo.Code,
                ProjectId = x.prj.Id.ToString(),
                ProjectCode = x.prj.Code,
                ProccessId = x.prc.Id.ToString(),
                ProccessName = x.prc.Name,
                ProccessCategoryName = x.prc.ProccessCategory.Name,
                AssignedUserId = x.wo.AssignedUserId,
                CreatingUserId = x.wo.CreatingUserId,
                UpdatingUserId = x.wo.UpdatingUserId,
                AgencyId = x.agency.Id.ToString(),
                AgencyName = x.agency.Name,
                WorkId = x.prj.WorkId.ToString(),
                WorkName = x.work.Name,
                ImagePath = x.path,
                AuthorId = x.author.Id.ToString(),
                AuthorName = x.author.Name + " " + x.author.Surname,
                Bandrol = x.work.Bandrol,
                Barcode = x.work.Barcode,
                CategoryId = x.work.CategoryId.ToString(),
                CategoryName = x.work.Category.Name,
                CertificateNumber = x.work.CertificateNumber,
                Description = x.prj.Description,
                ISBN = x.work.isbn,
                WorkOrginalName = x.work.WorkOrginalName,
                Language = x.work.Language,
                Subject = x.work.Subject,
                CreatedDate = x.prj.CreatedDate,
                UpdatedDate = x.prj.UpdatedDate,
                WorkState = x.wo.WorkState.toName(),

                WorkOrderMessages = x.messages,
                WorkAssignedUsers = x.assignedUsers,
            });
            return Ok(new { totalWorkOrderCount, woList });

        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            _user.ImagePath = _fileManagementReadRepository.FindAsync(x => x.EntityId == _user.UserId && x.IsActive, x => x).Result?.Path;
            var wo = await _workOrderReadRepository.Table.Select(x => new
            {
                wo = x,
                prj = x.Project,
                agency=x.Project.Work.Agency,
                work = x.Project.Work,
                path = x.Project.Work.PublishFiles.FirstOrDefault(p => p.Showcase).Path,
                prc = x.Proccess,
                assignedUsers = x.WorkAssignedUsers,
                messages = x.WorkOrderMessages

            }).Select(x => new WorkOrderDto
            {
                Id = x.wo.Id.ToString(),
                AuthUserId = _user.UserId,
                AuthUserName = _user.Username,
                AuthUserNameSurname = _user.NameSurname,
                AuthImagePath = _user.ImagePath,
                WorkOrderCode = x.wo.Code,
                ProccessId = x.prc.Id.ToString(),
                ProccessName = x.prc.Name,
                ProccessCategoryName = x.prc.ProccessCategory.Name,
                ProjectId = x.prj.Id.ToString(),
                ProjectState=x.prj.State.toName(),
                AssignedUserId = x.wo.AssignedUserId,
                CreatingUserId = x.wo.CreatingUserId,
                UpdatingUserId = x.wo.UpdatingUserId,
                AgencyId =x.agency!=null ?x.agency.Id.ToString():null,
                AgencyName = x.agency != null ?x.agency.Name:null,
                WorkId = x.prj.WorkId.ToString(),
                WorkName = x.work.Name,
                ImagePath = x.path,
                AuthorId = x.prj.Work.AuthorId.ToString(),
                AuthorName = x.prj.Work.Author.Name + " " + x.prj.Work.Author.Surname,
                Bandrol = x.prj.Work.Bandrol,
                Barcode = x.prj.Work.Barcode,
                CategoryId = x.prj.Work.CategoryId.ToString(),
                CategoryName = x.prj.Work.Category.Name,
                CertificateNumber = x.prj.Work.CertificateNumber,
                Description = x.wo.Description,
                ISBN = x.prj.Work.isbn,
                WorkOrginalName = x.prj.Work.WorkOrginalName,
                Language = x.prj.Work.Language,
                FinishedDate=x.wo.FinishedDate,
                Subject = x.prj.Work.Subject,
                CreatedDate = x.prj.CreatedDate,
                UpdatedDate = x.prj.UpdatedDate,
                WorkAssignedUsers = x.assignedUsers,
                WorkOrderMessages = x.messages,
                WorkState = x.wo.WorkState.toName(),
            }).FirstOrDefaultAsync(x=>x.Id == id);

            return Ok(wo);
        }

        [HttpPost]
        public async Task<IActionResult> Add(WorkOrderDto[] workOrderList)
        {
            List<WorkOrder> woList=new List<WorkOrder>();
            
            foreach (var item in workOrderList)
            {
                var wo=new WorkOrder
                {
                    Code = item.WorkOrderCode,
                    FinishedDate=DateTime.Now,
                    AssignedUserId = item.AssignedUserId,
                    ProjectId=Guid.Parse(item.ProjectId),
                    ProccessId = Guid.Parse(item.ProccessId),
                    CreatingUserId =_userService.GetUser().Result.UserId,
                    WorkState=WorkState.Waiting,
                };
                
                woList.Add(wo);
            }
            await _workOrderWriteRepository.AddRangeAsync(woList);
            await _workOrderWriteRepository.SaveAsync();

            //var item = await _projectReadRepository.GetAll().OrderBy(x => x.CreatedDate).LastAsync();
            // return Ok(item);
            return StatusCode((int)HttpStatusCode.Created);

        }
        
        [HttpPut]
        public async Task<IActionResult> Edit(WorkOrderDto wo)
        {
            _workOrderWriteRepository.Update(new()
            {
               Id = Guid.Parse(wo.Id),
               AssignedUserId = wo.AssignedUserId,
               Code=wo.WorkOrderCode,
               UpdatingUserId = _userService.GetUser().Result.UserId,
               Description=wo.Description,
               FinishedDate=wo.FinishedDate,
               ProccessId=Guid.Parse(wo.ProccessId),
               ProjectId = Guid.Parse(wo.ProjectId),
               CreatingUserId=wo.CreatingUserId,
               WorkState = wo.WorkState.GetEnum<WorkState>(),
            });
            await _workOrderWriteRepository.SaveAsync();
           
            return StatusCode((int)HttpStatusCode.Created);

        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _workOrderWriteRepository.RemoveAsync(id);
            await _workOrderWriteRepository.SaveAsync();
            return Ok();
        }


        [HttpPost("[action]")]
        //[Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> UploadForWorkOrder(string entityId, string? whichPage)
        {
            var datas = await _storageService.UploadAsync($@"resorce\workOrder-images", Request.Form.Files);

            var entity = _fileManagementReadRepository.GetSingleAsync(x => x.EntityId == entityId && x.IsActive).Result;

            if(entity != null)
                entity.IsActive = false;
            await _fileManagementWriteRepository.AddRangeAsync(datas.Select(d => new FileManagement
            {
                FileName = d.filename,
                Path = d.pathOrContainerName,
                Storage = _storageService.StorageName,
                EntityId = entityId,
                IsActive = true,
                WhichPage = whichPage,
                WhichClass = "AppUserClass",
                AddingUserId=_user.UserId,
            }).ToList());
            
            await _fileManagementWriteRepository.SaveAsync();
            if(entity!=null)
                _fileManagementWriteRepository.Update(entity);

            return Ok();

        }
        [HttpGet("[action]")]
        //[Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> GetFiles(string entityId)
        {
            IList<FileManagement> file= _fileManagementReadRepository.Select(x => x.EntityId == entityId, x => x).ToList();

            return Ok(file);
        }
        [HttpGet("[action]")]
        //[Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> ChangeShowcaseImage(string imageId, string id)
        {
            var files = _fileManagementReadRepository.Select(x => x.EntityId == id, x => x);
            var file1 = files.Where(x => x.IsActive).FirstOrDefault();
            if (file1 != null)
            {
                file1.IsActive = false;
            }
            var file2 = files.Where(x => x.EntityId == id && x.Id.ToString() == imageId).FirstOrDefault();
            if (file2 != null)
            {
                file2.IsActive = true;
            }
            await _fileManagementWriteRepository.SaveAsync();

            return Ok();
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> AddUserForWorkOrder(WorkAssignedUsersDto[] user)
        {
            List<WorkAssignedUsers> woList = new List<WorkAssignedUsers>();

            foreach (var item in user)
            {
                var wo = new WorkAssignedUsers
                {
                    WorkOrderId =Guid.Parse(item.WorkOrderId),
                    UserId = item.UserId,
                };

                woList.Add(wo);
            }
            await _workAssignedUsersWriteRepository.AddRangeAsync(woList);
            await _workAssignedUsersWriteRepository.SaveAsync();

            return StatusCode((int)HttpStatusCode.Created);

        }
        
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> DeleteUserForWorkOrder(string id)
        {
            await _workAssignedUsersWriteRepository.RemoveAsync(id);
            await _workAssignedUsersWriteRepository.SaveAsync();
            return Ok();
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetAllWorkAssignedUserForWorkOrder(string id) 
        {
            
            var assignedUsers = _workAssignedUsersReadRepository.Select(x => x.WorkOrderId == Guid.Parse(id),x=> new WorkAssignedUsersDto
            {
                Id=x.Id.ToString(),
                UserId = x.UserId,
            }).ToList();
            
            if(assignedUsers != null || assignedUsers.Count != 0)
            {
                assignedUsers.ForEach(x =>
                {
                    var user = _userManager.FindByIdAsync(x.UserId).Result;
                    x.UserName = _user.Username;
                    x.NameAndSurname=_user.NameSurname;
                    x.ImagePath = _fileManagementReadRepository.FindAsync(y => y.EntityId == x.UserId && y.IsActive, y => y).Result?.Path;
                });
            }
            return Ok(assignedUsers);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddMessagesForWorkOrder(WorkOrderMessagesDto message)
        {
            await _workOrderMessagesWriteRepository.AddAsync(new()
            {
                UserId=message.UserId,
                WorkOrderId=Guid.Parse(message.WorkOrderId),
                Message=message.Messages,
            });
            await _workOrderMessagesWriteRepository.SaveAsync();
            return StatusCode((int)HttpStatusCode.Created);
        }

        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> DeleteMessageForWorkOrder(string id)
        {
            await _workOrderMessagesWriteRepository.RemoveAsync(id);
            await _workOrderMessagesWriteRepository.SaveAsync();
            return Ok();
        }

        [HttpGet("[action]/{id?}")]
        public async Task<IActionResult> GetAllMessagesForWorkOrder(string? id)
        {
            List<WorkOrderMessagesDto> messages=null;
            if (id != null)
                messages = _workOrderMessagesReadRepository.Select(x => x.WorkOrderId.ToString() == id, x => new WorkOrderMessagesDto
            {
                Id = x.Id.ToString(),
                UserId = x.UserId,
                WorkOrderId = x.WorkOrderId.ToString(),
                Messages = x.Message,
                CreatedDate = x.CreatedDate,
            }).ToList();
            if (messages != null || messages.Count != 0)
            {
                messages.ForEach(x =>
                {
                    var user = _userManager.FindByIdAsync(x.UserId).Result;
                    x.UserName = user?.UserName;
                    x.NameAndSurname = user?.NameSurname;
                    x.ImagePath = _fileManagementReadRepository.FindAsync(y => y.EntityId == x.UserId && y.IsActive, y => y).Result?.Path;
                });
            }

            return Ok(messages);
        }

    }
}
