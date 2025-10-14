using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using YayinEviApi.Application.Abstractions.Services;
using YayinEviApi.Application.DTOs.HelperEntityDtos;
using YayinEviApi.Application.DTOs.User;
using YayinEviApi.Application.Repositories.IHelperEntitiesR.IWorkTypeR;

namespace YayinEviApi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")]
    public class WorkTypeController : ControllerBase
    {
        private IUserService _userService;
        readonly CreateUser _user;
        readonly IWorkTypeWriteRepository _workTypeWriteRepository;

        readonly IWorkTypeReadRepository _workTypeReadRepository;
        public WorkTypeController(IWorkTypeWriteRepository workTypeWriteRepository, IWorkTypeReadRepository workTypeReadRepository, IUserService userService)
        {
            _workTypeWriteRepository = workTypeWriteRepository;
            _workTypeReadRepository = workTypeReadRepository;
            _userService = userService;
            _user = _userService.GetUser().Result;
        }
        [HttpGet()]
        public async Task<IActionResult> GetAll()
        {
            var categoryTypes = _workTypeReadRepository.Select(null, x => new WorkTypeDto
            {
                Id = x.Id.ToString(),
                TypeCode = x.TypeCode,
                TypeName = x.TypeName,
                Description = x.Description,
                CreatingUserId=x.CreatingUserId,
                WorkCategoryId=x.WorkCategoryId.ToString(),
                WorkCategoryName=x.WorkCategory.Name,

            }).ToList();
            return Ok(categoryTypes);
        }

        [HttpGet("[action]/{id}")]
        [Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> GetById(string id)
        {
            var workCategoryType = _workTypeReadRepository.Table.Where(x=>x.Id.ToString()==id).Select( x=>new 
            {
                type=x,
                category=x.WorkCategory,
            }).Select(x=> new WorkTypeDto
            {
                Id = x.type.Id.ToString(),
                TypeCode = x.type.TypeCode ?? "",
                TypeName = x.type.TypeName ?? "",
                IsActive = x.type.IsActive,
                WorkCategoryId=x.type.WorkCategoryId.ToString(),
                WorkCategoryName=x.category.Name,

            });

            return Ok(workCategoryType);
        }

        [HttpPost()]
        public async Task<IActionResult> Add(WorkTypeDto createWorktype)
        {
            await _workTypeWriteRepository.AddAsync(new()
            {
                WorkCategoryId=Guid.Parse(createWorktype.WorkCategoryId),
                TypeCode = createWorktype.TypeCode,
                TypeName = createWorktype.TypeName,
                Description = createWorktype.Description,
                IsActive=createWorktype.IsActive,
                CreatingUserId=_user.UserId,
            });
            await _workTypeWriteRepository.SaveAsync();

            return StatusCode((int)HttpStatusCode.Created);
        }

        [HttpPut]
        public async Task<IActionResult> Edit(WorkTypeDto workType)
        {

            _workTypeWriteRepository.Update(new()
            {
                Id = Guid.Parse(workType.Id),
                WorkCategoryId=Guid.Parse(workType.WorkCategoryId),
                TypeCode = workType.TypeCode,
                TypeName = workType.TypeName,
                IsActive = workType.IsActive,
                CreatingUserId = workType.CreatingUserId,
                UpdatingUserId = _user.UserId,
            });
            await _workTypeWriteRepository.SaveAsync();

            return StatusCode((int)HttpStatusCode.Created);

        }

        [HttpDelete("{Id}")]
        [Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> Delete(string id)
        {
            await _workTypeWriteRepository.RemoveAsync(id);
            await _workTypeWriteRepository.SaveAsync();
            return Ok();
        }
    }
}
