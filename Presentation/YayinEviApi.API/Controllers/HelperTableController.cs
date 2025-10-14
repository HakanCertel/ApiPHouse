using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using YayinEviApi.Application.DTOs.HelperEntityDtos;
using YayinEviApi.Application.Features.Commands.Product.CreateProduct;
using YayinEviApi.Application.Repositories;
using YayinEviApi.Application.Repositories.IHelperEntitiesR.IWorkCategoryR;
using YayinEviApi.Application.Repositories.IHelperEntitiesR.IWorkTypeR;

namespace YayinEviApi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = "Admin")]
    public class HelperTableController : ControllerBase
    {
        readonly IWorkTypeWriteRepository _workTypeWriteRepository;
        readonly IWorkTypeReadRepository _workTypeReadRepository;
        readonly IWorkCategoryWriteRepository _workCategoryWriteRepository;

        public HelperTableController(IWorkTypeWriteRepository workTypeWriteRepository, IWorkCategoryWriteRepository workCategoryWriteRepository, IWorkTypeReadRepository workTypeReadRepository)
        {
            _workTypeWriteRepository = workTypeWriteRepository;
            _workCategoryWriteRepository = workCategoryWriteRepository;
            _workTypeReadRepository = workTypeReadRepository;
        }
        [HttpGet("cwt")]
        public async Task<IActionResult> GetWorkType()
        {
            var work = _workTypeReadRepository.GetAll().ToList();
            return Ok(work);
        }
        [HttpPost("cwt")]
        public async Task<IActionResult> CretaeWorkType(WorkTypeDto createWorktype)
        {
            await _workTypeWriteRepository.AddAsync(new()
            {
               TypeCode=createWorktype.TypeCode,
               TypeName=createWorktype.TypeName,
               Description=createWorktype.Description,
            });
            await _workTypeWriteRepository.SaveAsync();
            
            return StatusCode((int)HttpStatusCode.Created);
        }
        [HttpPost("wc")]
        public async Task<IActionResult> CretaeWorkCategory(WorkCategoryDto createWorkCategory)
        {
            await _workCategoryWriteRepository.AddAsync(new()
            {
                //WorkTypeId=createWorkCategory.WorkTypeId,
                //Code = createWorkCategory.Code,
                //Name = createWorkCategory.Name,
                Description = createWorkCategory.Description,
            });
            await _workTypeWriteRepository.SaveAsync();

            return StatusCode((int)HttpStatusCode.Created);
        }
    }

   
   

}
