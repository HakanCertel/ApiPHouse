using YayinEviApi.Domain.Entities.Common;
using YayinEviApi.Domain.Entities.RezervationE;
using YayinEviApi.Domain.Entities.UnitE;
using YayinEviApi.Domain.Entities.WarehouseE;
using YayinEviApi.Domain.Enum;

namespace YayinEviApi.Domain.Entities.MaterialE
{
    public class Material:BaseEntity
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public MaterialTypes? MaterialType { get; set; } = MaterialTypes.product;
        public Guid? WarehouseId { get; set; }
        public Guid? HallofWarehouseId { get; set; }
        public Guid? ShelfofWarehouseId { get; set; }
        public Guid? CellofWarehouseId { get; set; }
        public Guid? UnitId { get; set; }

        public Warehouse? Warehouse { get; set; }
        public HallofWarehouse? HallofWarehouse { get; set; }
        public ShelfofWarehouse? ShelfofWarehouse { get; set; }
        public CellofWarehouse? CellofWarehouse { get; set; }
        public MaterialUnit? Unit { get; set; }


        public ICollection<MaterialFile> MaterialFiles { get; set; }
        public ICollection<Rezervation> Rezervations { get; set; }
        public ICollection<Stock> Stocks { get; set; }
    }
}
