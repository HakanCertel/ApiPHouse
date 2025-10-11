using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using System.Net;
using YayinEviApi.Application.Abstractions.Services;
using YayinEviApi.Application.DTOs.User;
using YayinEviApi.Application.DTOs.WarehouseDtos;
using YayinEviApi.Application.Features.Queries.WarehouseQ.ShippingOrderQ;
using YayinEviApi.Application.Repositories;
using YayinEviApi.Application.Repositories.IWarehouseR;
using YayinEviApi.Application.Services;
using YayinEviApi.Domain.Entities.WarehouseE;
using YayinEviApi.Domain.Enum;
using YayinEviApi.Infrastructure.Operations;

namespace YayinEviApi.API.Controllers.WarehouseControllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")]

    public class ShippingOrderController : ControllerBase
    {
        private IUserService _userService;
        private IRezervationService _rezervationService;
        readonly CreateUser _user;
        readonly IShippingOrderRepository _shippingOrderRepository;
        readonly IAssignedUserToShippingWorkRepository _assignedUserToShippingWorkRepository;
        readonly IFileManagementReadRepository _fileManagementReadRepository;
        private Expression<Func<ShippingOrder,bool>>? _shippingOrderExpression;
        public ShippingOrderController(IShippingOrderRepository shippingOrderRepository, IUserService userService, IRezervationService rezervationService, IAssignedUserToShippingWorkRepository assignedUserToShippingWorkRepository, IFileManagementReadRepository fileManagementReadRepository)
        {
            _shippingOrderRepository = shippingOrderRepository;
            _userService = userService;
            _user = _userService.GetUser().Result;
            _rezervationService = rezervationService;
            _assignedUserToShippingWorkRepository = assignedUserToShippingWorkRepository;
            _shippingOrderExpression = x=>x.Id!=null;
            _fileManagementReadRepository = fileManagementReadRepository;
        }
        [HttpGet()]
        public async Task<IActionResult> GetAll([FromQuery] GetAllShoppingOrderQueryRequest query)
        {
           
             _shippingOrderExpression = query.State== "Tümü"?x=>x.Id!=null&&x.IsActive==query.Cancel: query.State!=null? x=>x.ShippingOrderState==query.State.GetEnum<WorkState>():x=>x.Id!=null;
            
            if (query.IsLoggingUser == true)
            {
                _shippingOrderExpression=x=>x.AssignedUserId==_user.UserId&&x.IsActive;
            }
            var shippings = _shippingOrderRepository.Table.Where(_shippingOrderExpression).Select(x => new
            {
                ship = x,
                parent = x.SaleItem.Parent,
                sds = x.SaleItem.Parent.SaleItems.ToList(),
                saleItem = x.SaleItem,
                cUserName = _userService.GetUser(x.CreatingUserId).Result.NameSurname,
                uUserName = x.UpdatingUserId != null ? _userService.GetUser(x.UpdatingUserId).Result.NameSurname : null,
                assignedUserName = x.AssignedUserId != null ? _userService.GetUser(x.AssignedUserId).Result.NameSurname : null,
                current = x.SaleItem.Parent.Current,
                material = x.SaleItem.Material,
                unit = x.SaleItem.Material.Unit != null ? x.SaleItem.Material.Unit.Name : null,
                totalRezQ = x.SaleItem.Material.Rezervations.Sum(y => y.RezervationQuantity),
                totalStockQ = x.SaleItem.Material.Stocks.Sum(y => y.Quantity),
            }).ToList().Select(x => new
            {
                main = x,
                rezervation = _rezervationService.GetSingle(x.ship.SaleItem.Id.ToString()).Result,
                imagePath = _fileManagementReadRepository.GetSingleAsync(y => y.EntityId == x.material.Id.ToString() && y.IsActive).Result?.Path,

            }).Select(x => new ShippingOrderDto
            {
                Id = x.main.ship.Id.ToString(),
                //Object = x.main,
                ShippingOrderState = x.main.ship.ShippingOrderState.toName(),
                SaleItemId = x.main.saleItem.Id.ToString(),
                SaleOrderId = x.main.parent.Id.ToString(),
                SaleOrderCode = x.main.parent.Code,
                RezervedCellofWarehouseName = x.rezervation != null ? x.rezervation.CellofWarehouseName : null,
                RezervedShelfofWarehouseName = x.rezervation != null ? x.rezervation.ShelfofWarehouseName : null,
                RezervedHallofWarehouseName = x.rezervation != null ? x.rezervation.HallofWarehouseName : null,
                RezervedWarehouseName = x.rezervation != null ? x.rezervation.WarehouseName : null,
                CreatingUserId = x.main.ship.CreatingUserId,
                CreatingUserName = x.main.cUserName,
                AssignedUserId = x.main.ship.AssignedUserId,
                AssignedUserName = x.main.assignedUserName,
                MaterialId = x.main.material.Id.ToString(),
                MaterialName = x.main.material.Name,
                MaterialUnitName = x.main.unit,
                CurrentName = x.main.current.Name,
                ShippingDate = x.main.parent.DeliveryDate,
                Quantity = x.main.saleItem.Quantity,
                ProccessedQuantity=x.main.ship.ProccessedQuantity,
                RezervedQuantity = x.rezervation != null ? x.rezervation.RezervationQuantity : 0,
                RemainderQuantity =x.main.saleItem.Quantity-x.main.ship.ProccessedQuantity ,
                TotalRezervationQuantity = x.main.totalRezQ,
                TotalStockQuantity = x.main.totalStockQ,
                UsableStockQuantity = x.main.totalStockQ - x.main.totalRezQ,
                IsActive = x.main.ship.IsActive,
                IsShipped = x.main.ship.IsShipped,
                IsStartedCollection = x.main.ship.IsStartedCollection,
                IsComplededCollection = x.main.ship.IsComplededCollection,
                IsStartedPacking = x.main.ship.IsStartedPacking,
                IsCompletedPacking = x.main.ship.IsCompletedPacking,
                OrderCollectionDate = x.main.ship.OrderCollectionDate,
                OrderPackingDate = x.main.ship.OrderPackingDate,
            }).ToList();
           
            return Ok(shippings);
        }

        //[HttpGet("[action]")]
        //public async Task<IActionResult> GetAllOrderForUser()
        //{

        //    var shippings = _shippingOrderRepository.Table.Where(x=>(x.AssignedUserId==_user.UserId || x.AssignedUserId==null)&&(x.ShippingOrderState==WorkState.Waiting||x.ShippingOrderState==WorkState.Proccessed)).Select(x => new
        //    {
        //        ship = x,
        //        parent = x.SaleItem.Parent,
        //        sds = x.SaleItem.Parent.SaleItems.ToList(),
        //        saleItem = x.SaleItem,
        //        cUserName = _userService.GetUser(x.CreatingUserId).Result.NameSurname,
        //        uUserName = x.UpdatingUserId != null ? _userService.GetUser(x.UpdatingUserId).Result.NameSurname : null,
        //        assignedUserName = x.AssignedUserId != null ? _userService.GetUser(x.AssignedUserId).Result.NameSurname : null,
        //        current = x.SaleItem.Parent.Current,
        //        material = x.SaleItem.Material,
        //        unit = x.SaleItem.Material.Unit != null ? x.SaleItem.Material.Unit.Name : null,
        //        totalRezQ = x.SaleItem.Material.Rezervations.Sum(y => y.RezervationQuantity),
        //        totalStockQ = x.SaleItem.Material.Stocks.Sum(y => y.Quantity),
        //    }).ToList().Select(x => new
        //    {
        //        main = x,
        //        rezervation = _rezervationService.GetSingle(x.ship.SaleItem.Id.ToString()).Result,
        //        imagePath = _fileManagementReadRepository.GetSingleAsync(y => y.EntityId == x.material.Id.ToString() && y.IsActive).Result?.Path,

        //    }).Select(x => new ShippingOrderDto
        //    {
        //        Id = x.main.ship.Id.ToString(),
        //        //Object = x.main,
        //        ShippingOrderState = x.main.ship.ShippingOrderState.toName(),
        //        SaleItemId = x.main.saleItem.Id.ToString(),
        //        SaleOrderId = x.main.parent.Id.ToString(),
        //        SaleOrderCode = x.main.parent.Code,
        //        RezervedCellofWarehouseName = x.rezervation != null ? x.rezervation.CellofWarehouseName : null,
        //        RezervedShelfofWarehouseName = x.rezervation != null ? x.rezervation.ShelfofWarehouseName : null,
        //        RezervedHallofWarehouseName = x.rezervation != null ? x.rezervation.HallofWarehouseName : null,
        //        RezervedWarehouseName = x.rezervation != null ? x.rezervation.WarehouseName : null,
        //        CreatingUserId = x.main.ship.CreatingUserId,
        //        CreatingUserName = x.main.cUserName,
        //        AssignedUserId = x.main.ship.AssignedUserId,
        //        AssignedUserName = x.main.assignedUserName,
        //        MaterialId = x.main.material.Id.ToString(),
        //        MaterialName = x.main.material.Name,
        //        MaterialUnitName = x.main.unit,
        //        CurrentName = x.main.current.Name,
        //        ShippingDate = x.main.parent.DeliveryDate,
        //        Quantity = x.main.saleItem.Quantity,
        //        RezervedQuantity = x.rezervation != null ? x.rezervation.RezervationQuantity : 0,
        //        RemainderQuantity = x.rezervation != null ? (x.main.saleItem.Quantity - x.rezervation.RezervationQuantity) : x.main.saleItem.Quantity,
        //        TotalRezervationQuantity = x.main.totalRezQ,
        //        TotalStockQuantity = x.main.totalStockQ,
        //        UsableStockQuantity = x.main.totalStockQ - x.main.totalRezQ,
        //        IsActive = x.main.ship.IsActive,
        //        IsShipped = x.main.ship.IsShipped,
        //        IsStartedCollection = x.main.ship.IsStartedCollection,
        //        IsComplededCollection = x.main.ship.IsComplededCollection,
        //        IsStartedPacking = x.main.ship.IsStartedPacking,
        //        IsCompletedPacking = x.main.ship.IsCompletedPacking,
        //        OrderCollectionDate = x.main.ship.OrderCollectionDate,
        //        OrderPackingDate = x.main.ship.OrderPackingDate,
        //    }).ToList();
        //    return Ok(shippings);
        //}

       

        [HttpPost()]
        public async Task<IActionResult> Add(ShippingOrderDto[] soList)
        {
            List<ShippingOrder> _soList = new List<ShippingOrder>();

            foreach (var ship in soList)
            {
                var _so = new ShippingOrder
                {
                    SaleId=Guid.Parse(ship.SaleOrderId),
                    SaleItemId=Guid.Parse(ship.SaleItemId),
                    CreatingUserId = _user.UserId,
                    ShippingDate = Convert.ToDateTime(ship.ShippingDate),
                    IsActive = ship.IsActive,
                    IsShipped = ship.IsShipped,
                    ShippingOrderState=ship.ShippingOrderState.GetEnum<WorkState>(),
                    
                };
                _soList.Add(_so);
            }
            await _shippingOrderRepository.AddRangeAsync(_soList);

            await _shippingOrderRepository.SaveAsync();

            return StatusCode((int)HttpStatusCode.Created);
        }

        [HttpPut]
        public async Task<IActionResult> Edit(ShippingOrderDto[] soList)
        {
            List<ShippingOrder> _soList = new List<ShippingOrder>();

            foreach (var ship in soList)
            {
                var _so = new ShippingOrder
                {
                    Id=Guid.Parse(ship.Id),
                    //SaleId=Guid.Parse(ship.SaleOrderId),
                    SaleItemId = Guid.Parse(ship.SaleItemId),
                    CreatingUserId = ship.CreatingUserId,
                    ShippingDate = Convert.ToDateTime(ship.ShippingDate),
                    IsActive = ship.IsActive,
                    IsShipped = ship.IsShipped,
                    ShippingOrderState=ship.ShippingOrderState.GetEnum<WorkState>(),
                    AssignedUserId = ship.AssignedUserId,
                    UpdatingUserId=_user.UserId,
                    ProccessedQuantity=ship.ProccessedQuantity,
                };
                _soList.Add(_so);
            }
             _shippingOrderRepository.UpdateRange(_soList);

            await _shippingOrderRepository.SaveAsync();

            return StatusCode((int)HttpStatusCode.Created);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _shippingOrderRepository.RemoveAsync(id);
            await _shippingOrderRepository.SaveAsync();
            return Ok();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddUserForWorkOrder(AssignedUserToShippingWorkDto[] user)
        {
            List<AssignedUserToShippingWork> woList = new List<AssignedUserToShippingWork>();

            foreach (var item in user)
            {
                var wo = new AssignedUserToShippingWork
                {
                    ShippingOrderId = Guid.Parse(item.ShippingOrderId),
                    UserId = item.UserId,
                    IsActive=true,
                };

                woList.Add(wo);
            }
            await _assignedUserToShippingWorkRepository.AddRangeAsync(woList);
            await _assignedUserToShippingWorkRepository.SaveAsync();

            return StatusCode((int)HttpStatusCode.Created);

        }
        [HttpPut("[action]")]
        public async Task<IActionResult> EditUserForWorkOrder(AssignedUserToShippingWorkDto[] user)
        {
            List<AssignedUserToShippingWork> woList = new List<AssignedUserToShippingWork>();

            foreach (var item in user)
            {
                var wo = new AssignedUserToShippingWork
                {
                    Id=Guid.Parse(item.Id),
                    ShippingOrderId = Guid.Parse(item.ShippingOrderId),
                    UserId = item.UserId,
                    IsActive= item.IsActive,
                };

                woList.Add(wo);
            }
             _assignedUserToShippingWorkRepository.UpdateRange(woList);
            await _assignedUserToShippingWorkRepository.SaveAsync();

            return StatusCode((int)HttpStatusCode.Created);

        }

        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> DeleteUserForWorkOrder(string id)
        {
            await _assignedUserToShippingWorkRepository.RemoveAsync(id);
            await _assignedUserToShippingWorkRepository.SaveAsync();
            return Ok();
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllOrderForUser()
        {

            var shippings = _assignedUserToShippingWorkRepository.Table.Where(x => x.UserId.ToString() == _user.UserId && x.IsActive).Select(x => new
            {
                assignedWork = x,
                ship = x.ShippingOrder,
                saleItem = x.ShippingOrder.SaleItem,
                parent = x.ShippingOrder.SaleItem.Parent,
                cUserName = _userService.GetUser(x.ShippingOrder.CreatingUserId).Result.NameSurname,
                uUserName = x.ShippingOrder.UpdatingUserId != null ? _userService.GetUser(x.ShippingOrder.UpdatingUserId).Result.NameSurname : null,
                assignedUserName = x.UserId != null ? _userService.GetUser(x.UserId).Result.NameSurname : null,
                current = x.ShippingOrder.SaleItem.Parent.Current,
                material = x.ShippingOrder.SaleItem.Material,
                unit = x.ShippingOrder.SaleItem.Material.Unit != null ? x.ShippingOrder.SaleItem.Material.Unit.Name : null,
                totalRezQ = x.ShippingOrder.SaleItem.Material.Rezervations.Sum(y => y.RezervationQuantity),
                totalStockQ = x.ShippingOrder.SaleItem.Material.Stocks.Sum(y => y.Quantity),
            }).ToList().Select(x => new
            {
                main = x,
                rezervation = _rezervationService.GetSingle(x.saleItem.Id.ToString()).Result,

            }).Select(x => new AssignedUserToShippingWorkDto
            {
                Id = x.main.assignedWork.Id.ToString(),
                ShippingOrderState = x.main.ship.ShippingOrderState.toName(),
                ShippingOrderId = x.main.ship.Id.ToString(),
                SaleOrderCode = x.main.parent.Code,
                SaleItemId = x.main.ship.SaleItemId.ToString(),
                SaleOrderId=x.main.saleItem.ParentId.ToString(),
                RezervationItemId = x.rezervation != null ? x.rezervation.Id.ToString() : null,
                RezervedCellofWarehouseId = x.rezervation != null ? x.rezervation.CellofWarehouseId : null,
                RezervedCellofWarehouseName = x.rezervation != null ? x.rezervation.CellofWarehouseName : null,
                RezervedShelfofWarehouseName = x.rezervation != null ? x.rezervation.ShelfofWarehouseName : null,
                RezervedHallofWarehouseName = x.rezervation != null ? x.rezervation.HallofWarehouseName : null,
                RezervedWarehouseName = x.rezervation != null ? x.rezervation.WarehouseName : null,
                CreatingUserId = x.main.ship.CreatingUserId,
                CreatingUserName = x.main.cUserName,
                AssignedUserId = x.main.ship.AssignedUserId,
                AssignedUserName = x.main.assignedUserName,
                MaterialId = x.main.material.Id.ToString(),
                MaterialName = x.main.material.Name,
                MaterialUnitName = x.main.unit,
                CurrentName = x.main.current.Name,
                ShippingDate = x.main.parent.DeliveryDate,
                Quantity = x.main.saleItem.Quantity,
                ProccessedQuantity=x.main.ship.ProccessedQuantity,
                CollectedQuantity = x.rezervation != null ? x.rezervation.RezervationQuantity : 0,
                RezervedQuantity = x.rezervation != null ? x.rezervation.RezervationQuantity : 0,
                RemainderQuantity = x.main.saleItem.Quantity -Convert.ToDecimal( x.main.ship.ProccessedQuantity),
                TotalRezervationQuantity = x.main.totalRezQ,
                TotalStockQuantity = x.main.totalStockQ,
                UsableStockQuantity = x.main.totalStockQ - x.main.totalRezQ,
                IsActive = x.main.assignedWork.IsActive,
                IsShipped = x.main.ship.IsShipped,
                UserId = _user.UserId,
            }).ToList();
            return Ok(shippings);
        }

    }
}
