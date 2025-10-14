using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using YayinEviApi.Application.DTOs.HelperEntityDtos;
using YayinEviApi.Application.Repositories.IHelperEntitiesR.IDepartmentR;
using YayinEviApi.Domain.Entities.HelperEntities.ProccessE;

namespace YayinEviApi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")]
    public class DepartmentController : ControllerBase
    {
        readonly IDepartmentReadRepository _departmentReadRepository;
        readonly IDepartmentWriteRepository _departmentWriteRepository;

        public DepartmentController(IDepartmentWriteRepository departmentWriteRepository, IDepartmentReadRepository departmentReadRepository)
        {
            _departmentWriteRepository = departmentWriteRepository;
            _departmentReadRepository = departmentReadRepository;
        }

        [HttpPost()]
        public async Task<IActionResult> Add(DepartmentDto department)
        {
            await _departmentWriteRepository.AddAsync(new()
            {
                Code = department.Code,
                Name = department.Name,
                IsActive=true,

            });
            await _departmentWriteRepository.SaveAsync();

            return StatusCode((int)HttpStatusCode.Created);
        }
        [HttpPut]
        public async Task<IActionResult> Edit(DepartmentDto department)
        {
            _departmentWriteRepository.Update(new()
            {
                Id =Guid.Parse( department.Id.ToString()),
                Code = department.Code,
                Name = department.Name,
                IsActive = department.IsActive,
                CreatedDate =Convert.ToDateTime( department.CreatedDate),
                UpdatedDate =DateTime.Now,
            });
            await _departmentWriteRepository.SaveAsync();

            return StatusCode((int)HttpStatusCode.Created);

        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _departmentWriteRepository.RemoveAsync(id);
            await _departmentWriteRepository.SaveAsync();
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {

            var departments = _departmentReadRepository.Select(null, x => new DepartmentDto
            {
                Id = x.Id.ToString(),
                Name = x.Name,
                Code = x.Code,
                IsActive = x.IsActive,
                CreatedDate = x.CreatedDate,
                UpdatedDate = x.UpdatedDate,
            }).ToList();

            return Ok(departments);

        }
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var department = await _departmentReadRepository.GetByIdAsync(id);
            var newDepartment = new DepartmentDto
            {
                Id = department.Id.ToString(),
                Code = department.Code ?? "",
                Name = department.Name ?? "",
                IsActive = department.IsActive,
            };

            return Ok(newDepartment);
        }
    }
}
