using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using System.Net;
using YayinEviApi.Application.Abstractions.Services;
using YayinEviApi.Application.DTOs.RezervationDtos;
using YayinEviApi.Application.DTOs.User;
using YayinEviApi.Application.DTOs.WarehouseDtos;
using YayinEviApi.Application.Repositories.IRezervationR;
using YayinEviApi.Application.Repositories.IWarehouseR;
using YayinEviApi.Domain.Entities.RezervationE;

namespace YayinEviApi.API.Controllers.RezervationControllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")]

    public class RezervationController : ControllerBase
    {
        private IUserService _userService;
        readonly CreateUser _user;
        readonly IRezervationRepository _rezervationRepository;
        readonly IStockRepository _stockRepository;
        Expression<Func<StockDto, bool>>? _filterExpressionStock;
        public RezervationController(IUserService userService, IRezervationRepository rezervationRepository, IStockRepository stockRepository)
        {
            _userService = userService;
            _rezervationRepository = rezervationRepository;
            _user = _userService.GetUser().Result;
            _stockRepository = stockRepository;
        }

        [HttpGet()]
        public async Task<IActionResult> GetAll([FromQuery ]bool cancel)
        {
            var rez = _rezervationRepository.Table.Select(x=>new
            {
                rez=x,
                cUserName = _userService.GetUser(x.CreatingUserId).Result.NameSurname,
                uUserName = x.UpdatingUserId != null ? _userService.GetUser(x.UpdatingUserId).Result.NameSurname : null,
                unit=x.Material.Unit!=null?x.Material.Unit.Name:null
            }).Select(x => new RezervationDto
            {
                Id = x.rez.Id.ToString(),
                CellofWarehouseId=x.rez.CellofWarehouseId.ToString(),
                CellofWarehouseName=x.rez.CellofWarehouse.Name,
                WarehouseName=x.rez.CellofWarehouse.ShelfofWarehouse.HallofWarehouse.Warehouse.Name,
                ChildCode=x.rez.ChildCode,
                ChildId=x.rez.ChildId.ToString(),
                CreatingUserId=x.rez.CreatingUserId,
                CreatingUserName=x.cUserName,
                IsCancel=x.rez.IsCancel,
                MaterialId=x.rez.MaterialId.ToString(),
                MaterialName=x.rez.Material.Name,
                UnitName=x.unit,
                ParentCode=x.rez.ParentCode,
                ParentId=x.rez.ParentId.ToString(),
                RezervationQuantity=x.rez.RezervationQuantity,
                RezervationState=x.rez.RezervationState,
                RezervationStatu=x.rez.RezervationStatu,
                UpdatingUserId=x.rez.UpdatingUserId,
                UpdatingUserName=x.uUserName,
            }).ToList();
            return Ok(rez);
        }
        
        [HttpPost()]
        public async Task<IActionResult> Add(RezervationDto[] rezList)
        {
            List<Rezervation> rList = new List<Rezervation>();

            foreach (var rez in rezList)
            {
                var stocks = _stockRepository.Select(x => x.MaterialId.ToString() == rez.MaterialId, x => new
                {
                    cellId = x.CellofWarehouseId,
                    rezervationQ = x.Material.Rezervations.Where(y => y.CellofWarehouseId == x.CellofWarehouseId && y.MaterialId == x.MaterialId).Sum(y => y.RezervationQuantity),
                    stockQ = x.Quantity
                }).Select(x=>new StockDto
                {
                    CellofWarehouseId=x.cellId.ToString(),
                    RezervationQuantity=x.rezervationQ,
                    UsableQuantity=x.stockQ-x.rezervationQ
                }).Where(x=>x.UsableQuantity>0).ToList();
                
                if (!stocks.Any()) { continue; }
               
                decimal addedQuantity=0 ;
                  
                while (stocks.Count>0) {
                        
                    var firststockQ = stocks.Max(x=>x.UsableQuantity);
                    var firstStock =stocks.Where(x => x.UsableQuantity == firststockQ).FirstOrDefault();
                    stocks.Remove(firstStock);

                    var difference = rez.RezervationQuantity - addedQuantity;

                    if (difference > firststockQ)
                    {
                        addedQuantity = addedQuantity + Convert.ToDecimal(firststockQ);
                        var _rez = new Rezervation
                        {
                            CellofWarehouseId = Guid.Parse(firstStock.CellofWarehouseId),
                            ChildCode = rez.ChildCode,
                            ChildId = Guid.Parse(rez.ChildId),
                            CreatingUserId = _user.UserId,
                            IsCancel = rez.IsCancel,
                            MaterialId = Guid.Parse(rez.MaterialId),
                            ParentCode = rez.ParentCode,
                            ParentId = Guid.Parse(rez.ParentId),
                            RezervationQuantity = Convert.ToDecimal(firststockQ),
                            RezervationState = rez.RezervationState,
                            RezervationStatu = rez.RezervationStatu,
                            UpdatingUserId = rez.UpdatingUserId,
                        };
                        rList.Add(_rez);
                    }
                    else
                    {
                        addedQuantity = addedQuantity + Convert.ToDecimal(difference);
                        var _rez = new Rezervation
                        {
                            CellofWarehouseId = Guid.Parse(firstStock.CellofWarehouseId),
                            ChildCode = rez.ChildCode,
                            ChildId = Guid.Parse(rez.ChildId),
                            CreatingUserId = _user.UserId,
                            IsCancel = rez.IsCancel,
                            MaterialId = Guid.Parse(rez.MaterialId),
                            ParentCode = rez.ParentCode,
                            ParentId = Guid.Parse(rez.ParentId),
                            RezervationQuantity = Convert.ToDecimal(difference),
                            RezervationState = rez.RezervationState,
                            RezervationStatu = rez.RezervationStatu,
                            UpdatingUserId = rez.UpdatingUserId,
                        };
                        rList.Add(_rez);
                        break;
                    }
                        
                }
                
            }
            await _rezervationRepository.AddRangeAsync(rList);

            await _rezervationRepository.SaveAsync();

            return StatusCode((int)HttpStatusCode.Created);
        }
        [HttpPut()]
        public async Task<IActionResult> Edit(RezervationDto[] rezList)
        {
            List<Rezervation> rList = new List<Rezervation>();

            foreach (var rez in rezList)
            {
                var _rezSingle = _rezervationRepository.GetSingleAsync(x => x.Id.ToString() == rez.Id).Result;

                if (_rezSingle.RezervationQuantity > rez.RezervationQuantity)
                {
                    var _rez = new Rezervation
                    {
                        CellofWarehouseId = Guid.Parse(rez.CellofWarehouseId),
                        ChildCode = rez.ChildCode,
                        ChildId = Guid.Parse(rez.ChildId),
                        CreatingUserId = _user.UserId,
                        IsCancel = rez.IsCancel,
                        MaterialId = Guid.Parse(rez.MaterialId),
                        ParentCode = rez.ParentCode,
                        ParentId = Guid.Parse(rez.ParentId),
                        RezervationQuantity = Convert.ToDecimal(rez.RezervationQuantity),
                        RezervationState = rez.RezervationState,
                        RezervationStatu = rez.RezervationStatu,
                        UpdatingUserId = rez.UpdatingUserId,
                    };
                    rList.Add(_rez);
                }
                else if (_rezSingle.RezervationQuantity < rez.RezervationQuantity)
                {
                    var stocks = _stockRepository.Select(x => x.MaterialId.ToString() == rez.MaterialId, x => new
                    {
                        cellId = x.CellofWarehouseId,
                        rezervationQ = x.Material.Rezervations.Where(y => y.CellofWarehouseId == x.CellofWarehouseId && y.MaterialId == x.MaterialId).Sum(y => y.RezervationQuantity),
                        stockQ = x.Quantity
                    }).Select(x => new StockDto
                    {
                        CellofWarehouseId = x.cellId.ToString(),
                        RezervationQuantity = x.rezervationQ,
                        UsableQuantity = x.stockQ - x.rezervationQ
                    }).Where(x => x.UsableQuantity > 0).ToList();

                    if (!stocks.Any()) { continue; }

                    decimal addedQuantity = _rezSingle.RezervationQuantity;

                    while (stocks.Count > 0)
                    {

                        var firststockQ = stocks.Max(x => x.UsableQuantity);
                        var firstStock = stocks.Where(x => x.UsableQuantity == firststockQ).FirstOrDefault();
                        stocks.Remove(firstStock);

                        var difference = rez.RezervationQuantity - addedQuantity;

                        if (difference > firststockQ)
                        {
                            addedQuantity = addedQuantity + Convert.ToDecimal(firststockQ);
                            var _rez = new Rezervation
                            {
                                CellofWarehouseId = Guid.Parse(firstStock.CellofWarehouseId),
                                ChildCode = rez.ChildCode,
                                ChildId = Guid.Parse(rez.ChildId),
                                CreatingUserId = _user.UserId,
                                IsCancel = rez.IsCancel,
                                MaterialId = Guid.Parse(rez.MaterialId),
                                ParentCode = rez.ParentCode,
                                ParentId = Guid.Parse(rez.ParentId),
                                RezervationQuantity = Convert.ToDecimal(firststockQ),
                                RezervationState = rez.RezervationState,
                                RezervationStatu = rez.RezervationStatu,
                                UpdatingUserId = rez.UpdatingUserId,
                            };
                            rList.Add(_rez);
                        }
                        else
                        {
                            addedQuantity = addedQuantity + Convert.ToDecimal(difference);
                            var _rez = new Rezervation
                            {
                                CellofWarehouseId = Guid.Parse(firstStock.CellofWarehouseId),
                                ChildCode = rez.ChildCode,
                                ChildId = Guid.Parse(rez.ChildId),
                                CreatingUserId = _user.UserId,
                                IsCancel = rez.IsCancel,
                                MaterialId = Guid.Parse(rez.MaterialId),
                                ParentCode = rez.ParentCode,
                                ParentId = Guid.Parse(rez.ParentId),
                                RezervationQuantity = Convert.ToDecimal(difference),
                                RezervationState = rez.RezervationState,
                                RezervationStatu = rez.RezervationStatu,
                                UpdatingUserId = rez.UpdatingUserId,
                            };
                            rList.Add(_rez);
                            break;
                        }

                    }

                }
            }

            _rezervationRepository.UpdateRange(rList);

            await _rezervationRepository.SaveAsync();

            return StatusCode((int)HttpStatusCode.Created);
        }
        [HttpPut("[action]")]
        public async Task<IActionResult> Cancel(RezervationDto rez)
        {
            var _rez = new Rezervation
            {
                Id = Guid.Parse(rez.Id),
                CellofWarehouseId = Guid.Parse(rez.CellofWarehouseId),
                ChildCode = rez.ChildCode,
                ChildId = Guid.Parse(rez.ChildId),
                CreatingUserId = rez.CreatingUserId,
                IsCancel = true,
                MaterialId = Guid.Parse(rez.MaterialId),
                ParentCode = rez.ParentCode,
                ParentId = Guid.Parse(rez.ParentId),
                RezervationQuantity = Convert.ToDecimal(rez.RezervationQuantity),
                RezervationState = rez.RezervationState,
                RezervationStatu = rez.RezervationStatu,
                UpdatingUserId = rez.UpdatingUserId,
            };
            _rezervationRepository.Update(_rez);
            await _rezervationRepository.SaveAsync();
            return Ok();
        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _rezervationRepository.RemoveAsync(id);
            await _rezervationRepository.SaveAsync();
            return Ok();
        }
    }
}
