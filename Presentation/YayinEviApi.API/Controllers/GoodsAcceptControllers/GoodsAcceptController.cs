using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Reflection.Metadata.Ecma335;
using YayinEviApi.Application.Abstractions.Services;
using YayinEviApi.Application.DTOs.GoodsAcceptDtos;
using YayinEviApi.Application.DTOs.User;
using YayinEviApi.Application.Repositories.IGoogsAcceptR;
using YayinEviApi.Application.RequestParameters;
using YayinEviApi.Domain.Entities.GoodsAccepE;
using YayinEviApi.Domain.Entities.GoodsAcceptE;

namespace YayinEviApi.API.Controllers.GoodsAcceptControllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")]

    public class GoodsAcceptController : ControllerBase
    {
        private IUserService _userService;
        readonly CreateUser _user;
        readonly IGoodsAcceptRepository _goodsAcceptRepository;
        readonly IGoodsAccepItemRepository _goodsAccepItemRepository;

        public GoodsAcceptController(IUserService userService, IGoodsAcceptRepository goodsAcceptRepository, IGoodsAccepItemRepository goodsAccepItemRepository)
        {
            _userService = userService;
            _user = _userService.GetUser().Result;
            _goodsAcceptRepository = goodsAcceptRepository;
            _goodsAccepItemRepository = goodsAccepItemRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] Pagination pagination)
        {
            var totalCount = _goodsAcceptRepository.GetAll(false).Count();
            
            var goodsAccepts = _goodsAcceptRepository.Table.Select(x => new
            {
                gAccept=x,
                cUserName = _userService.GetUser(x.CreatingUserId).Result.NameSurname,
                uUserName =x.UpdatingUserId!=null? _userService.GetUser(x.UpdatingUserId).Result.NameSurname:null,
            }).Select(x => new GoodsAcceptDto
            {
                Id=x.gAccept.Id.ToString(),
                Code=x.gAccept.Code,
                DocumentCode=x.gAccept.DocumentCode,
                DocumentDate=x.gAccept.DocumentDate,
                CreatingUserId=x.gAccept.CreatingUserId,
                CreatingUserName = x.cUserName,
                UpdatingUserId =x.gAccept.UpdatingUserId,
                UpdatingUserName = x.uUserName,
                AcceptCellofWarehouseId=x.gAccept.AcceptCellofWarehouseId.ToString(),
                AcceptCellofWarehouseName=x.gAccept.AcceptCellofWarehouse.Name,
                WarehouseName=x.gAccept.AcceptCellofWarehouse.ShelfofWarehouse.HallofWarehouse.Warehouse.Name,
                Address=x.gAccept.Address,
                Country=x.gAccept.Country,
                County=x.gAccept.County,
                CurrentId = x.gAccept.CurrentId.ToString(),
                CurrentName = x.gAccept.CurrentName,
                Town = x.gAccept.Town,
                
            }).ToList();
            var entities=goodsAccepts.Select(x => x).Skip(pagination.Page * pagination.Size).Take(pagination.Size);

            return Ok(new { totalCount,entities });
        }

        [HttpGet("[action]/{id}")]
        [Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> GetById(string id)
        {
            var sc = await _goodsAcceptRepository.Select(x=>x.Id.ToString()==id, x => new
            {
                gAccept = x,
                cUserName = _userService.GetUser(x.CreatingUserId).Result.NameSurname,
                uUserName = x.UpdatingUserId != null ? _userService.GetUser(x.UpdatingUserId).Result.NameSurname : null,
            }).Select(x => new GoodsAcceptDto
            {
                Id = x.gAccept.Id.ToString(),
                DocumentCode = x.gAccept.DocumentCode,
                DocumentDate = x.gAccept.DocumentDate,
                CreatingUserId = x.gAccept.CreatingUserId,
                CreatingUserName = x.cUserName,
                UpdatingUserId = x.gAccept.UpdatingUserId,
                UpdatingUserName = x.uUserName,
                AcceptCellofWarehouseId = x.gAccept.AcceptCellofWarehouseId.ToString(),
                AcceptCellofWarehouseName = x.gAccept.AcceptCellofWarehouse.Name,
                WarehouseName=x.gAccept.AcceptCellofWarehouse.ShelfofWarehouse.HallofWarehouse.Warehouse.Name,
                Address = x.gAccept.Address,
                Country = x.gAccept.Country,
                County = x.gAccept.County,
                CurrentId = x.gAccept.CurrentId!=null?x.gAccept.CurrentId.ToString():null,
                CurrentName=x.gAccept.CurrentName,
                Town = x.gAccept.Town,
                CreatedDate= x.gAccept.CreatedDate

            }).FirstOrDefaultAsync();

            return Ok(sc);
        }
        [HttpPost]
        public async Task<IActionResult> Add(GoodsAcceptDto goodsAccept)
        {
            GoodsAccep ga = new GoodsAccep
            {
                DocumentCode= goodsAccept.DocumentCode,
                DocumentDate=Convert.ToDateTime(goodsAccept.DocumentDate),
                CreatingUserId=_user.UserId,
                CreatedDate=DateTime.Now,
                IsActive=true,
                AcceptCellofWarehouseId=Guid.Parse(goodsAccept.AcceptCellofWarehouseId),
                Address = goodsAccept.Address,
                Country = goodsAccept.Country,
                County=goodsAccept.County,
                CurrentId=goodsAccept.CurrentId!=null ? Guid.Parse(goodsAccept.CurrentId) : null,
                CurrentName=goodsAccept.CurrentName,
                Town = goodsAccept.Town,
                
            };
            await _goodsAcceptRepository.AddAsync(ga);

            await _goodsAcceptRepository.SaveAsync();
            goodsAccept.Id = ga.Id.ToString();
            return Ok(goodsAccept);
            //return StatusCode((int)HttpStatusCode.Created);

        }
        [HttpPut]
        public async Task<IActionResult> Edit(GoodsAcceptDto goodsAccept)
        {
            _goodsAcceptRepository.Update(new()
            {
                Id=Guid.Parse(goodsAccept.Id),
                DocumentCode= goodsAccept.DocumentCode,
                DocumentDate=Convert.ToDateTime(goodsAccept.DocumentDate),
                CreatingUserId= goodsAccept.CreatingUserId,
                UpdatedDate=DateTime.Now,
                UpdatingUserId=_user.UserId,
                AcceptCellofWarehouseId=Guid.Parse(goodsAccept.AcceptCellofWarehouseId),
                Address=goodsAccept.Address,
                Country=goodsAccept.Country,
                County=goodsAccept.County,
                //CurrentId=goodsAccept.CurrentId!=null?Guid.Parse(goodsAccept.CurrentId):null,
                CurrentName =goodsAccept.CurrentName,
                Town=goodsAccept.Town,
                CreatedDate= Convert.ToDateTime(goodsAccept.CreatedDate)
            });
            await _goodsAcceptRepository.SaveAsync();

            //return StatusCode((int)HttpStatusCode.Created);
            return Ok(goodsAccept);

        }

        [HttpDelete("{Id}")]
        [Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> Delete(string id)
        {
            var countItems=_goodsAccepItemRepository.GetWhere(x=>x.ParentId.ToString()==id);
            foreach (var item in countItems)
            {
                 _goodsAccepItemRepository.Remove(item);
            }

            await _goodsAccepItemRepository.SaveAsync();
            await _goodsAcceptRepository.RemoveAsync(id);
            await _goodsAcceptRepository.SaveAsync();

            return Ok();
        }

        [HttpGet("[action]/{id?}")]
        public async Task<IActionResult> GetAllByParentId(string? id)
        {
            var stockCountItems = _goodsAccepItemRepository.Table.Where(x => x.ParentId.ToString() == id).Select(x => new
            {
                items = x,
                parent = x.Parent,
                cell = x.Parent.AcceptCellofWarehouse,
                cUserName = _userService.GetUser(x.Parent.CreatingUserId).Result.NameSurname,
                uUserName = x.Parent.UpdatingUserId != null ? _userService.GetUser(x.Parent.UpdatingUserId).Result.NameSurname:null,
            }).Select(x => new GoodsAcceptItemDto
            {
                Id = x.items.Id.ToString(),
                ParentId=x.items.ParentId.ToString(),
                ParentCode=x.parent.DocumentCode,
                DocumentDate=x.parent.DocumentDate,
                Quantity=x.items.Quantity,
                UnitId=x.items.UnitId.ToString(),
                UnitCode=x.items.Unit.Code,
                UnitName=x.items.Unit.Name,
                EnteringCellId=x.cell.Id.ToString(),
                EnteringCellName=x.cell.Name,
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
            var countItem = await _goodsAccepItemRepository.GetByIdAsync(id);
            var newCountItem = new GoodsAcceptItemDto
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
        public async Task<IActionResult> AddItems(GoodsAcceptItemDto[] items)
        {
            List<GoodsAcceptItems> countItems = new List<GoodsAcceptItems>();

            foreach (var item in items)
            {
                var countItem = new GoodsAcceptItems
                {
                    MaterialId = Guid.Parse(item.MaterialId),
                    UnitId =item.UnitId!=null? Guid.Parse(item.UnitId):null,
                    Quantity=Convert.ToDecimal( item.Quantity),
                    ParentId=Guid.Parse(item.ParentId),
                };
                countItems.Add(countItem);
            }
            await _goodsAccepItemRepository.AddRangeAsync(countItems);

            await _goodsAccepItemRepository.SaveAsync();

            return Ok(countItems);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> EditItems(GoodsAcceptItemDto[] items)
        {
            List<GoodsAcceptItems> countItems = new List<GoodsAcceptItems>();

            foreach (var item in items)
            {
                var countItem = new GoodsAcceptItems
                {
                    Id= Guid.Parse(item.Id),
                    MaterialId = Guid.Parse(item.MaterialId),
                    //UnitId = item.UnitId != null ? Guid.Parse(item.UnitId) : null,
                    Quantity = Convert.ToDecimal(item.Quantity),
                    ParentId = Guid.Parse(item.ParentId),
                    UpdatedDate = DateTime.Now,
                };
                countItems.Add(countItem);
            }
             _goodsAccepItemRepository.UpdateRange(countItems);

            await _goodsAccepItemRepository.SaveAsync();

            //return StatusCode((int)HttpStatusCode.Created);
            return Ok(items);
        }
        [HttpDelete("[action]/{Id}")]
        [Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> DeleteItem(string id)
        {
            await _goodsAccepItemRepository.RemoveAsync(id);
            await _goodsAccepItemRepository.SaveAsync();
            return Ok();
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetNewCode(string serie = "IRS")
        {
            var newCode = await _goodsAcceptRepository.GetNewCodeAsync(serie, x => x.Code);

            return Ok(new { newCode });
        }
    }
}
