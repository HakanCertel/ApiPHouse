using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using YayinEviApi.Application.Abstractions.Services;
using YayinEviApi.Application.DTOs.CurrentDtos;
using YayinEviApi.Application.DTOs.User;
using YayinEviApi.Application.Repositories.ICurrentR;
using YayinEviApi.Application.RequestParameters;
using YayinEviApi.Domain.Entities.CurrentE;
using YayinEviApi.Domain.Enum;
using YayinEviApi.Infrastructure.Enums;
using YayinEviApi.Infrastructure.Operations;

namespace YayinEviApi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")]
    public class CurrentController : ControllerBase
    {
        private IUserService _userService;
        readonly CreateUser _user;
        readonly ICurrentRepository _currentRepository;
        public CurrentController(ICurrentRepository currentRepository, IUserService userService)
        {
            _currentRepository = currentRepository;
            _userService = userService;
            _user = _userService.GetUser().Result;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] Pagination pagination) 
        {
            var totalCount = _currentRepository.GetAll(false).Count();

            var current = _currentRepository.Table.Select( x => new CurrentDto
            {
                Id = x.Id.ToString(),
                Name = x.Name,
                Address = x.Address,
                Code = x.Code,
                Country = x.Country,
                County = x.County,
                Town=x.Town,
                Appellation = x.Appellation,
                Description = x.Description,
                TaxNumber=x.TaxNumber,
                TaxDepartment=x.TaxDepartment,
                LocalOrForeing= x.LocalOrForeing!=null? x.LocalOrForeing.toName():"",
                CurrentState=x.CurrentState.toName(),
                CurrentStatus=x.CurrentStatu.toName(),
                DepartmentofPerson=x.DepartmentofPerson,
                Email=x.Email, 
                MobilePhone=x.MobilePhone,
                PhoneNumber=x.PhoneNumber,
                ResponsiblePerson = x.ResponsiblePerson
            }).Select(x => x).Skip(pagination.Page * pagination.Size).Take(pagination.Size).ToList();
            
            return Ok(new { totalCount, current });
        }

        [HttpGet("[action]/{id}")]
        [Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> GetById(string id)
        {
            var ageny = await _currentRepository.Table.Select(x => new CurrentDto
            {
                Id = x.Id.ToString(),
                Name = x.Name,
                Address = x.Address,
                Code = x.Code,
                Country = x.Country,
                County = x.County,
                Town = x.Town,
                Appellation = x.Appellation,
                Description = x.Description,
                TaxNumber = x.TaxNumber,
                TaxDepartment = x.TaxDepartment,
                LocalOrForeing = x.LocalOrForeing.toName(),
                CurrentState = x.CurrentState.toName(),
                CurrentStatus = x.CurrentStatu.toName(),
                DepartmentofPerson = x.DepartmentofPerson,
                Email = x.Email,
                MobilePhone = x.MobilePhone,
                PhoneNumber = x.PhoneNumber,
                ResponsiblePerson = x.ResponsiblePerson
            }).FirstOrDefaultAsync(x => x.Id == id);

            return Ok(ageny);
        }
        [HttpPost()]
        public async Task<IActionResult> Add(CurrentDto currentDto)
        {
            Current current = new Current
            {
                Name = currentDto.Name,
                Address = currentDto.Address,
                Code = currentDto.Code,
                Country = currentDto.Country,
                County = currentDto.County,
                Town = currentDto.Town,
                Appellation = currentDto.Appellation,
                Description = currentDto.Description,
                TaxNumber = currentDto.TaxNumber,
                TaxDepartment = currentDto.TaxDepartment,
                LocalOrForeing = currentDto.LocalOrForeing.GetEnum<LocalOrForeing>(),
                CurrentState = currentDto.CurrentState.GetEnum<CurrentState>(),
                CurrentStatu = currentDto.CurrentStatus.GetEnum<CurrentStatus>(),
                DepartmentofPerson = currentDto.DepartmentofPerson,
                Email = currentDto.Email,
                MobilePhone = currentDto.MobilePhone,
                PhoneNumber = currentDto.PhoneNumber,
                ResponsiblePerson = currentDto.ResponsiblePerson
            };
            await _currentRepository.AddAsync(current);
            await _currentRepository.SaveAsync();
            return Ok(current);
            //return StatusCode((int)HttpStatusCode.Created);
        }
        [HttpPut]
        public async Task<IActionResult> Edit(CurrentDto currentDto)
        {
            _currentRepository.Update(new()
            {
                Id = Guid.Parse(currentDto.Id),
                Name = currentDto.Name,
                Address = currentDto.Address,
                Code = currentDto.Code,
                Country = currentDto.Country,
                County = currentDto.County,
                Town = currentDto.Town,
                Appellation = currentDto.Appellation,
                Description = currentDto.Description,
                TaxNumber = currentDto.TaxNumber,
                TaxDepartment = currentDto.TaxDepartment,
                LocalOrForeing = currentDto.LocalOrForeing.GetEnum<LocalOrForeing>(),
                CurrentState = currentDto.CurrentState.GetEnum<CurrentState>(),
                CurrentStatu = currentDto.CurrentStatus.GetEnum<CurrentStatus>(),
                DepartmentofPerson = currentDto.DepartmentofPerson,
                Email = currentDto.Email,
                MobilePhone = currentDto.MobilePhone,
                PhoneNumber = currentDto.PhoneNumber,
                ResponsiblePerson = currentDto.ResponsiblePerson

            });
            await _currentRepository.SaveAsync();

            return Ok(currentDto);

        }
        [HttpDelete("{Id}")]
        [Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> Delete(string id)
        {
            await _currentRepository.RemoveAsync(id);
            await _currentRepository.SaveAsync();
            return Ok();
        }

    }
}
