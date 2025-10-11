using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using YayinEviApi.Application.DTOs.HelperEntityDtos;
using YayinEviApi.Application.Repositories.IHelperEntitiesR.IWorkCategoryR;

namespace YayinEviApi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = "Admin")]
    public class WorkCategoryController : ControllerBase
    {
        readonly IWorkCategoryWriteRepository _workCategoryWriteRepository;
        readonly IWorkCategoryReadRepository _workCategoryReadRepository;

        public WorkCategoryController(IWorkCategoryWriteRepository workCategoryWriteRepository, IWorkCategoryReadRepository workCategoryReadRepository)
        {
            _workCategoryWriteRepository = workCategoryWriteRepository;
            _workCategoryReadRepository = workCategoryReadRepository;
        }

        [HttpPost()]
        public async Task<IActionResult> CretaeWorkCategory(CreateWorkCategory createWorkCategory)
        {
            await _workCategoryWriteRepository.AddAsync(new()
            {
                WorkTypeId = createWorkCategory.WorkTypeId,
                Code = createWorkCategory.Code,
                Name = createWorkCategory.Name,
                Description = createWorkCategory.Description,
            });
            await _workCategoryWriteRepository.SaveAsync();

            return StatusCode((int)HttpStatusCode.Created);
        }
        
        [HttpGet()]
        public async Task<IActionResult> GetAllWorkCategory()
        {
            var work = _workCategoryReadRepository.Select(null,x=>new WorkCategoryL
            {
                CategoryId=x.Id.ToString(),
                WorkTypeId = x.WorkTypeId.ToString(),
                WorkTypeName=x.WorkType.TypeName,
                CategoryCode=x.Code,
                CategoryName = x.Name,
                Description = x.Description,

            }).ToList();
            return Ok(work);
        }
    }


    public class CreateWorkCategory
    {
        public Guid WorkTypeId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

    }

}
