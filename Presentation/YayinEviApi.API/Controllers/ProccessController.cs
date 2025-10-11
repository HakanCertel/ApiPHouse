using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using YayinEviApi.Application.DTOs.AuthorDto;
using YayinEviApi.Application.DTOs.HelperEntityDtos;
using YayinEviApi.Application.Repositories.IProccessR;
using YayinEviApi.Application.RequestParameters;
using YayinEviApi.Domain.Entities.HelperEntities.ProccessE;

namespace YayinEviApi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = "Admin")]
    public class ProccessController : ControllerBase
    {
        readonly IProccessReadRepository _proccessReadRepository;

        readonly IProccessWriteRepository _proccessWriteRepository;
        public ProccessController(IProccessReadRepository proccessReadRepository, IProccessWriteRepository proccessWriteRepository)
        {
            _proccessReadRepository = proccessReadRepository;
            _proccessWriteRepository = proccessWriteRepository;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {

            var proccess = _proccessReadRepository.Select(null, x => new ProccessDto
            {
                ProccessId = x.Id.ToString(),
                ProccessName = x.Name,
                ProccessCode = x.Code,
                ProccessCategoryId=x.ProccessCategoryId.ToString(),
                ProccessCategoryName=x.ProccessCategory.Name,
                CreatedDate = x.CreatedDate,
                UpdatedDate = x.UpdatedDate,
            }).ToList();
               
            return Ok(proccess);

        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAll()
        {

            var proccess = _proccessReadRepository.Select(null, x => new ProccessDto
            {
                ProccessId = x.Id.ToString(),
                ProccessName = x.Name,
                ProccessCode = x.Code,
                ProccessCategoryId=x.ProccessCategoryId.ToString(),
                ProccessCategoryName=x.ProccessCategory.Name,
                CreatedDate = x.CreatedDate,
                UpdatedDate = x.UpdatedDate,
            }).ToList();

            return Ok(proccess);

        }
        [HttpGet("[action]/{id}")]
        //[Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> GetById(string id)
        {
            var newProccess =await _proccessReadRepository.Select(null,x=> new ProccessDto
            {
                ProccessId = x.Id.ToString(),
                ProccessCode = x.Code ?? "",
                ProccessName = x.Name ?? "",
                ProccessCategoryId = x.ProccessCategoryId.ToString(),
                ProccessCategoryName=x.ProccessCategory.Name
            }).FirstOrDefaultAsync(x=>x.ProccessId==id);
            //var newProccess = new ProccessDto
            //{
            //    ProccessId = proccess.Id.ToString(),
            //    ProccessCode = proccess.Code ?? "",
            //    ProccessName = proccess.Name ?? "",
            //    ProccessCategoryId=proccess.ProccessCategoryId.ToString()
            //};

            return Ok(newProccess);
        }
        [HttpPost()]
        public async Task<IActionResult> Add(ProccessDto proccess)
        {
            await _proccessWriteRepository.AddAsync(new()
            {
                Code = proccess.ProccessCode,
                Name = proccess.ProccessName,
                ProccessCategoryId= proccess.ProccessCategoryId!=null?Guid.Parse(proccess.ProccessCategoryId):null,
            });
            await _proccessWriteRepository.SaveAsync();

            return StatusCode((int)HttpStatusCode.Created);
        }

        [HttpPut]
        public async Task<IActionResult> Edit(ProccessDto proccess)
        {
            _proccessWriteRepository.Update(new()
            {
                Id = Guid.Parse(proccess.ProccessId),
                Code = proccess.ProccessCode,
                Name = proccess.ProccessName,
                ProccessCategoryId = proccess.ProccessCategoryId != null ? Guid.Parse(proccess.ProccessCategoryId) : null
            });
            await _proccessWriteRepository.SaveAsync();

            return StatusCode((int)HttpStatusCode.Created);

        }
        [HttpDelete("{Id}")]
        //[Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> Delete(string id)
        {
            await _proccessWriteRepository.RemoveAsync(id);
            await _proccessWriteRepository.SaveAsync();
            return Ok();
        }

    }
}
