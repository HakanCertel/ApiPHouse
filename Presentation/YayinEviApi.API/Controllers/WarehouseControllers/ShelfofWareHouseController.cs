using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using System.Net;
using YayinEviApi.Application.Abstractions.Services;
using YayinEviApi.Application.DTOs.User;
using YayinEviApi.Application.DTOs.WarehouseDtos;
using YayinEviApi.Application.Repositories.IWarehouseR;
using YayinEviApi.Domain.Entities.WarehouseE;

namespace YayinEviApi.API.Controllers.WarehouseControllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")]

    public class ShelfofWareHouseController : ControllerBase
    {
        private IUserService _userService;
        readonly CreateUser _user;
        readonly IShelfofWarehouseRepository _shelfofWareHouseRepository;
        Expression<Func<ShelfofWarehouse, bool>>? _shelfofWarehouseExpression;

        public ShelfofWareHouseController(IShelfofWarehouseRepository shelfofWareHouseRepository, IUserService userService)
        {
            _shelfofWareHouseRepository = shelfofWareHouseRepository;
            _userService = userService;

            _user = _userService.GetUser().Result;
        }

        [HttpGet("{id?}")]
        public async Task<IActionResult> GetAll(string? id)
        {
            _shelfofWarehouseExpression = id == null ? null : x => x.HallofWarehouseId.ToString() == id;
            var howList = _shelfofWarehouseExpression == null ? _shelfofWareHouseRepository.Table : _shelfofWareHouseRepository.Table.Where(_shelfofWarehouseExpression);

            var soWarehouses = howList.Select(x => new ShelfofWarehouseDto
            {
                Id = x.Id.ToString(),
                Name = x.Name,
                Code = x.Code,
                HallofWarehouseId = x.HallofWarehouseId.ToString(),
                HallofWarehouseCode = x.HallofWarehouse.Code,
                HallofWarehouseName = x.HallofWarehouse.Name,
                WarehouseId = x.HallofWarehouse.Warehouse.Id.ToString(),
                WarehouseCode = x.HallofWarehouse.Warehouse.Code,
                WarehouseName = x.HallofWarehouse.Warehouse.Name,
                IsGoodAcceptWarehouse=x.HallofWarehouse.Warehouse.IsGoodsAcceptWareHouse,
                IsShippingWarehouse=x.HallofWarehouse.Warehouse.IsShippingWareHouse,
                IsActive = x.IsActive,
            }).ToList();

            return Ok(soWarehouses);

        }
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var shelfofwarehouse = await _shelfofWareHouseRepository.GetByIdAsync(id);
            var newcellofwarehouses = new ShelfofWarehouseDto
            {
                Id = shelfofwarehouse.Id.ToString(),
                Code = shelfofwarehouse.Code,
                Name = shelfofwarehouse.Name,
                IsActive = shelfofwarehouse.IsActive,
                HallofWarehouseId = shelfofwarehouse.HallofWarehouse.Id.ToString(),
                HallofWarehouseCode = shelfofwarehouse.HallofWarehouse.Code,
                HallofWarehouseName = shelfofwarehouse.HallofWarehouse.Name,
                WarehouseId = shelfofwarehouse.HallofWarehouse.Warehouse.Id.ToString(),
                WarehouseCode = shelfofwarehouse.HallofWarehouse.Warehouse.Code,
                WarehouseName = shelfofwarehouse.HallofWarehouse.Warehouse.Name,
                IsGoodAcceptWarehouse=shelfofwarehouse.HallofWarehouse.Warehouse.IsGoodsAcceptWareHouse,
                IsShippingWarehouse=shelfofwarehouse.HallofWarehouse.Warehouse.IsShippingWareHouse,
            };

            return Ok(newcellofwarehouses);
        }

        [HttpPost()]
        public async Task<IActionResult> Add(ShelfofWarehouseDto[] shelfofwarehouseList)
        {
            List<ShelfofWarehouse> cowList = new List<ShelfofWarehouse>();

            foreach (var shelfofwarehouse in shelfofwarehouseList)
            {
                var howItem = new ShelfofWarehouse
                {
                    Code = shelfofwarehouse.Code,
                    Name = shelfofwarehouse.Name,
                    IsActive = shelfofwarehouse.IsActive,
                    HallofWarehouseId = Guid.Parse(shelfofwarehouse.HallofWarehouseId),
                };
                cowList.Add(howItem);
            }
            await _shelfofWareHouseRepository.AddRangeAsync(cowList);

            await _shelfofWareHouseRepository.SaveAsync();

            return StatusCode((int)HttpStatusCode.Created);
        }

        [HttpPut]
        public async Task<IActionResult> Edit(ShelfofWarehouseDto[] shelfofwarehouseList)
        {
            List<ShelfofWarehouse> cowList = new List<ShelfofWarehouse>();

            foreach (var shelfofwarehouse in shelfofwarehouseList)
            {
                var cowItem = new ShelfofWarehouse
                {
                    Id = Guid.Parse(shelfofwarehouse.Id),
                    Code = shelfofwarehouse.Code,
                    Name = shelfofwarehouse.Name,
                    IsActive = shelfofwarehouse.IsActive,
                    HallofWarehouseId = Guid.Parse(shelfofwarehouse.HallofWarehouseId),
                };
                cowList.Add(cowItem);
            }

            _shelfofWareHouseRepository.UpdateRange(cowList);

            await _shelfofWareHouseRepository.SaveAsync();

            return StatusCode((int)HttpStatusCode.Created);

        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _shelfofWareHouseRepository.RemoveAsync(id);
            await _shelfofWareHouseRepository.SaveAsync();
            return Ok();
        }

    }
}
