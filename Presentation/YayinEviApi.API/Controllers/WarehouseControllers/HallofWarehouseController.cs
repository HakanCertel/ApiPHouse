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

    public class HallofWarehouseController : ControllerBase
    {
        private IUserService _userService;
        readonly CreateUser _user;
        readonly IHallofWarehouseRepository _hallofWarehouseRepository;
        Expression<Func<HallofWarehouse, bool>>? _hallofWarehouseExpression;
        public HallofWarehouseController(IHallofWarehouseRepository hallofWarehouseRepository, IUserService userService)
        {
            _hallofWarehouseRepository = hallofWarehouseRepository;
            _userService = userService;

            _user = _userService.GetUser().Result;
        }

        [HttpGet("{id?}")]
        public async Task<IActionResult> GetAll(string? id)
        {
            _hallofWarehouseExpression = id == null ? null : x => x.WarehouseId.ToString() == id;
            var howList = _hallofWarehouseExpression == null ? _hallofWarehouseRepository.Table : _hallofWarehouseRepository.Table.Where(_hallofWarehouseExpression);
            
            var warehouses =howList.Select(x => new HallofWarehouseDto
            {
                Id = x.Id.ToString(),
                Name = x.Name,
                Code = x.Code,
                WarehouseId = x.Warehouse.Id.ToString(),
                WarehouseCode = x.Warehouse.Code,
                WarehouseName = x.Warehouse.Name,
                IsGoodAcceptWarehouse=x.Warehouse.IsGoodsAcceptWareHouse,
                IsShippingWarehouse=x.Warehouse.IsShippingWareHouse,
                IsActive = x.IsActive,
            }).ToList();

            return Ok(warehouses);

        }
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var hallofwarehouse = await _hallofWarehouseRepository.GetByIdAsync(id);
            var newwarehouses = new HallofWarehouseDto
            {
                Id = hallofwarehouse.Id.ToString(),
                Code= hallofwarehouse.Code,
                Name = hallofwarehouse.Name,
                IsActive = hallofwarehouse.IsActive,
                WarehouseId=hallofwarehouse.Warehouse.Id.ToString(),
                WarehouseCode=hallofwarehouse.Warehouse.Code,
                WarehouseName=hallofwarehouse.Warehouse.Name,
                IsShippingWarehouse=hallofwarehouse.Warehouse.IsShippingWareHouse,
                IsGoodAcceptWarehouse=hallofwarehouse.Warehouse.IsGoodsAcceptWareHouse,
            };

            return Ok(newwarehouses);
        }

        [HttpPost()]
        public async Task<IActionResult> Add(HallofWarehouseDto[] hallofwarehouseList)
        {
            List<HallofWarehouse> howList = new List<HallofWarehouse>();

            foreach (var hallofwarehouse in hallofwarehouseList)
            {
                var howItem = new HallofWarehouse
                {
                    Code=hallofwarehouse.Code,
                    Name = hallofwarehouse.Name,
                    IsActive = hallofwarehouse.IsActive,
                    WarehouseId = Guid.Parse(hallofwarehouse.WarehouseId),
                };
                howList.Add(howItem);
            }
            await _hallofWarehouseRepository.AddRangeAsync(howList);
           
            await _hallofWarehouseRepository.SaveAsync();

            return Ok(howList);


            //return StatusCode((int)HttpStatusCode.Created);
        }

        [HttpPut]
        public async Task<IActionResult> Edit(HallofWarehouseDto[] hallofwarehouseList)
        {
            List<HallofWarehouse> howList = new List<HallofWarehouse>();

            foreach (var hallofwarehouse in hallofwarehouseList)
            {
                var howItem = new HallofWarehouse
                {
                    Id=Guid.Parse(hallofwarehouse.Id),
                    Code= hallofwarehouse.Code,
                    Name = hallofwarehouse.Name,
                    IsActive = hallofwarehouse.IsActive,
                    WarehouseId = Guid.Parse(hallofwarehouse.WarehouseId),
                };
                howList.Add(howItem);
            }
            
            _hallofWarehouseRepository.UpdateRange(howList);
           
            //_hallofWarehouseRepository.Update(new()
            //{
            //    Id = Guid.Parse(hallofwarehouse.Id.ToString()),
            //    Name = hallofwarehouse.Name,
            //    IsActive = hallofwarehouse.IsActive,
            //    WarehouseId=Guid.Parse(hallofwarehouse.WarehouseId)
            //});
            await _hallofWarehouseRepository.SaveAsync();

            return Ok(howList);
            //return StatusCode((int)HttpStatusCode.Created);

        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _hallofWarehouseRepository.RemoveAsync(id);
            await _hallofWarehouseRepository.SaveAsync();
            return Ok();
        }
    }
}
