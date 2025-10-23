using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using YayinEviApi.Application.Abstractions.Services;
using YayinEviApi.Application.DTOs.MaterialDtos;
using YayinEviApi.Application.DTOs.ShipDtos;
using YayinEviApi.Application.DTOs.User;
using YayinEviApi.Application.Repositories.IShipR;
using YayinEviApi.Application.RequestParameters;
using YayinEviApi.Domain.Entities.ShipE;

namespace YayinEviApi.API.Controllers.ShipControllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")]

    public class ShipController : ControllerBase
    {
        private IUserService _userService;
        readonly CreateUser _user;
        readonly IShipRepository _shipRepository;
        readonly IShipItemRepository _shipItemRepository;

        public ShipController(IUserService userService, IShipRepository shipRepository, IShipItemRepository shipItemRepository)
        {
            _userService = userService;
            _user = _userService.GetUser().Result;
            _shipRepository = shipRepository;
            _shipItemRepository = shipItemRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] Pagination pagination)
        {
            var totalCount = _shipRepository.GetAll(false).Count();
            
            var ships = _shipRepository.Table.Select(x => new
            {
                ship=x,
                cUserName = _userService.GetUser(x.CreatingUserId).Result.NameSurname,
                uUserName =x.UpdatingUserId!=null? _userService.GetUser(x.UpdatingUserId).Result.NameSurname:null,
            }).Select(x => new ShipDto
            {
                Id=x.ship.Id.ToString(),
                DocumentCode=x.ship.DocumentCode,
                Code=x.ship.Code,
                DocumentDate=x.ship.DocumentDate,
                CreatingUserId=x.ship.CreatingUserId,
                CreatingUserName = x.cUserName,
                UpdatingUserId =x.ship.UpdatingUserId,
                UpdatingUserName = x.uUserName,
                ShipCellofWarehouseId=x.ship.ShipCellofWarehouseId.ToString(),
                ShipCellofWarehouseName=x.ship.ShipCellofWarehouse.Name,
                WarehouseName=x.ship.ShipCellofWarehouse.ShelfofWarehouse.HallofWarehouse.Warehouse.Name,
                Address=x.ship.Address,
                Country=x.ship.Country,
                County=x.ship.County,
                CurrentId = x.ship.CurrentId.ToString(),
                CurrentCode=x.ship.Current.Code,
                CurrentName = x.ship.Current.Name,
                CUrrentAddress=x.ship.Current.Address,
                Town = x.ship.Town,
                
            }).ToList();
            var entities=ships.Select(x => x).Skip(pagination.Page * pagination.Size).Take(pagination.Size);

            return Ok(new { totalCount,entities });
        }

        [HttpGet("[action]/{id}")]
        [Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> GetById(string id)
        {
            var sc = await _shipRepository.Select(x=>x.Id.ToString()==id, x => new
            {
                ship = x,
                //warehouse=x.ShipCellofWarehouse.ShelfofWarehouse.HallofWarehouse.Warehouse,
                current=x.Current,
                cUserName = _userService.GetUser(x.CreatingUserId).Result.NameSurname,
                uUserName = x.UpdatingUserId != null ? _userService.GetUser(x.UpdatingUserId).Result.NameSurname : null,
            }).Select(x => new ShipDto
            {
                Id = x.ship.Id.ToString(),
                Code=x.ship.Code,
                DocumentCode = x.ship.DocumentCode,
                DocumentDate = x.ship.DocumentDate,
                CreatingUserId = x.ship.CreatingUserId,
                CreatingUserName = x.cUserName,
                UpdatingUserId = x.ship.UpdatingUserId,
                UpdatingUserName = x.uUserName,
                ShipCellofWarehouseId = x.ship.ShipCellofWarehouseId.ToString(),
                ShipCellofWarehouseName = x.ship.ShipCellofWarehouse.Name,
                WarehouseName=x.ship.ShipCellofWarehouse.ShelfofWarehouse.HallofWarehouse.Warehouse.Name,
                WarehouseId=x.ship.ShipCellofWarehouse.ShelfofWarehouse.HallofWarehouse.Warehouse.Id.ToString(),
                CurrentId = x.ship.CurrentId.ToString(),
                CurrentCode=x.current.Code,
                CurrentName=x.current.Name,
                Address = x.current.Address,
                County = x.current.County,
                Country = x.current.Country,
                Town = x.current.Town,
                CreatedDate= x.ship.CreatedDate

            }).FirstOrDefaultAsync();

            return Ok(sc);
        }
        [HttpPost]
        public async Task<IActionResult> Add(ShipDto ship)
        {
            if (_shipRepository.Select(x => x.Code == ship.Code, x => x).Any())
            {
                ship.Code = _shipRepository.GetNewCodeAsync(ship.Serie, x => x.Code).Result?.ToString();
            }
            Ship ga = new Ship
            {
                Code=ship.Code,
                DocumentCode= ship.DocumentCode,
                DocumentDate=Convert.ToDateTime(ship.DocumentDate),
                CreatingUserId=_user.UserId,
                CreatedDate=DateTime.Now,
                IsActive=true,
                ShipCellofWarehouseId=Guid.Parse(ship.ShipCellofWarehouseId),
                CurrentId=Guid.Parse(ship.CurrentId),
            };
            await _shipRepository.AddAsync(ga);

            await _shipRepository.SaveAsync();
            ship.Id = ga.Id.ToString();
            return Ok(ship);
            //return StatusCode((int)HttpStatusCode.Created);

        }
        [HttpPut]
        public async Task<IActionResult> Edit(ShipDto ship)
        {
            _shipRepository.Update(new()
            {
                Id=Guid.Parse(ship.Id),
                Code=ship.Code,
                DocumentCode= ship.DocumentCode,
                DocumentDate=Convert.ToDateTime(ship.DocumentDate),
                CreatingUserId= ship.CreatingUserId,
                UpdatedDate=DateTime.Now,
                UpdatingUserId=_user.UserId,
                ShipCellofWarehouseId =Guid.Parse(ship.ShipCellofWarehouseId),
                CurrentId=Guid.Parse(ship.CurrentId),
                CreatedDate= Convert.ToDateTime(ship.CreatedDate)
            });
            await _shipRepository.SaveAsync();

            //return StatusCode((int)HttpStatusCode.Created);
            return Ok(ship);

        }

        [HttpDelete("{Id}")]
        [Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> Delete(string id)
        {
            var countItems=_shipItemRepository.GetWhere(x=>x.ParentId.ToString()==id);
            foreach (var item in countItems)
            {
                 _shipItemRepository.Remove(item);
            }

            await _shipItemRepository.SaveAsync();
            await _shipRepository.RemoveAsync(id);
            await _shipRepository.SaveAsync();

            return Ok();
        }

        [HttpGet("[action]/{id?}")]
        public async Task<IActionResult> GetAllByParentId(string? id)
        {
            var stockCountItems = _shipItemRepository.Table.Where(x => x.ParentId.ToString() == id).Select(x => new
            {
                items = x,
                material=x.Material,
                parent = x.Parent,
                cell = x.Parent.ShipCellofWarehouse,
                cUserName = _userService.GetUser(x.Parent.CreatingUserId).Result.NameSurname,
                uUserName = x.Parent.UpdatingUserId != null ? _userService.GetUser(x.Parent.UpdatingUserId).Result.NameSurname:null,
            }).Select(x => new ShipItemDto
            {
                Id = x.items.Id.ToString(),
                ParentId=x.items.ParentId.ToString(),
                ParentCode=x.parent.DocumentCode,
                DocumentDate=x.parent.DocumentDate,
                Quantity=x.items.Quantity,
                UnitId=x.material.UnitId.ToString(),
                UnitCode=x.material.Unit.Code,
                UnitName=x.material.Unit.Name,
                OutgoingCellId=x.cell.Id.ToString(),
                OutgoingCellName=x.cell.Name,
                MaterialId=x.items.MaterialId.ToString(),
                MaterialCode=x.items.Material.Code,
                MaterialName=x.items.Material.Name,
                CreatingUserId = x.parent.CreatingUserId,
                CreatingUserName = x.cUserName,
                UpdatingUserId = x.parent.UpdatingUserId,
                UpdatingUserName = x.uUserName,
                
            }).ToList();

            return Ok(stockCountItems);
        }
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetItemById(string id)
        {
            var countItem = await _shipItemRepository.GetByIdAsync(id);
            var newCountItem = new ShipItemDto
            {
                Id = countItem.Id.ToString(),
                ParentId = countItem.ParentId.ToString(),
                Quantity = countItem.Quantity,
                UnitId = countItem.UnitId.ToString(),
                UnitCode = countItem.Unit.Code,
                UnitName = countItem.Unit.Name,
                MaterialId = countItem.MaterialId.ToString(),
                MaterialCode = countItem.Material.Code,
                MaterialName = countItem.Material.Name,

            };

            return Ok(newCountItem);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddItems(ShipItemDto[] items)
        {
            List<ShipItem> countItems = new List<ShipItem>();

            foreach (var item in items)
            {
                var countItem = new ShipItem
                {
                    MaterialId = Guid.Parse(item.MaterialId),
                    UnitId =item.UnitId!=null? Guid.Parse(item.UnitId):null,
                    Quantity=Convert.ToDecimal( item.Quantity),
                    ParentId=Guid.Parse(item.ParentId),
                };
                countItems.Add(countItem);
            }
            await _shipItemRepository.AddRangeAsync(countItems);

            await _shipItemRepository.SaveAsync();

            return StatusCode((int)HttpStatusCode.Created);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> EditItems(ShipItemDto[] items)
        {
            List<ShipItem> countItems = new List<ShipItem>();

            foreach (var item in items)
            {
                var countItem = new ShipItem
                {
                    Id= Guid.Parse(item.Id),
                    MaterialId = Guid.Parse(item.MaterialId),
                    Quantity = Convert.ToDecimal(item.Quantity),
                    ParentId = Guid.Parse(item.ParentId),
                    UpdatedDate = DateTime.Now,
                };
                countItems.Add(countItem);
            }
             _shipItemRepository.UpdateRange(countItems);

            await _shipItemRepository.SaveAsync();

            return Ok(items);
        }
        [HttpDelete("[action]/{Id}")]
        [Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> DeleteItem(string id)
        {
            await _shipItemRepository.RemoveAsync(id);
            await _shipItemRepository.SaveAsync();
            return Ok();
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetNewCode(string serie = "IRS")
        {
            var newCode = await _shipRepository.GetNewCodeAsync(serie, x => x.Code);

            return Ok(new { newCode });
        }
    }
}
