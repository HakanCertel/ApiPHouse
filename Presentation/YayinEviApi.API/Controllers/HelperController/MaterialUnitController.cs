using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using YayinEviApi.Application.Abstractions.Services;
using YayinEviApi.Application.DTOs.UnitDtos;
using YayinEviApi.Application.DTOs.User;
using YayinEviApi.Application.Repositories.IUnitR;
using YayinEviApi.Domain.Entities.UnitE;

namespace YayinEviApi.API.Controllers.HelperController
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")]

    public class MaterialUnitController : ControllerBase
    {
        private IUserService _userService;
        readonly CreateUser _user;
        readonly IUnitRpository _unitRpository;

        public MaterialUnitController(IUnitRpository unitRpository, IUserService userService)
        {
            _unitRpository = unitRpository;
            _userService = userService;

            _user = _userService.GetUser().Result;
        }
        [HttpGet()]
        public async Task<IActionResult> GetAll()
        {
            var unit = _unitRpository.Table.Select(x=>new UnitDto
            {
                Id=x.Id.ToString(),
                Code=x.Code,
                Name=x.Name,
                IsActive=x.IsActive
            });
            return Ok(unit);
        }
        [HttpPost()]
        public async Task<IActionResult> Add(UnitDto[] unitList)
        {
            List<MaterialUnit> uList = new List<MaterialUnit>();

            foreach (var unit in unitList)
            {
                var materialUnit = new MaterialUnit
                {
                    Code = unit.Code,
                    Name = unit.Name,
                    IsActive = unit.IsActive,
                };
                uList.Add(materialUnit);
            }
            await _unitRpository.AddRangeAsync(uList);

            await _unitRpository.SaveAsync();

            return StatusCode((int)HttpStatusCode.Created);
        }
        [HttpPut]
        public async Task<IActionResult> Edit(MaterialUnit[] unitList)
        {
            List<MaterialUnit> uList = new List<MaterialUnit>();


            foreach (var unit in unitList)
            {
                var materialUnit = new MaterialUnit
                {
                    Id = unit.Id,
                    Code = unit.Code,
                    Name = unit.Name,
                    IsActive = unit.IsActive,
                };
                uList.Add(materialUnit);
            }

            _unitRpository.UpdateRange(uList);

            await _unitRpository.SaveAsync();

            return StatusCode((int)HttpStatusCode.Created);

        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _unitRpository.RemoveAsync(id);
            await _unitRpository.SaveAsync();
            return Ok();
        }
    }
}
