using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using YayinEviApi.Application.Abstractions.Services;
using YayinEviApi.Application.DTOs.HelperEntityDtos;
using YayinEviApi.Application.DTOs.User;
using YayinEviApi.Application.Repositories.IHelperEntitiesR.IWorkCategoryR;

namespace YayinEviApi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")]
    public class WorkCategoryController : ControllerBase
    {
        private IUserService _userService;
        readonly CreateUser _user;
        readonly IWorkCategoryWriteRepository _workCategoryWriteRepository;
        readonly IWorkCategoryReadRepository _workCategoryReadRepository;

        public WorkCategoryController(IWorkCategoryWriteRepository workCategoryWriteRepository, IWorkCategoryReadRepository workCategoryReadRepository, IUserService userService)
        {
            _workCategoryWriteRepository = workCategoryWriteRepository;
            _workCategoryReadRepository = workCategoryReadRepository;
            _userService = userService;
            _user = _userService.GetUser().Result;
        }

        [HttpPost()]
        public async Task<IActionResult> Add(WorkCategoryDto createWorkCategory)
        {
            await _workCategoryWriteRepository.AddAsync(new()
            {
                Code = createWorkCategory.CategoryCode,
                Name = createWorkCategory.CategoryName,
                Description = createWorkCategory.Description,
                IsActive=true,
                CreatingUserId=_user.UserId,
            });
            await _workCategoryWriteRepository.SaveAsync();

            return StatusCode((int)HttpStatusCode.Created);
        }
        [HttpPut]
        public async Task<IActionResult> Edit(WorkCategoryDto workCategory)
        {

            _workCategoryWriteRepository.Update(new()
            {
                Id = Guid.Parse(workCategory.CategoryId),
                Code = workCategory.CategoryCode,
                Name = workCategory.CategoryName,
                IsActive = workCategory.IsActive,
                CreatingUserId=workCategory.CreatingUserId,
                UpdatingUserId = _user.UserId,
            });
            await _workCategoryWriteRepository.SaveAsync();

            return StatusCode((int)HttpStatusCode.Created);

        }

        [HttpDelete("{Id}")]
        [Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> Delete(string id)
        {
            await _workCategoryWriteRepository.RemoveAsync(id);
            await _workCategoryWriteRepository.SaveAsync();
            return Ok();
        }

        [HttpGet()]
        public async Task<IActionResult> GetAll()
        {
            var work = _workCategoryReadRepository.Select(null,x=>new WorkCategoryDto
            {
                CategoryId=x.Id.ToString(),
                CategoryCode=x.Code,
                CategoryName = x.Name,
                Description = x.Description,

            }).ToList();
            return Ok(work);
        }

        [HttpGet("[action]/{id}")]
        [Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> GetById(string id)
        {
            var workCategory = await _workCategoryReadRepository.GetByIdAsync(id);
            var newWorkCategory = new WorkCategoryDto
            {
                CategoryId = workCategory.Id.ToString(),
                CategoryCode = workCategory.Code ?? "",
                CategoryName = workCategory.Name ?? "",
                IsActive = workCategory.IsActive,
            };

            return Ok(newWorkCategory);
        }
    }

}
