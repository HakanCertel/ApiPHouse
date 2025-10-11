using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using YayinEviApi.Application.Abstractions.Services;
using YayinEviApi.Application.DTOs.User;
using YayinEviApi.Application.DTOs.WarehouseDtos;
using YayinEviApi.Application.Repositories.IWarehouseR;

namespace YayinEviApi.API.Controllers.WarehouseControllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")]

    public class WarehouseController : ControllerBase
    {
        private readonly IWarehouseRepository? _warehouseRepository;
        private IUserService _userService;
        readonly CreateUser _user;
        public WarehouseController(IWarehouseRepository? warehouseRepository, IUserService userService)
        {
            _warehouseRepository = warehouseRepository;
            _userService = userService;

            _user = _userService.GetUser().Result;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {

            var warehouses = _warehouseRepository.Select(null, x => new Warehousedto
            {
                Id = x.Id.ToString(),
                Name = x.Name,
                Code = x.Code,
                IsActive=x.IsActive,
                IsShippingWareHouse=x.IsShippingWareHouse,
                IsGoodsAcceptWareHouse=x.IsGoodsAcceptWareHouse,
            }).ToList();

            return Ok(warehouses);

        }
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var warehouse = await _warehouseRepository.GetByIdAsync(id);
            var newwarehouses = new Warehousedto
            {
                Id = warehouse.Id.ToString(),
                Code = warehouse.Code ?? "",
                Name = warehouse.Name,
                IsActive=warehouse.IsActive,
                IsShippingWareHouse=warehouse.IsShippingWareHouse,
                IsGoodsAcceptWareHouse=warehouse.IsGoodsAcceptWareHouse,
            };

            return Ok(newwarehouses);
        }

        [HttpPost()]
        public async Task<IActionResult> Add(Warehousedto warehouse)
        {
            await _warehouseRepository.AddAsync(new()
            {
                Code = warehouse.Code,
                Name = warehouse.Name,
                IsActive=warehouse.IsActive,
                IsShippingWareHouse=warehouse.IsShippingWareHouse,
                IsGoodsAcceptWareHouse=warehouse.IsGoodsAcceptWareHouse,
            });
            await _warehouseRepository.SaveAsync();

            return StatusCode((int)HttpStatusCode.Created);
        }

        [HttpPut]
        public async Task<IActionResult> Edit(Warehousedto warehouse)
        {
            _warehouseRepository.Update(new()
            {
                Id = Guid.Parse(warehouse.Id.ToString()),
                Code = warehouse.Code,
                Name = warehouse.Name,
                IsActive = warehouse.IsActive,
                IsShippingWareHouse=warehouse.IsShippingWareHouse,
                IsGoodsAcceptWareHouse =warehouse.IsGoodsAcceptWareHouse,
                // CreatedDate=(DateTime)department.CreatedDate,
                //UpdatedDate=(DateTime)department.UpdatedDate,
            });
            await _warehouseRepository.SaveAsync();

            return StatusCode((int)HttpStatusCode.Created);

        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _warehouseRepository.RemoveAsync(id);
            await _warehouseRepository.SaveAsync();
            return Ok();
        }
    }
}
