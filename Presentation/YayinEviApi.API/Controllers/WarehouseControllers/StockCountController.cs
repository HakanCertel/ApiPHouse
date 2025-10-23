using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using YayinEviApi.Application.Abstractions.Services;
using YayinEviApi.Application.DTOs.CurrentDtos;
using YayinEviApi.Application.DTOs.User;
using YayinEviApi.Application.DTOs.WarehouseDtos;
using YayinEviApi.Application.Repositories.IWarehouseR;
using YayinEviApi.Domain.Entities.WarehouseE;

namespace YayinEviApi.API.Controllers.WarehouseControllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")]

    public class StockCountController : ControllerBase
    {
        private IUserService _userService;
        readonly CreateUser _user;
        readonly IStockCountRepository _stockCountRepository;
        readonly IStockCountItemsRepository _stockCountItemsRepository;

        public StockCountController(IStockCountRepository stockCountRepository, IUserService userService, IStockCountItemsRepository stockCountItemsRepository)
        {
            _stockCountRepository = stockCountRepository;
            _stockCountItemsRepository = stockCountItemsRepository;
            _userService = userService;
            _user = _userService.GetUser().Result;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var stockCount = _stockCountRepository.Table.Select(x => new
            {
                sCount=x,
                cUserName = _userService.GetUser(x.CreatingUserId).Result.NameSurname,
                uUserName =x.UpdatingUserId!=null? _userService.GetUser(x.UpdatingUserId).Result.NameSurname:null,
            }).Select(x => new StockCountDto
            {
               Id=x.sCount.Id.ToString(),
               Code=x.sCount.Code,
               DocumentCode=x.sCount.DocumentCode,
               DocumenDate=x.sCount.DocumentDate,
               IsConfirmed=x.sCount.IsConfirmed,
               ConfirmDate=x.sCount.ConfirmDate,
               CreatingUserId=x.sCount.CreatingUserId,
                CreatingUserName = x.cUserName,
                UpdatingUserId =x.sCount.UpdatingUserId,
                UpdatingUserName = x.uUserName,
            }).ToList();

            return Ok(stockCount);
        }

        [HttpGet("[action]/{id}")]
        [Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> GetById(string id)
        {
            var sc = await _stockCountRepository.Select(x=>x.Id.ToString()==id, x => new StockCountDto
            {
                Id = x.Id.ToString(),
                Code= x.Code,
                DocumentCode = x.DocumentCode,
                DocumenDate = x.DocumentDate,
                IsConfirmed = x.IsConfirmed,
                ConfirmDate = x.ConfirmDate,
                CreatingUserId = x.CreatingUserId,
                UpdatingUserId = x.UpdatingUserId,
                IsActive=x.IsActive,
            }).FirstOrDefaultAsync();

            return Ok(sc);
        }
        [HttpPost]
        public async Task<IActionResult> Add(StockCountDto stockCount)
        {
            if (_stockCountRepository.Select(x => x.Code == stockCount.Code, x => x).Any())
            {
                stockCount.Code = _stockCountRepository.GetNewCodeAsync(stockCount.Serie, x => x.Code).Result?.ToString();
            }
            StockCount sc = new StockCount
            {
                Code=stockCount.Code,
                DocumentDate=Convert.ToDateTime( stockCount.DocumenDate),
                CreatingUserId=_user.UserId,
                ConfirmDate=stockCount.ConfirmDate,
                CreatedDate=DateTime.Now,
                IsActive=true,
                IsConfirmed=stockCount.IsConfirmed,
            };
            await _stockCountRepository.AddAsync(sc);

            await _stockCountRepository.SaveAsync();
            return Ok(sc);
            //return StatusCode((int)HttpStatusCode.Created);

        }
        [HttpPut]
        public async Task<IActionResult> Edit(StockCountDto sc)
        {
            _stockCountRepository.Update(new()
            {
                Id=Guid.Parse(sc.Id),
                Code=sc.Code,
                DocumentCode=sc.DocumentCode,
                DocumentDate=Convert.ToDateTime(sc.DocumenDate),
                ConfirmDate=sc.ConfirmDate,
                CreatingUserId=sc.CreatingUserId,
                IsActive=sc.IsActive,
                IsConfirmed=sc.IsConfirmed,
                UpdatedDate=DateTime.Now,
                UpdatingUserId=_user.UserId,
            });
            await _stockCountRepository.SaveAsync();

            //return StatusCode((int)HttpStatusCode.Created);
            return Ok(sc);

        }

        [HttpDelete("{Id}")]
        [Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> Delete(string id)
        {
            var countItems=_stockCountItemsRepository.GetWhere(x=>x.StockCountId.ToString()==id);
            foreach (var item in countItems)
            {
                _stockCountItemsRepository.Remove(item);
            }
           
            await _stockCountItemsRepository.SaveAsync();
            await _stockCountRepository.RemoveAsync(id);
            await _stockCountRepository.SaveAsync();
            return Ok();
        }

        [HttpGet("[action]/{id?}")]
        public async Task<IActionResult> GetAllByParentId(string? id)
        {
            var stockCountItems = _stockCountItemsRepository.Table.Where(x => x.StockCountId.ToString() == id).Select(x => new
            {
                sCount = x.StockCount,
                sCountItem = x,
                material=x.Material,
                cell = x.CellofWarehouse,
                shelf = x.CellofWarehouse.ShelfofWarehouse,
                hall = x.CellofWarehouse.ShelfofWarehouse.HallofWarehouse,
                warehouse = x.CellofWarehouse.ShelfofWarehouse.HallofWarehouse.Warehouse,
                cUserName = _userService.GetUser(x.StockCount.CreatingUserId).Result.NameSurname,
                uUserName = x.StockCount.UpdatingUserId != null ? _userService.GetUser(x.StockCount.UpdatingUserId).Result.NameSurname:null,
            }).Select(x => new StockCountItemsDto
            {
                Id = x.sCountItem.Id.ToString(),
                StockCounttId=x.sCountItem.StockCountId.ToString(),
                StockCountCode=x.sCount.DocumentCode,
                DocumentDate=x.sCount.DocumentDate,
                Quantity=x.sCountItem.Quantity,
                UnitId=x.material.UnitId.ToString(),
                UnitCode=x.material.Unit.Code,
                UnitName=x.material.Unit.Name,
                CellofWarehouseId=x.sCountItem.CellofWarehouseId.ToString(),
                CellofWarehouseCode=x.sCountItem.CellofWarehouse.Code,
                CellofWarehouseName=x.sCountItem.CellofWarehouse.Name,
                ShelfofWarehouseId=x.shelf.Id.ToString(),
                ShelfofWarehouseCode=x.shelf.Code,
                ShelfofWarehouseName=x.shelf.Name,
                HallofWarehouseId=x.hall.Id.ToString(),
                HallofWarehouseCode=x.hall.Code,
                HallofWarehouseName=x.hall.Name,
                WarehouseId=x.warehouse.Id.ToString(),
                WarehouseCode=x.warehouse.Code,
                WarehouseName=x.warehouse.Name,
                MaterialId=x.sCountItem.MaterialId.ToString(),
                MaterialCode=x.sCountItem.Material.Code,
                MaterialName=x.sCountItem.Material.Name,
                CreatingUserId = x.sCount.CreatingUserId,
                CreatingUserName = x.cUserName,
                UpdatingUserId = x.sCount.UpdatingUserId,
                UpdatingUserName = x.uUserName,
            }).ToList();

            return Ok(stockCountItems);
        }
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetItemById(string id)
        {
            var countItem = await _stockCountItemsRepository.GetByIdAsync(id);
            var newCountItem = new StockCountItemsDto
            {
                Id = countItem.Id.ToString(),
                StockCounttId = countItem.StockCountId.ToString(),
                Quantity = countItem.Quantity,
                UnitId = countItem.UnitId.ToString(),
                UnitCode = countItem.Unit.Code,
                UnitName = countItem.Unit.Name,
                CellofWarehouseId = countItem.CellofWarehouseId.ToString(),
                CellofWarehouseCode = countItem.CellofWarehouse.Code,
                CellofWarehouseName = countItem.CellofWarehouse.Name,
                MaterialId = countItem.MaterialId.ToString(),
                MaterialCode = countItem.Material.Code,
                MaterialName = countItem.Material.Name,

            };

            return Ok(newCountItem);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddItems(StockCountItemsDto[] items)
        {
            List<StockCountItems> countItems = new List<StockCountItems>();

            foreach (var item in items)
            {
                var countItem = new StockCountItems
                {
                    MaterialId = Guid.Parse(item.MaterialId),
                    //UnitId =item.UnitId!=null? Guid.Parse(item.UnitId):null,
                    Quantity=Convert.ToDecimal( item.Quantity),
                    StockCountId=Guid.Parse(item.StockCounttId),
                    CellofWarehouseId=Guid.Parse(item.CellofWarehouseId),
                };
                countItems.Add(countItem);
            }
            await _stockCountItemsRepository.AddRangeAsync(countItems);

            await _stockCountItemsRepository.SaveAsync();

            return StatusCode((int)HttpStatusCode.Created);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> EditItems(StockCountItemsDto[] items)
        {
            List<StockCountItems> countItems = new List<StockCountItems>();

            foreach (var item in items)
            {
                var countItem = new StockCountItems
                {
                    Id= Guid.Parse(item.Id),
                    MaterialId = Guid.Parse(item.MaterialId),
                    //UnitId = item.UnitId != null ? Guid.Parse(item.UnitId) : null,
                    Quantity = Convert.ToDecimal(item.Quantity),
                    StockCountId = Guid.Parse(item.StockCounttId),
                    CellofWarehouseId = Guid.Parse(item.CellofWarehouseId),
                    UpdatedDate = DateTime.Now,
                };
                countItems.Add(countItem);
            }
             _stockCountItemsRepository.UpdateRange(countItems);

            await _stockCountItemsRepository.SaveAsync();

            //return StatusCode((int)HttpStatusCode.Created);
            return Ok(items);
        }
        [HttpDelete("[action]/{Id}")]
        [Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> DeleteItem(string id)
        {
            await _stockCountItemsRepository.RemoveAsync(id);
            await _stockCountItemsRepository.SaveAsync();
            return Ok();
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetNewCode(string serie = "CNT")
        {
            var newCode = await _stockCountRepository.GetNewCodeAsync(serie, x => x.Code);

            return Ok(new { newCode });
        }
    }
}
