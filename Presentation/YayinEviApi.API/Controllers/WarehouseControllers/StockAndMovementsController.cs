using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using YayinEviApi.Application.Abstractions.Services;
using YayinEviApi.Application.DTOs.User;
using YayinEviApi.Application.DTOs.WarehouseDtos;
using YayinEviApi.Application.Repositories.IRezervationR;
using YayinEviApi.Application.Repositories.IWarehouseR;
using YayinEviApi.Domain.Entities.WarehouseE;

namespace YayinEviApi.API.Controllers.WarehouseControllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")]

    public class StockAndMovementsController : ControllerBase
    {
        private IUserService _userService;
        readonly CreateUser _user;
        private IRezervationRepository _rezervationRepository;
        Expression<Func<StockMovement, bool>>? _filterExpressionStockMovement;
        Expression<Func<Stock, bool>>? _filterExpressionStock;

        readonly IStockRepository _stockRepository;
        readonly IStockMovementRepository _stockMovementRepository;

        public StockAndMovementsController(IUserService userService, IStockRepository stockRepository, IStockMovementRepository stockMovementRepository, IRezervationRepository rezervationRepository)
        {
            _userService = userService;
            _stockRepository = stockRepository;
            _stockMovementRepository = stockMovementRepository;
            _rezervationRepository = rezervationRepository;
            
            _user = _userService.GetUser().Result;
            _filterExpressionStock = x => x.Id != null;
        }

        [HttpGet("{materialId?}")]
        public async Task<IActionResult> GetAll(string? materialId, [FromQuery] string? warehouseId)
        {
            if(materialId!=null&&warehouseId==null)
                _filterExpressionStock =x => x.MaterialId.ToString() == materialId;
            else if(warehouseId!=null&&materialId==null)
                _filterExpressionStock=x=>x.CellofWarehouse.ShelfofWarehouse.HallofWarehouse.WarehouseId.ToString() == warehouseId;
            else if(materialId != null && warehouseId != null)
                _filterExpressionStock= x => x.MaterialId.ToString() == materialId&& x.CellofWarehouse.ShelfofWarehouse.HallofWarehouse.WarehouseId.ToString() == warehouseId;

            var stockList = _filterExpressionStock == null ? _stockRepository.Table : _stockRepository.Table.Where(_filterExpressionStock);

            var stocks = stockList.Select(x => new
            {
                stock = x,
                material=x.Material,
                unit=x.Unit,
                cell=x.CellofWarehouse,
                shelf=x.CellofWarehouse.ShelfofWarehouse,
                hall=x.CellofWarehouse.ShelfofWarehouse.HallofWarehouse,
                warehouse=x.CellofWarehouse.ShelfofWarehouse.HallofWarehouse.Warehouse,
                rezervation=x.Material.Rezervations.Where(y=>y.MaterialId==x.MaterialId&&y.CellofWarehouseId==x.CellofWarehouseId).Sum(y=>y.RezervationQuantity)
            }).Select(x => new StockDto
            {
                Id = x.stock.Id.ToString(),
                Quantity = x.stock.Quantity,
                RezervationQuantity=x.rezervation,
                UsableQuantity=x.stock.Quantity-x.rezervation,
                QuantityBeforeUpdated=x.stock.Quantity,
                UnitId = x.material.UnitId.ToString(),
                UnitCode = x.material.Unit.Code,
                UnitName = x.material.Unit.Name,
                CellofWarehouseId = x.stock.CellofWarehouseId.ToString(),
                CellofWarehouseCode = x.cell.Code,
                CellofWarehouseName = x.cell.Name,
                ShelfofWarehouseId = x.shelf.Id.ToString(),
                ShelfofWarehouseCode = x.shelf.Code,
                ShelfofWarehouseName = x.shelf.Name,
                HallofWarehouseId = x.hall.Id.ToString(),
                HallofWarehouseCode = x.hall.Code,
                HallofWarehouseName = x.hall.Name,
                WarehouseId = x.warehouse.Id.ToString(),
                WarehouseCode = x.warehouse.Code,
                WarehouseName = x.warehouse.Name,
                MaterialId = x.material.Id.ToString(),
                MaterialCode = x.material.Code,
                MaterialName = x.material.Name,
            }).ToList();

            return Ok(stocks);
        }
        
        [HttpPost()]
        public async Task<IActionResult> AddItems(StockDto[] items)
        {
            List<Stock> hasStocks = new List<Stock>();
            List<Stock> newStocks = new List<Stock>();

            foreach (var item in items)
            {
                Stock? hasStock = _stockRepository.GetSingleAsync(x => x.MaterialId.ToString() == item.MaterialId&&x.CellofWarehouseId.ToString()==item.CellofWarehouseId)?.Result;
                
                if (hasStock == null) { 
                    var stock = new Stock
                    {
                        MaterialId = Guid.Parse(item.MaterialId),
                        //UnitId = item.UnitId != null ? Guid.Parse(item.UnitId) : null,
                        Quantity = Convert.ToDecimal(item.Quantity),
                        CellofWarehouseId = Guid.Parse(item.CellofWarehouseId),
                    };
                    newStocks.Add(stock);
                }
                else
                {
                    hasStock.Quantity = hasStock.Quantity + Convert.ToDecimal(item.Quantity);
                    hasStocks.Add(hasStock);
                }
            }
            if(hasStocks.Count > 0)
            {
                _stockRepository.UpdateRange(hasStocks);
            }
            if(newStocks.Count > 0)
            {
                await _stockRepository.AddRangeAsync(newStocks);
            }

            await _stockRepository.SaveAsync();

            hasStocks.AddRange(newStocks);

            return Ok(hasStocks);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> OutgoingItems(StockDto[] items)
        {
            List<Stock> hasStocks = new List<Stock>();
            List<Stock> newStocks = new List<Stock>();

            foreach (var item in items)
            {
                Stock? hasStock = _stockRepository.GetSingleAsync(x => x.MaterialId.ToString() == item.MaterialId && x.CellofWarehouseId.ToString() == item.CellofWarehouseId)?.Result;

                if (hasStock == null)
                {
                    var stock = new Stock
                    {
                        MaterialId = Guid.Parse(item.MaterialId),
                        //UnitId = item.UnitId != null ? Guid.Parse(item.UnitId) : null,
                        Quantity = Convert.ToDecimal(-item.Quantity),
                        CellofWarehouseId = Guid.Parse(item.CellofWarehouseId),
                    };
                    newStocks.Add(stock);
                }
                else
                {
                    hasStock.Quantity = hasStock.Quantity - Convert.ToDecimal(item.Quantity);
                    hasStocks.Add(hasStock);
                }
            }
            if (hasStocks.Count > 0)
            {
                _stockRepository.UpdateRange(hasStocks);
            }
            if (newStocks.Count > 0)
            {
                await _stockRepository.AddRangeAsync(newStocks);
            }

            await _stockRepository.SaveAsync();

            hasStocks.AddRange(newStocks);

            return Ok(hasStocks);
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateEntiringStock(StockDto[] items)
        {
            if (items.Length > 0)
            {
                foreach (var item in items)
                {
                    var movementItem= await _stockMovementRepository.GetSingleAsync(x => x.MovementClassItemId == item.MovmentClassItemId);
                    if (movementItem == null)
                        return StatusCode(StatusCodes.Status404NotFound);
                    var stock = _stockRepository.GetSingleAsync(x => x.Id.ToString() == item.Id).Result;
                    item.Quantity= stock.Quantity+(stock.Quantity- movementItem.MovementQuantity);
                    _stockRepository.Update(stock);
                }
              
                await _stockRepository.SaveAsync();

                return StatusCode(StatusCodes.Status200OK);
            }

            return StatusCode(StatusCodes.Status304NotModified);

        }

        [HttpGet("[action]/{materialId?}")]
        public async Task<IActionResult> GetAllMovements(string? materialId)
        {
            _filterExpressionStockMovement = materialId == null ? null : x => x.MaterialId.ToString() == materialId;
            var stockMovementsList = _filterExpressionStockMovement == null ? _stockMovementRepository.Table : _stockMovementRepository.Table.Where(_filterExpressionStockMovement);

            var stocksMovements = stockMovementsList.Select(x => new
            {
                stockMovment = x,
                material = x.Material,
                unit = x.Unit,
                enteringCell = x.EnteringCell,
                enteringShelf = x.EnteringCell.ShelfofWarehouse,
                enteringHall = x.EnteringCell.ShelfofWarehouse.HallofWarehouse,
                enteringWarehouse = x.EnteringCell.ShelfofWarehouse.HallofWarehouse.Warehouse,
                outgoingCell = x.OutgoingCell,
                outgoingShelf =x.OutgoingCell!=null? x.OutgoingCell.ShelfofWarehouse:null,
                outgoingHall = x.OutgoingCell != null ? x.OutgoingCell.ShelfofWarehouse.HallofWarehouse:null,
                outgoingWarehouse = x.OutgoingCell != null ? x.OutgoingCell.ShelfofWarehouse.HallofWarehouse.Warehouse:null,
                userNameSurname=_userService.GetUser(x.CreatingUserId).Result.NameSurname,
            }).Select(x => new StockMovementDto
            {
                Id = x.stockMovment.Id.ToString(),
                UnitId = x.material.Unit.Id.ToString(),
                UnitCode = x.material.Unit.Code,
                UnitName = x.material.Unit.Name,
                MaterialId = x.material.Id.ToString(),
                MaterialCode = x.material.Code,
                MaterialName = x.material.Name,
                CreatingUserId=x.stockMovment.CreatingUserId,
                CreatingUserNameSurname=x.userNameSurname,
                MovementClass=x.stockMovment.MovementClass,
                MovementClassCode=x.stockMovment.MovementClassCode,
                MovementClassId=x.stockMovment.MovementClassId,
                MovementClassItemId=x.stockMovment.MovementClassItemId,
                MovementQuantity=x.stockMovment.MovementQuantity,
                MovementDate=x.stockMovment.CreatedDate,
                OutgoingCellofWarehouseId =x.outgoingCell!=null? x.stockMovment.OutgoingCellId.ToString():null,
                OutgoingCellofWarehouseCode = x.outgoingCell != null? x.outgoingCell.Code:null,
                OutgoingCellofWarehouseName = x.outgoingCell != null ? x.outgoingCell.Name:null,
                OutgoingShelfofWarehouseId = x.outgoingCell != null ? x.outgoingShelf.Id.ToString():null,
                OutgoingShelfofWarehouseCode = x.outgoingCell != null ? x.outgoingShelf.Code:null,
                OutgoingShelfofWarehouseName = x.outgoingCell != null ? x.outgoingShelf.Name:null,
                OutgoingHallofWarehouseId = x.outgoingCell != null ? x.outgoingHall.Id.ToString():null,
                OutgoingHallofWarehouseCode = x.outgoingCell != null ? x.outgoingHall.Code:null,
                OutgoingHallofWarehouseName = x.outgoingCell != null ? x.outgoingHall.Name:null,
                OutgoingWarehouseId = x.outgoingCell != null ? x.outgoingWarehouse.Id.ToString():null,
                OutgoingWarehouseCode = x.outgoingCell != null ? x.outgoingWarehouse.Code:null,
                OutgoingWarehouseName = x.outgoingCell != null ? x.outgoingWarehouse.Name:null,
                EnteringCellofWarehouseId = x.stockMovment.EnteringCellId.ToString(),
                EnteringCellofWarehouseCode = x.enteringCell.Code,
                EnteringCellofWarehouseName = x.enteringCell.Name,
                EnteringShelfofWarehouseId = x.enteringShelf.Id.ToString(),
                EnteringShelfofWarehouseCode = x.enteringShelf.Code,
                EnteringShelfofWarehouseName = x.enteringShelf.Name,
                EnteringHallofWarehouseId = x.enteringHall.Id.ToString(),
                EnteringHallofWarehouseCode = x.enteringHall.Code,
                EnteringHallofWarehouseName = x.enteringHall.Name,
                EnteringWarehouseId = x.enteringWarehouse.Id.ToString(),
                EnteringWarehouseCode = x.enteringWarehouse.Code,
                EnteringWarehouseName = x.enteringWarehouse.Name,
            }).ToList();

            return Ok(stocksMovements);
        }


        [HttpGet("[action]")]
        public async Task<IActionResult> GetMovementsByClassId(string materialId,string classId)
        {
            var stockMov=_stockMovementRepository.Select(x=>x.MaterialId.ToString()==materialId && x.MovementClassId==classId,x=>x);

            return Ok(stockMov);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddMovementItems(StockMovementDto[] items)
        {
            List<StockMovement> stocks = new List<StockMovement>();

            foreach (var item in items)
            {
                var stock = new StockMovement
                {
                    MaterialId = Guid.Parse(item.MaterialId),
                    //UnitId = item.UnitId != null ? Guid.Parse(item.UnitId) : null,
                    MovementQuantity = Convert.ToDecimal(item.MovementQuantity),
                    EnteringCellId = item.EnteringCellofWarehouseId != null ? Guid.Parse(item.EnteringCellofWarehouseId):null,
                    OutgoingCellId =item.OutgoingCellofWarehouseId!=null? Guid.Parse(item.OutgoingCellofWarehouseId):null,
                    CreatingUserId=_user.UserId,
                    MovementClass=item.MovementClass,
                    MovementClassCode=item.MovementClassCode,
                    MovementClassId=item.MovementClassId,
                    MovementClassItemId=item.MovementClassItemId,
                };
                stocks.Add(stock);
            }
            await _stockMovementRepository.AddRangeAsync(stocks);

            await _stockMovementRepository.SaveAsync();

            return Ok(stocks);
        }
        [HttpPut()]
        public async Task<IActionResult> EditMovementItems(StockMovementDto[] items)
        {
            List<StockMovement> stocks = new List<StockMovement>();

            foreach (var item in items)
            {
                var stock = new StockMovement
                {
                    Id= Guid.Parse(item.Id),
                    MaterialId = Guid.Parse(item.MaterialId),
                    //UnitId =item.UnitId!=null? Guid.Parse(item.UnitId):null,
                    MovementQuantity = Convert.ToDecimal(item.MovementQuantity),
                    EnteringCellId = Guid.Parse(item.EnteringCellofWarehouseId),
                    OutgoingCellId = Guid.Parse(item.OutgoingCellofWarehouseId),
                    CreatingUserId = _user.UserId,
                    MovementClass = item.MovementClass,
                    MovementClassCode = item.MovementClassCode,
                    MovementClassId = item.MovementClassId,
                    UpdatedDate= DateTime.Now,
                };
                stocks.Add(stock);
            }
            await _stockMovementRepository.AddRangeAsync(stocks);

            await _stockMovementRepository.SaveAsync();

            return Ok(stocks);
        }
        
        [HttpDelete("[action]/{Id}")]
        [Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> DeleteMovementItem(string id)
        {
            await _stockMovementRepository.RemoveAsync(id);
            await _stockMovementRepository.SaveAsync();
            return Ok();
        }
    }
}
