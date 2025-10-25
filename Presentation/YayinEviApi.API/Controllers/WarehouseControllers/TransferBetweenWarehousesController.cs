using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using YayinEviApi.Application.Abstractions.Services;
using YayinEviApi.Application.DTOs.GoodsAcceptDtos;
using YayinEviApi.Application.DTOs.User;
using YayinEviApi.Application.DTOs.WarehouseDtos;
using YayinEviApi.Application.Repositories.IWarehouseR;
using YayinEviApi.Application.RequestParameters;
using YayinEviApi.Domain.Entities.GoodsAccepE;
using YayinEviApi.Domain.Entities.GoodsAcceptE;
using YayinEviApi.Domain.Entities.WarehouseE;
using YayinEviApi.Persistence.Repositories.WarehouseR;

namespace YayinEviApi.API.Controllers.WarehouseControllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")]

    public class TransferBetweenWarehousesController : ControllerBase
    {
        private IUserService _userService;
        readonly CreateUser _user;
        readonly ITransferDemandBetweenWarehousesRepository _transferDemandBetweenWarehousesRepository;
        readonly ITransferDemandItemBetweenWarehousesRepository _transferDemandItemBetweenWarehousesRepository;

        public TransferBetweenWarehousesController(ITransferDemandItemBetweenWarehousesRepository transferDemandItemBetweenWarehousesRepository, ITransferDemandBetweenWarehousesRepository transferDemandBetweenWarehousesRepository, IUserService userService)
        {
            _transferDemandItemBetweenWarehousesRepository = transferDemandItemBetweenWarehousesRepository;
            _transferDemandBetweenWarehousesRepository = transferDemandBetweenWarehousesRepository;
            _userService = userService;
            _user = _userService.GetUser().Result;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] Pagination pagination)
        {
            var totalCount = _transferDemandBetweenWarehousesRepository.GetAll(false).Count();

            var transfers = _transferDemandBetweenWarehousesRepository.Table.Select(x => new
            {
                tdbw = x,
                cUserName = _userService.GetUser(x.CreatingUserId).Result.NameSurname,
                uUserName = x.UpdatingUserId != null ? _userService.GetUser(x.UpdatingUserId).Result.NameSurname : null,
            }).Select(x => new TransferDemandBetweenWarehousesDto
            {
                Id = x.tdbw.Id.ToString(),
                DocumentCode = x.tdbw.DocumentCode,
                Code= x.tdbw.Code,
                DocumentDate = x.tdbw.DocumentDate,
                IsConfirmed = x.tdbw.IsConfirmed,
                ConfirmedDate = x.tdbw.ConfirmedDate,
                TransferedCellofWarehouseId= x.tdbw.TransferedCellofWarehouseId.ToString(),
                TransferedCellofWarehouseName = x.tdbw.TransferedCellofWarehouse.Name,
                TransferingCellofWarehouseId = x.tdbw.TransferingCellofWarehouseId.ToString(),
                TransferingCellofWarehouseName= x.tdbw.TransferingCellofWarehouse.Name,
                CreatingUserId = x.tdbw.CreatingUserId,
                CreatingUserName = x.cUserName,
                UpdatingUserId = x.tdbw.UpdatingUserId,
                UpdatingUserName = x.uUserName,
                CreatedDate= x.tdbw.CreatedDate,
                DemandedDate= x.tdbw.DemandedDate,
                Description= x.tdbw.Description,
                IsActive= x.tdbw.IsActive,
                TransferedWarehouseName=x.tdbw.TransferedCellofWarehouse.ShelfofWarehouse.HallofWarehouse.Warehouse.Name,
                TransferingWarehouseName=x.tdbw.TransferingCellofWarehouse.ShelfofWarehouse.HallofWarehouse.Warehouse.Name,
                
            }).ToList();

            var entities = transfers.Select(x => x).Skip(pagination.Page * pagination.Size).Take(pagination.Size);

            return Ok(new { totalCount, entities });
        }

        [HttpGet("[action]/{id}")]
        [Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> GetById(string id)
        {
            var sc = await _transferDemandBetweenWarehousesRepository.Select(x => x.Id.ToString() == id, x => new TransferDemandBetweenWarehousesDto
            {
                Id = x.Id.ToString(),
                DocumentCode = x.DocumentCode,
                Code=x.Code,
                DocumentDate = x.DocumentDate,
                IsConfirmed = x.IsConfirmed,
                ConfirmedDate = x.ConfirmedDate,
                CreatingUserId = x.CreatingUserId,
                UpdatingUserId = x.UpdatingUserId,
                IsActive = x.IsActive,
                TransferedCellofWarehouseId=x.TransferedCellofWarehouseId.ToString(),
                TransferedCellofWarehouseName=x.TransferedCellofWarehouse.Name,
                TransferedWarehouseName=x.TransferedCellofWarehouse.ShelfofWarehouse.HallofWarehouse.Warehouse.Name,
                TransferingCellofWarehouseId=x.TransferingCellofWarehouseId.ToString(),
                TransferingCellofWarehouseName=x.TransferingCellofWarehouse.Name,
                TransferingWarehouseName=x.TransferingCellofWarehouse.ShelfofWarehouse.HallofWarehouse.Warehouse.Name,
                CreatedDate = x.CreatedDate,
                DemandedDate = x.DemandedDate,
                Description = x.Description
            }).FirstOrDefaultAsync();

            return Ok(sc);
        }

        [HttpPost]
        public async Task<IActionResult> Add(TransferDemandBetweenWarehousesDto transferDto)
        {
            TransferDemandBetweenWarehouses sc = new TransferDemandBetweenWarehouses
            {
                DocumentCode = transferDto.DocumentCode,
                Code=transferDto.Code,
                DocumentDate = Convert.ToDateTime(transferDto.DocumentDate),
                CreatingUserId = _user.UserId,
                ConfirmedDate = transferDto.ConfirmedDate,
                CreatedDate = DateTime.Now,
                IsActive = true,
                IsConfirmed = transferDto.IsConfirmed,
                TransferedCellofWarehouseId=Guid.Parse(transferDto.TransferedCellofWarehouseId),
                TransferingCellofWarehouseId=Guid.Parse(transferDto.TransferingCellofWarehouseId),
                DemandedDate=transferDto.DemandedDate,
                Description=transferDto.Description,
                
            };
            await _transferDemandBetweenWarehousesRepository.AddAsync(sc);

            await _transferDemandBetweenWarehousesRepository.SaveAsync();
            transferDto.Id=sc.Id.ToString();
            transferDto.CreatingUserId=_user.UserId;
            return Ok(transferDto);
            //return StatusCode((int)HttpStatusCode.Created);

        }

        [HttpPut]
        public async Task<IActionResult> Edit(TransferDemandBetweenWarehousesDto sc)
        {
            _transferDemandBetweenWarehousesRepository.Update(new()
            {
                Id = Guid.Parse(sc.Id),
                Code=sc.Code,
                DocumentCode = sc.DocumentCode,
                DocumentDate = Convert.ToDateTime(sc.DocumentDate),
                ConfirmedDate = sc.ConfirmedDate,
                CreatingUserId = sc.CreatingUserId,
                IsActive = sc.IsActive,
                IsConfirmed = sc.IsConfirmed,
                UpdatedDate = DateTime.Now,
                UpdatingUserId = _user.UserId,
                DemandedDate = sc.DemandedDate,
                Description = sc.Description,
                TransferedCellofWarehouseId=Guid.Parse(sc.TransferedCellofWarehouseId),
                TransferingCellofWarehouseId=Guid.Parse(sc.TransferingCellofWarehouseId),
                
            });
            await _transferDemandBetweenWarehousesRepository.SaveAsync();

            //return StatusCode((int)HttpStatusCode.Created);
            return Ok(sc);

        }

        [HttpDelete("{Id}")]
        [Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> Delete(string id)
        {
            var countItems = _transferDemandItemBetweenWarehousesRepository.GetWhere(x => x.ParentId.ToString() == id);
            foreach (var item in countItems)
            {   
                if(!item.IsConfirmed)
                 _transferDemandItemBetweenWarehousesRepository.Remove(item);
            }
            await _transferDemandItemBetweenWarehousesRepository.SaveAsync();

            await _transferDemandBetweenWarehousesRepository.RemoveAsync(id);
            await _transferDemandBetweenWarehousesRepository.SaveAsync();
            return Ok();
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetNewCode(string serie = "WHD")
        {
            var newCode = await _transferDemandBetweenWarehousesRepository.GetNewCodeAsync(serie, x => x.Code);

            return Ok(new { newCode });
        }

        [HttpGet("[action]/{id?}")]
        public async Task<IActionResult> GetAllByParentId(string? id)
        {
            var transferItems = _transferDemandItemBetweenWarehousesRepository.Table.Where(x => x.ParentId.ToString() == id).Select(x => new
            {
                items = x,
                parent = x.Parent,
                transferingcell = x.Parent.TransferingCellofWarehouse,
                transferedCell=x.Parent.TransferedCellofWarehouse,
                cUserName = _userService.GetUser(x.Parent.CreatingUserId).Result.NameSurname,
                uUserName = x.Parent.UpdatingUserId != null ? _userService.GetUser(x.Parent.UpdatingUserId).Result.NameSurname : null,
            }).Select(x => new TransferDemandItemBetweenWarehousesDto
            {
                Id = x.items.Id.ToString(),
                ParentId = x.items.ParentId.ToString(),
                ParentCode = x.parent.DocumentCode,
                DocumentDate = x.parent.DocumentDate,
                Quantity = x.items.Quantity,
                IsConfirmed=x.items.IsConfirmed,
                UnitId = x.items.UnitId.ToString(),
                UnitCode = x.items.Unit.Code,
                UnitName = x.items.Unit.Name,
                TransferingCellId = x.transferingcell.Id.ToString(),
                TransferingCellName = x.transferingcell.Name,
                TransferingWarehouseName=x.transferingcell.ShelfofWarehouse.HallofWarehouse.Warehouse.Name,
                TransferedCellId = x.transferedCell.Id.ToString(),
                TransferedCellName = x.transferedCell.Name,
                TransferedWarehouseName=x.transferedCell.ShelfofWarehouse.HallofWarehouse.Warehouse.Name,
                MaterialId = x.items.MaterialId.ToString(),
                MaterialCode = x.items.Material.Code,
                MaterialName = x.items.Material.Name,
                CreatingUserId = x.parent.CreatingUserId,
                CreatingUserName = x.cUserName,
                UpdatingUserId = x.parent.UpdatingUserId,
                UpdatingUserName = x.uUserName,
                ConfirmedDate=x.items.ConfirmedDate,
                DemandedDate=x.items.DemandedDate,
                Description=x.items.Description,
                ParentDescription=x.parent.Description,

            }).ToList();

            return Ok(transferItems);
        }
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetItemById(string id)
        {
            var countItem = await _transferDemandItemBetweenWarehousesRepository.GetByIdAsync(id);
            var newCountItem = new TransferDemandItemBetweenWarehousesDto
            {
                Id = countItem.Id.ToString(),
                ParentId = countItem.ParentId.ToString(),
                Quantity = countItem.Quantity,
                UnitId = countItem.UnitId.ToString(),
                UnitCode = countItem.Unit.Code,
                UnitName = countItem.Unit.Name,
                TransferedCellId = countItem.Parent.TransferedCellofWarehouseId.ToString(),
                TransferingCellId = countItem.Parent.TransferingCellofWarehouseId.ToString(),
                MaterialId = countItem.MaterialId.ToString(),
                MaterialCode = countItem.Material.Code,
                MaterialName = countItem.Material.Name,

            };

            return Ok(newCountItem);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddItems(TransferDemandItemBetweenWarehousesDto[] items)
        {
            List<TransferDemandItemBetweenWarehouses> countItems = new List<TransferDemandItemBetweenWarehouses>();

            foreach (var item in items)
            {
                var countItem = new TransferDemandItemBetweenWarehouses
                {
                    MaterialId = Guid.Parse(item.MaterialId),
                    UnitId = item.UnitId != null ? Guid.Parse(item.UnitId) : null,
                    Quantity = Convert.ToDecimal(item.Quantity),
                    ParentId = Guid.Parse(item.ParentId),
                    IsConfirmed = item.IsConfirmed,
                    ConfirmedDate=item.ConfirmedDate,
                    Description= item.Description,
                    DemandedDate = item.DemandedDate,
                    
                };
                countItems.Add(countItem);
            }
            await _transferDemandItemBetweenWarehousesRepository.AddRangeAsync(countItems);

            await _transferDemandItemBetweenWarehousesRepository.SaveAsync();

            return Ok(countItems);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> EditItems(TransferDemandItemBetweenWarehousesDto[] items)
        {
            List<TransferDemandItemBetweenWarehouses> countItems = new List<TransferDemandItemBetweenWarehouses>();

            foreach (var item in items)
            {
                var countItem = new TransferDemandItemBetweenWarehouses
                {
                    Id = Guid.Parse(item.Id),
                    MaterialId = Guid.Parse(item.MaterialId),
                    //UnitId = item.UnitId != null ? Guid.Parse(item.UnitId) : null,
                    Quantity = Convert.ToDecimal(item.Quantity),
                    ParentId = Guid.Parse(item.ParentId),
                    UpdatedDate = DateTime.Now,
                    IsConfirmed = item.IsConfirmed,
                    ConfirmedDate = item.ConfirmedDate,
                    Description = item.Description,
                    DemandedDate = item.DemandedDate,
                };
                countItems.Add(countItem);
            }
            _transferDemandItemBetweenWarehousesRepository.UpdateRange(countItems);

            await _transferDemandItemBetweenWarehousesRepository.SaveAsync();

            //return StatusCode((int)HttpStatusCode.Created);
            return Ok(items);
        }

        [HttpDelete("[action]/{Id}")]
        [Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> DeleteItem(string id)
        {
            await _transferDemandItemBetweenWarehousesRepository.RemoveAsync(id);
            await _transferDemandItemBetweenWarehousesRepository.SaveAsync();
            return Ok();
        }
    }
}
