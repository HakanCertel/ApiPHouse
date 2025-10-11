using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using YayinEviApi.Application.Abstractions.Services;
using YayinEviApi.Application.DTOs.User;
using YayinEviApi.Application.DTOs.WarehouseDtos;
using YayinEviApi.Application.Repositories;
using YayinEviApi.Application.Repositories.IWarehouseR;
using YayinEviApi.Application.RequestParameters;
using YayinEviApi.Application.Services;
using YayinEviApi.Domain.Entities.WarehouseE;

namespace YayinEviApi.API.Controllers.WarehouseControllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")]

    public class OrderCollectionController : ControllerBase
    {
        private IUserService _userService;
        readonly CreateUser _user;
        private IRezervationService _rezervationService;
        readonly IOrderCollectionRepository _orderCollectionRepository;
        readonly IOrderCollectionItemRepository _orderCollectionItemRepository;
        readonly IShippingOrderRepository _shippingOrderRepository;
        readonly IFileManagementReadRepository _fileManagementReadRepository;
        private Expression<Func<OrderCollection, bool>>? _shippingOrderExpression;

        public OrderCollectionController(IRezervationService rezervationService, IUserService userService, IOrderCollectionRepository orderCollectionRepository, IOrderCollectionItemRepository orderCollectionItemRepository, IShippingOrderRepository shippingOrderRepository, IFileManagementReadRepository fileManagementReadRepository)
        {
            _rezervationService = rezervationService;
            _userService = userService;
            _orderCollectionRepository = orderCollectionRepository;
            _orderCollectionItemRepository = orderCollectionItemRepository;
            _shippingOrderRepository = shippingOrderRepository;
            _fileManagementReadRepository = fileManagementReadRepository;
            _shippingOrderExpression = x => !x.IsCollectionCompleted;
            _user = _userService.GetUser().Result;

        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] NullablePagination? pagination)
        {
            var totalCount = _orderCollectionRepository.GetAll().Count();

            var orderCollections = _orderCollectionRepository.Table.Where(_shippingOrderExpression).Select(x => new
            {
                oCollection = x,
                //oCollectionItems=x.OrderCollectionItems,
                cUserName = _userService.GetUser(x.CreatingUserId).Result.NameSurname,
            }).Select(x => new OrderCollectionDto
            {
                Id = x.oCollection.Id.ToString(),
                Code = x.oCollection.Code,
                IsActive=x.oCollection.IsActive,
                IsCollectionCompleted=x.oCollection.IsCollectionCompleted,
                CreatingUserId = x.oCollection.CreatingUserId,
                CreatingUserName = x.cUserName,
                //OrderCollectionItems=x.oCollectionItems,

            }).ToList();
            var entities = orderCollections.Select(x => x).Skip(Convert.ToInt32(pagination.Page) * Convert.ToInt32(pagination.Size)).Take(pagination.Size??totalCount);

            return Ok(new { totalCount, entities });
        }


        [HttpGet("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> GetByIsActive()
        {
            var _oc = await _orderCollectionRepository.Select(x => !x.IsCollectionCompleted, x => new
            {
                oc = x,
                //ocItems=x.OrderCollectionItems,
                cUserName = _userService.GetUser(x.CreatingUserId).Result.NameSurname,
               
            }).Select(x => new OrderCollectionDto
            {
                Id = x.oc.Id.ToString(),
                Code = x.oc.Code,
                IsActive=x.oc.IsActive,
                IsCollectionCompleted=x.oc.IsCollectionCompleted,
                //OrderCollectionItems=x.ocItems,
                CreatingUserId = x.oc.CreatingUserId,
                CreatingUserName = x.cUserName,

            }).FirstOrDefaultAsync();

            return Ok(_oc);
        }

        [HttpPost]
        public async Task<IActionResult> Add(OrderCollectionDto oc)
        {
            OrderCollection _oc = new OrderCollection
            {
                Code = oc.Code,
                CreatingUserId = _user.UserId,
                IsActive = oc.IsActive,
            };
            
            await _orderCollectionRepository.AddAsync(_oc);

            await _orderCollectionRepository.SaveAsync();
            oc.Id = _oc.Id.ToString();
            oc.CreatingUserId = _oc.CreatingUserId;
            return Ok(oc);
            //return StatusCode((int)HttpStatusCode.Created);

        }
        [HttpPut]
        public async Task<IActionResult> Edit(OrderCollectionDto oc)
        {
            OrderCollection _oc = new OrderCollection
            {
                Id=Guid.Parse(oc.Id),
                Code = oc.Code,
                CreatingUserId = oc.CreatingUserId,
                UpdatingUserId=_user.UserId,
                IsCollectionCompleted=oc.IsCollectionCompleted,
                UpdatedDate = DateTime.Now,
                IsActive = oc.IsActive,
            };

             _orderCollectionRepository.Update(_oc);

            await _orderCollectionRepository.SaveAsync();
            return Ok(oc);
            //return StatusCode((int)HttpStatusCode.Created);

        }

        [HttpGet("[action]/{Id}")]
        public async Task<IActionResult> GetAllByParentId( string id)
        {
           
            var shippings = _orderCollectionItemRepository.Table.Where(x=>x.ParentId==Guid.Parse(id)&&x.Parent.CreatingUserId==_user.UserId).Select(x => new
            {
                ocItem = x,
                saleItem = x.SaleItem,
                saleOrder=x.SaleItem.Parent,
                current=x.SaleItem.Parent.Current,
                cUserName = _userService.GetUser(x.Parent.CreatingUserId).Result.NameSurname,
                material = x.SaleItem.Material,
                unit = x.SaleItem.Material.Unit != null ? x.SaleItem.Material.Unit.Name : null,
                cell=x.Cell,
            }).ToList().Select(x => new
            {
                main=x,
                rezervation=_rezervationService.GetSingle(x.saleItem.Id.ToString()).Result,
                imagePath = _fileManagementReadRepository.GetSingleAsync(y => y.EntityId == x.material.Id.ToString() && y.IsActive).Result?.Path,

            }).Select(x => new OrderCollectionItemDto
            {
                Id = x.main.ocItem.Id.ToString(),
                ParentId = x.main.ocItem.ParentId.ToString(),
                //ShippingOrderId=x.main.ocItem.ShippingOrderId.ToString(),
                //SaleOrderId=x.main.saleOrder.Id.ToString(),
                SaleOrderCode = x.main.saleOrder.Code,
                CurrentName = x.main.current.Name,
                ShippingDate = x.main.saleOrder.DeliveryDate,
                SaleItemId = x.main.saleItem.Id.ToString(),
                MaterialId = x.main.ocItem.MaterialId.ToString(),
                MaterialName = x.main.material.Name,
                UnitName = x.main.unit,
                Quantity = x.main.saleItem.Quantity,
                RezervationQuantity=x.rezervation!=null?x.rezervation.RezervationQuantity:0,
                IsActive = x.main.ocItem.IsActive,
                ImagePath=x.imagePath,
                CollectedQuantity=x.main.ocItem.CollectedQuantity,
                CollectedCellId=x.main.ocItem.CellId.ToString(),
                CollectedCellName=x.main.cell.Name,
                SaleOrderId=x.main.saleOrder.Id.ToString(),
                ShippingOrderId=x.main.ocItem.ShippingOrderId.ToString(),
            }).ToList();
            return Ok(shippings);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllItem()
        {

            var shippings = _orderCollectionItemRepository.Table.Where(x=>x.IsActive).Select(x => new
            {
                ocItem = x,
                parent=x.Parent,
                saleItem = x.SaleItem,
                saleOrder = x.SaleItem.Parent,
                current = x.SaleItem.Parent.Current,
                cUserName = _userService.GetUser(x.Parent.CreatingUserId).Result.NameSurname,
                material = x.SaleItem.Material,
                unit = x.SaleItem.Material.Unit != null ? x.SaleItem.Material.Unit.Name : null,
                cell = x.Cell,
            }).Where(x=>x.parent.IsCollectionCompleted).Select(x => new OrderCollectionItemDto
            {
                Id = x.ocItem.Id.ToString(),
                ParentId = x.ocItem.ParentId.ToString(),
                //ShippingOrderId=x.main.ocItem.ShippingOrderId.ToString(),
                //SaleOrderId=x.main.saleOrder.Id.ToString(),
                SaleOrderCode = x.saleOrder.Code,
                CurrentName = x.current.Name,
                ShippingDate = x.saleOrder.DeliveryDate,
                SaleItemId = x.saleItem.Id.ToString(),
                MaterialId = x.ocItem.MaterialId.ToString(),
                MaterialName = x.material.Name,
                UnitName = x.unit,
                Quantity = x.saleItem.Quantity,
                IsActive = x.ocItem.IsActive,
                CollectedQuantity = x.ocItem.CollectedQuantity,
                CollectedCellId = x.ocItem.CellId.ToString(),
                CollectedCellName = x.cell.Name,
                SaleOrderId = x.saleOrder.Id.ToString(),
                ShippingOrderId = x.ocItem.ShippingOrderId.ToString(),
            }).ToList();
            return Ok(shippings);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddItems(OrderCollectionItemDto[] items)
        {
            List<OrderCollectionItem> ocItems = new List<OrderCollectionItem>();

            foreach (var item in items)
            {
                var ocItem = new OrderCollectionItem
                {
                    ParentId = Guid.Parse(item.ParentId),
                    IsActive = item.IsActive,
                    ShippingOrderId = Guid.Parse(item.ShippingOrderId),
                    CollectedQuantity = Convert.ToDecimal(item.CollectedQuantity),
                    CellId =Guid.Parse(item.CollectedCellId),
                    MaterialId=Guid.Parse(item.MaterialId),
                    SaleItemId = Guid.Parse(item.SaleItemId),
                    //SaleOrderId =Guid.Parse(item.SaleOrderId),
                    
                };
                //countItems.Add(countItem);
                await _orderCollectionItemRepository.AddAsync(ocItem);
                item.Id = ocItem.Id.ToString();
            }

            await _orderCollectionItemRepository.SaveAsync();

            //return StatusCode((int)HttpStatusCode.Created);
            return Ok(items);
        }
    }
}
