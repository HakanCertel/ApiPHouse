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

    public class CellofWarehouseController : ControllerBase
    {
        private IUserService _userService;
        readonly CreateUser _user;
        readonly ICellofWareHouseRepository _cellofWareHouseRepository;
        Expression<Func<CellofWarehouse, bool>>? _cellofWarehouseExpression;

        public CellofWarehouseController(ICellofWareHouseRepository cellofWareHouseRepository, IUserService userService)
        {
            _cellofWareHouseRepository = cellofWareHouseRepository;
            _userService = userService;

            _user = _userService.GetUser().Result;
        }

        [HttpGet("{id?}")]
        public async Task<IActionResult> GetAll(string? id)
        {
            _cellofWarehouseExpression = id == null ? null : x => x.ShelfofWarehouseId.ToString() == id;
            var howList = _cellofWarehouseExpression == null ? _cellofWareHouseRepository.Table : _cellofWareHouseRepository.Table.Where(_cellofWarehouseExpression);

            var warehouses = howList.Select(x => new CellofWarehouseDto
            {
                Id = x.Id.ToString(),
                Name = x.Name,
                Code = x.Code,
                ShelfofWarehouseId=x.ShelfofWarehouseId.ToString(),
                ShelfofWarehouseCode=x.ShelfofWarehouse.Code,
                ShelfofWarehouseName=x.ShelfofWarehouse.Name,
                HallofWarehouseId=x.ShelfofWarehouse.HallofWarehouse.Id.ToString(),
                HallofWarehouseCode=x.ShelfofWarehouse.HallofWarehouse.Code,
                HallofWarehouseName = x.ShelfofWarehouse.HallofWarehouse.Name,
                WarehouseId = x.ShelfofWarehouse.HallofWarehouse.Warehouse.Id.ToString(),
                WarehouseCode = x.ShelfofWarehouse.HallofWarehouse.Warehouse.Code,
                WarehouseName = x.ShelfofWarehouse.HallofWarehouse.Warehouse.Name,
                IsShippingWarehouse=x.ShelfofWarehouse.HallofWarehouse.Warehouse.IsShippingWareHouse,
                IsDefaultCell=x.IsDefaultCell,
                IsActive = x.IsActive,
            }).ToList();

            return Ok(warehouses);

        }
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var cellofwarehouse = await _cellofWareHouseRepository.GetByIdAsync(id);
            var newcellofwarehouses = new CellofWarehouseDto
            {
                Id = cellofwarehouse.Id.ToString(),
                Code = cellofwarehouse.Code,
                Name = cellofwarehouse.Name,
                IsActive = cellofwarehouse.IsActive,
                ShelfofWarehouseId = cellofwarehouse.ShelfofWarehouse.Id.ToString(),
                ShelfofWarehouseCode=cellofwarehouse.ShelfofWarehouse.Code,
                ShelfofWarehouseName = cellofwarehouse.ShelfofWarehouse.Name,
                WarehouseId = cellofwarehouse.ShelfofWarehouse.HallofWarehouse.Warehouse.Id.ToString(),
                WarehouseCode = cellofwarehouse.ShelfofWarehouse.HallofWarehouse.Warehouse.Code,
                WarehouseName = cellofwarehouse.ShelfofWarehouse.HallofWarehouse.Warehouse.Name,
                IsDefaultCell=cellofwarehouse.IsDefaultCell,
            };

            return Ok(newcellofwarehouses);
        }

        [HttpPost()]
        public async Task<IActionResult> Add(CellofWarehouseDto[] cellofwarehouseList)
        {
            List<CellofWarehouse> cowList = new List<CellofWarehouse>();

            foreach (var cellofwarehouse in cellofwarehouseList)
            {
                var howItem = new CellofWarehouse
                {
                    Code = cellofwarehouse.Code,
                    Name = cellofwarehouse.Name,
                    IsActive = cellofwarehouse.IsActive,
                    ShelfofWarehouseId = Guid.Parse(cellofwarehouse.HallofWarehouseId),
                    IsDefaultCell=cellofwarehouse.IsDefaultCell
                };
                cowList.Add(howItem);
            }
            await _cellofWareHouseRepository.AddRangeAsync(cowList);

            await _cellofWareHouseRepository.SaveAsync();

            return StatusCode((int)HttpStatusCode.Created);
        }

        [HttpPut]
        public async Task<IActionResult> Edit(CellofWarehouseDto[] cellofwarehouseList)
        {
            List<CellofWarehouse> cowList = new List<CellofWarehouse>();

            foreach (var cellofwarehouse in cellofwarehouseList)
            {
                var cowItem = new CellofWarehouse
                {
                    Id = Guid.Parse(cellofwarehouse.Id),
                    Code = cellofwarehouse.Code,
                    Name = cellofwarehouse.Name,
                    IsActive = cellofwarehouse.IsActive,
                    ShelfofWarehouseId = Guid.Parse(cellofwarehouse.ShelfofWarehouseId),
                    IsDefaultCell=cellofwarehouse.IsDefaultCell,
                };
                cowList.Add(cowItem);
            }

            _cellofWareHouseRepository.UpdateRange(cowList);

            await _cellofWareHouseRepository.SaveAsync();

            return Ok(cellofwarehouseList);

        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _cellofWareHouseRepository.RemoveAsync(id);
            await _cellofWareHouseRepository.SaveAsync();
            return Ok();
        }
    }
}
