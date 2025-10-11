using YayinEviApi.Application.Abstractions.Services;
using YayinEviApi.Application.DTOs.RezervationDtos;
using YayinEviApi.Application.Repositories.IRezervationR;
using YayinEviApi.Application.Services;

namespace YayinEviApi.Persistence.Services
{
    public class RezervationService : IRezervationService
    {
        readonly IRezervationRepository _rezervationRepository;
        private IUserService _userService;

        public RezervationService(IRezervationRepository rezervationRepository, IUserService userService)
        {
            _rezervationRepository = rezervationRepository;
            _userService = userService;
        }

        public async Task<RezervationDto>? GetSingle(string id)
        {
            
            var rez = _rezervationRepository.Table.Where(x=>x.ChildId.ToString()==id).Select(x => new
            {
                rez = x,
                cUserName = _userService.GetUser(x.CreatingUserId).Result.NameSurname,
                uUserName = x.UpdatingUserId != null ? _userService.GetUser(x.UpdatingUserId).Result.NameSurname : null,
                unit = x.Material.Unit != null ? x.Material.Unit.Name : null
            }).Select(x => new RezervationDto
            {
                Id = x.rez.Id.ToString(),
                CellofWarehouseId = x.rez.CellofWarehouseId.ToString(),
                CellofWarehouseName = x.rez.CellofWarehouse.Name,
                ShelfofWarehouseName= x.rez.CellofWarehouse.ShelfofWarehouse.Name,
                HallofWarehouseName=x.rez.CellofWarehouse.ShelfofWarehouse.HallofWarehouse.Name,
                WarehouseName = x.rez.CellofWarehouse.ShelfofWarehouse.HallofWarehouse.Warehouse.Name,
                ChildCode = x.rez.ChildCode,
                ChildId = x.rez.ChildId.ToString(),
                CreatingUserId = x.rez.CreatingUserId,
                CreatingUserName = x.cUserName,
                IsCancel = x.rez.IsCancel,
                MaterialId = x.rez.MaterialId.ToString(),
                MaterialName = x.rez.Material.Name,
                UnitName = x.unit,
                ParentCode = x.rez.ParentCode,
                ParentId = x.rez.ParentId.ToString(),
                RezervationQuantity = x.rez.RezervationQuantity,
                RezervationState = x.rez.RezervationState,
                RezervationStatu = x.rez.RezervationStatu,
                UpdatingUserId = x.rez.UpdatingUserId,
                UpdatingUserName = x.uUserName,
            }).FirstOrDefault();
            return rez;
        }
        public async Task<List<RezervationDto>> GertAll()
        {
            var rez = _rezervationRepository.Table.Select(x => new
            {
                rez = x,
                cUserName = _userService.GetUser(x.CreatingUserId).Result.NameSurname,
                uUserName = x.UpdatingUserId != null ? _userService.GetUser(x.UpdatingUserId).Result.NameSurname : null,
                unit = x.Material.Unit != null ? x.Material.Unit.Name : null
            }).Select(x => new RezervationDto
            {
                Id = x.rez.Id.ToString(),
                CellofWarehouseId = x.rez.CellofWarehouseId.ToString(),
                CellofWarehouseName = x.rez.CellofWarehouse.Name,
                WarehouseName = x.rez.CellofWarehouse.ShelfofWarehouse.HallofWarehouse.Warehouse.Name,
                ChildCode = x.rez.ChildCode,
                ChildId = x.rez.ChildId.ToString(),
                CreatingUserId = x.rez.CreatingUserId,
                CreatingUserName = x.cUserName,
                IsCancel = x.rez.IsCancel,
                MaterialId = x.rez.MaterialId.ToString(),
                MaterialName = x.rez.Material.Name,
                UnitName = x.unit,
                ParentCode = x.rez.ParentCode,
                ParentId = x.rez.ParentId.ToString(),
                RezervationQuantity = x.rez.RezervationQuantity,
                RezervationState = x.rez.RezervationState,
                RezervationStatu = x.rez.RezervationStatu,
                UpdatingUserId = x.rez.UpdatingUserId,
                UpdatingUserName = x.uUserName,
            }).ToList();

            return rez;
        }

    }
}
