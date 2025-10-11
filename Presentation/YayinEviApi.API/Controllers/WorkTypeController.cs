using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using YayinEviApi.Application.Repositories.IHelperEntitiesR.IWorkTypeR;

namespace YayinEviApi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = "Admin")]
    public class WorkTypeController : ControllerBase
    {
        readonly IWorkTypeWriteRepository _workTypeWriteRepository;

        readonly IWorkTypeReadRepository _workTypeReadRepository;
        public WorkTypeController(IWorkTypeWriteRepository workTypeWriteRepository, IWorkTypeReadRepository workTypeReadRepository)
        {
            _workTypeWriteRepository = workTypeWriteRepository;
            _workTypeReadRepository = workTypeReadRepository;
        }
        [HttpGet()]
        public async Task<IActionResult> GetWorkType()
        {
            var work = _workTypeReadRepository.GetAll().ToList();
            return Ok(work);
        }
        [HttpPost()]
        public async Task<IActionResult> CretaeWorkType(CreateWorkType createWorktype)
        {
            await _workTypeWriteRepository.AddAsync(new()
            {
                TypeCode = createWorktype.TypeCode,
                TypeName = createWorktype.TypeName,
                Description = createWorktype.Description,
            });
            await _workTypeWriteRepository.SaveAsync();

            return StatusCode((int)HttpStatusCode.Created);
        }
    }
    public class CreateWorkType
    {
        public string TypeCode { get; set; }
        public string TypeName { get; set; }
        public string Description { get; set; }

    }
                
}
