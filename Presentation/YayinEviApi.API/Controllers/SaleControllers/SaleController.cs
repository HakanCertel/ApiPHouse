using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using YayinEviApi.Application.Abstractions.Services;
using YayinEviApi.Application.DTOs.SaleDtos;
using YayinEviApi.Application.DTOs.User;
using YayinEviApi.Application.Repositories.ISaleR;
using YayinEviApi.Application.RequestParameters;
using YayinEviApi.Application.Services;
using YayinEviApi.Domain.Entities.SalesE;
using YayinEviApi.Domain.Enum;
using YayinEviApi.Infrastructure.Operations;

namespace YayinEviApi.API.Controllers.SaleControllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")]

    public class SaleController : ControllerBase
    {
        private IUserService _userService;
        readonly CreateUser _user;
        private IRezervationService _rezervationService;
        readonly ISaleRepository _saleRepository;
        readonly ISaleItemRepository _saleItemRepository;

        public SaleController(ISaleRepository saleRepository, IUserService userService, ISaleItemRepository saleItemRepository, IRezervationService rezervationService)
        {
            _saleRepository = saleRepository;
            _userService = userService;
            _user = _userService.GetUser().Result;
            _saleItemRepository = saleItemRepository;
            _rezervationService = rezervationService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] Pagination pagination)
        {
            var totalCount = _saleRepository.GetAll(false).Count();

            var sales = _saleRepository.Table.Select(x => new
            {
                sale = x,
                cUserName = _userService.GetUser(x.CreatingUserId).Result.NameSurname,
                uUserName = x.UpdatingUserId != null ? _userService.GetUser(x.UpdatingUserId).Result.NameSurname : null,
            }).Select(x => new SaleDto
            {
                Id = x.sale.Id.ToString(),
                Code = x.sale.Code,
                DocumentDate = x.sale.DocumentDate,
                DeliveryDate=x.sale.DeliveryDate,
                CreatingUserId = x.sale.CreatingUserId,
                CreatingUserName = x.cUserName,
                UpdatingUserId = x.sale.UpdatingUserId,
                UpdatingUserName = x.uUserName,
                CurrentLocalOrForeing=x.sale.Current.LocalOrForeing.toName(),
                CurrentCode = x.sale.Code,
                CurrentAddress = x.sale.Current.Address,
                CurrentCountry = x.sale.Current.Country,
                CurrentCounty = x.sale.Current.County,
                CurrentId = x.sale.CurrentId.ToString(),
                CurrentName = x.sale.Current.Name,
                CurrencyType = x.sale.CurrencyType.toName(),
                DeliveryLocalOrForeing= x.sale.DeliveryCurrent.LocalOrForeing.toName(),
                DeliveryCurrentCode=x.sale.DeliveryCurrent.Code,
                DeliveryCurrentName=x.sale.DeliveryCurrent.Name,
                DeliveryCurrentCountry=x.sale.DeliveryCurrent.Country,
                DeliveryCurrentCounty=x.sale.DeliveryCurrent.County,
                DeliveryCurrentAddress=x.sale.DeliveryCurrent.Address,
                DeliveryCurrentId=x.sale.DeliveryCurrentId.ToString(),
                CreatedDate=x.sale.CreatedDate,
                IsSendedShippingOrder=x.sale.IsSendedShippingOrder,
                IsActive=x.sale.IsActive,

            }).ToList();
            var entities= sales.Select(x => x).Skip(pagination.Page * pagination.Size).Take(pagination.Size);

            return Ok(new { totalCount,entities });
        }

        [HttpGet("[action]/{id}")]
        [Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> GetById(string id)
        {
            var sc = await _saleRepository.Select(x=>x.Id.ToString()==id, x => new
            {
                sale = x,
                cUserName = _userService.GetUser(x.CreatingUserId).Result.NameSurname,
                uUserName = x.UpdatingUserId != null ? _userService.GetUser(x.UpdatingUserId).Result.NameSurname : null,
                netTotal=x.SaleItems!=null ? x.SaleItems.Select(y=>new  {
                    net=y.Price*y.Quantity
                }).Sum(y=>y.net):0,
                itemDiscountTotal =x.SaleItems!=null? x.SaleItems.Select(y => new
                {
                    discount=(y.ItemDiscountRate*y.Quantity*y.Price)/100
                }).Sum(y=>y.discount):0,
                taxTotal = x.SaleItems != null ? x.SaleItems.Select(y => new
                {
                    tax=((y.Price*y.Quantity)-((y.Price*y.Quantity*y.ItemDiscountRate)/100))
                }).Sum(y=>y.tax):0,
            }).Select(x => new SaleDto
            {
                Id = x.sale.Id.ToString(),
                DocumentDate = x.sale.DocumentDate,
                DeliveryDate = x.sale.DeliveryDate,
                CreatingUserId = x.sale.CreatingUserId,
                CreatingUserName = x.cUserName,
                UpdatingUserId = x.sale.UpdatingUserId,
                UpdatingUserName = x.uUserName,
                CurrentAddress = x.sale.Current.Address,
                CurrentCountry = x.sale.Current.Country,
                CurrentCounty = x.sale.Current.County,
                CurrentId = x.sale.CurrentId.ToString(),
                CurrentName = x.sale.Current.Name,
                CurrencyType = x.sale.CurrencyType.toName(),
                Code = x.sale.Code,
                CurrentCode = x.sale.Code,
                DeliveryCurrentCode = x.sale.DeliveryCurrent.Code,
                DeliveryCurrentName = x.sale.DeliveryCurrent.Name,
                DeliveryCurrentCountry = x.sale.DeliveryCurrent.Country,
                DeliveryCurrentCounty = x.sale.DeliveryCurrent.County,
                DeliveryCurrentAddress = x.sale.DeliveryCurrent.Address,
                DeliveryCurrentId = x.sale.DeliveryCurrentId.ToString(),
                CreatedDate = x.sale.CreatedDate,
                FixDiscount=Convert.ToDecimal(x.sale.FixDiscount),
                IsSendedShippingOrder=x.sale.IsSendedShippingOrder,

            }).FirstOrDefaultAsync();

            return Ok(sc);
        }
        
        [HttpPost]
        public async Task<IActionResult> Add(SaleDto sale)
        {
            Sale _sale = new Sale
            {
                Code= sale.Code,
                DocumentDate=Convert.ToDateTime(sale.DocumentDate),
                CreatingUserId=_user.UserId,
                CreatedDate=DateTime.Now,
                IsActive=sale.IsActive,
                CurrentId=Guid.Parse(sale.CurrentId),
                DeliveryCurrentId=sale.DeliveryCurrentId!=null?Guid.Parse(sale.DeliveryCurrentId):null,
                DeliveryDate= sale.DocumentDate,
                PaymentMethodId=sale.PaymentMethodId!=null?Guid.Parse(sale.PaymentMethodId):null,
                PriceListId=sale.PriceListId!=null?Guid.Parse(sale.PriceListId):null,
            };
            _sale.CurrencyType = sale.CurrencyType.GetEnum<CurrencyType>();
            await _saleRepository.AddAsync(_sale);

            await _saleRepository.SaveAsync();
            sale.Id = _sale.Id.ToString();
            sale.CreatingUserId = _sale.CreatingUserId;
            return Ok(sale);
            //return StatusCode((int)HttpStatusCode.Created);

        }
       
        [HttpPut]
        public async Task<IActionResult> Edit(SaleDto sale)
        {
            _saleRepository.Update(new()
            {
                Id=Guid.Parse(sale.Id),
                Code= sale.Code,
                DocumentDate=Convert.ToDateTime(sale.DocumentDate),
                CreatingUserId= sale.CreatingUserId,
                UpdatedDate=DateTime.Now,
                UpdatingUserId=_user.UserId,
                CurrentId = Guid.Parse(sale.CurrentId),
                //DeliveryCurrentId = sale.DeliveryCurrentId != null ? Guid.Parse(sale.DeliveryCurrentId) : null,
                CurrencyType = sale.CurrencyType.GetEnum<CurrencyType>(),
                DeliveryDate = sale.DocumentDate,
                PaymentMethodId = sale.PaymentMethodId != null ? Guid.Parse(sale.PaymentMethodId) : null,
                PriceListId = sale.PriceListId != null ? Guid.Parse(sale.PriceListId) : null,
                IsSendedShippingOrder=sale.IsSendedShippingOrder,
                IsActive = sale.IsActive,
            });
            await _saleRepository.SaveAsync();

            //return StatusCode((int)HttpStatusCode.Created);
            return Ok(sale);

        }

        [HttpDelete("{Id}")]
        [Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> Delete(string id)
        {
            var countItems=_saleItemRepository.GetWhere(x=>x.ParentId.ToString()==id);
            foreach (var item in countItems)
            {
                 _saleItemRepository.Remove(item);
            }

            await _saleItemRepository.SaveAsync();
            await _saleRepository.RemoveAsync(id);
            await _saleRepository.SaveAsync();

            return Ok();
        }

        [HttpGet("[action]/{id?}")]
        public async Task<IActionResult> GetAllByParentId(string? id)
        {
            var saleItems = _saleItemRepository.Table.Where(x => x.ParentId == Guid.Parse(id)).Select(x => new
            {
                item = x,
                parent = x.Parent,
                cUserName = _userService.GetUser(x.Parent.CreatingUserId).Result.NameSurname,
                uUserName = x.Parent.UpdatingUserId != null ? _userService.GetUser(x.Parent.UpdatingUserId).Result.NameSurname:null,
                net= x.Quantity * x.Price-(x.Quantity*x.Price*x.ItemDiscountRate/100),
                kdv= (x.Quantity * x.Price - (x.Quantity * x.Price * x.ItemDiscountRate / 100)) *x.TaxType.toName().TaxConverter(),
                material=x.Material,
                current=x.Parent.Current,
                //deliveryCur=x.Parent.DeliveryCurrent,
                //rezercQ=_rezervationService.GetSingle(x.Id.ToString()).RezervationQuantity,

            }).ToList().Select(x => new SaleItemDto
            {
                Id = x.item.Id.ToString(),
                ParentId=x.item.ParentId.ToString(),
                ParentCode=x.parent.Code,
                DocumentDate=x.parent.DocumentDate,
                Quantity=x.item.Quantity,
                RezervedQuantity= _rezervationService.GetSingle(x.item.Id.ToString()).Result==null?0: _rezervationService.GetSingle(x.item.Id.ToString()).Result.RezervationQuantity,
                MaterialUnitId = x.item.Material.UnitId.ToString(),
                //MaterialUnitName = x.material.Unit.Name,
                MaterialId=x.material.Id.ToString(),
                MaterialCode=x.material.Code,
                MaterialName=x.material.Name,
                DeliveryDate=x.parent.DeliveryDate,
                CurrencyType=x.parent.CurrencyType.toName(),
                CurrentId=x.current.Id.ToString(),
                CurrentCode=x.current.Code,
                CurrentName=x.current.Name,
                CurrentAddress=x.current.Address,
                CurrentCountry=x.current.Country,
                CurrentCounty=x.current.County,
                //DeliveryCurrentId=x.deliveryCur.ToString(),
                //DeliveryCurrentCode=x.deliveryCur.Code,
                //DeliveryCurrentName=x.deliveryCur.Name,
                //DeliveryCurrentCountry=x.deliveryCur.Country,
                //DeliveryCurrentCounty=x.deliveryCur.County,
                //DeliveryCurrentAddress=x.deliveryCur.Address,
                Price=x.item.Price,
                TaxType=x.item.TaxType.toName(),
                ItemDiscountRate=x.item.ItemDiscountRate,
                NetTotal=x.net,
                TaxTotal=x.kdv,
                GeneralTotal=x.net+x.kdv,
                IsActive=x.item.IsActive,
                IsSendedShippingOrder=x.item.IsSendedShippingOrder,
            }).ToList();

            return Ok(saleItems);
        }
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetItemById(string id)
        {
            var saleItem = await _saleItemRepository.GetByIdAsync(id);
            var newSaleItem = new SaleItemDto
            {
                Id = saleItem.Id.ToString(),
                ParentId = saleItem.ParentId.ToString(),
                Quantity = saleItem.Quantity,
                MaterialUnitId = saleItem.Material.UnitId.ToString(),
                MaterialUnitName = saleItem.Material.Unit.Name,
                MaterialId = saleItem.MaterialId.ToString(),
                MaterialCode = saleItem.Material.Code,
                MaterialName = saleItem.Material.Name,

            };

            return Ok(newSaleItem);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddItems(SaleItemDto[] items)
        {
            List<SaleItem> saleItems = new List<SaleItem>();

            foreach (var item in items)
            {
                var saleItem = new SaleItem
                {
                    ParentId=Guid.Parse(item.ParentId),
                    MaterialId = Guid.Parse(item.MaterialId),
                    Quantity=Convert.ToDecimal( item.Quantity),
                    Price=Convert.ToDecimal( item.Price ),
                    TaxType=item.TaxType.GetEnum<TaxType>(),
                    ItemDiscountRate=item.ItemDiscountRate,
                    IsActive=item.IsActive,
                    IsSendedShippingOrder=item.IsSendedShippingOrder,
                };
                //countItems.Add(countItem);
                await _saleItemRepository.AddAsync(saleItem);
                item.Id=saleItem.Id.ToString();
                var rezervation= await _rezervationService.GetSingle(item.Id);
                item.RezervedQuantity = rezervation?.RezervationQuantity;
            }

            await _saleItemRepository.SaveAsync();

            //return StatusCode((int)HttpStatusCode.Created);
            return Ok(items);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> EditItems(SaleItemDto[] items)
        {
            List<SaleItem> saleItems = new List<SaleItem>();

            foreach (var item in items)
            {
                var saleItem = new SaleItem
                {
                    Id = Guid.Parse(item.Id),
                    ParentId = Guid.Parse(item.ParentId),
                    MaterialId = Guid.Parse(item.MaterialId),
                    Quantity = Convert.ToDecimal(item.Quantity),
                    Price = Convert.ToDecimal(item.Price),
                    TaxType = item.TaxType.GetEnum<TaxType>(),
                    ItemDiscountRate = item.ItemDiscountRate,
                    IsActive = item.IsActive,
                    IsSendedShippingOrder = item.IsSendedShippingOrder,
                };
                saleItems.Add(saleItem);
            }
             _saleItemRepository.UpdateRange(saleItems);

            await _saleItemRepository.SaveAsync();

            //return StatusCode((int)HttpStatusCode.Created);
            return Ok(items);
        }
        [HttpDelete("[action]/{Id}")]
        [Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> DeleteItem(string id)
        {
            await _saleItemRepository.RemoveAsync(id);
            await _saleItemRepository.SaveAsync();
            return Ok();
        }
    }
}
