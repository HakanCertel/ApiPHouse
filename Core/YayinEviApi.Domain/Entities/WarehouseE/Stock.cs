using YayinEviApi.Domain.Entities.Common;
using YayinEviApi.Domain.Entities.MaterialE;
using YayinEviApi.Domain.Entities.UnitE;

namespace YayinEviApi.Domain.Entities.WarehouseE
{
    public class Stock:BaseEntity
    {
        public Guid CellofWarehouseId { get; set; }
        public Guid MaterialId { get; set; }
        public Guid? UnitId { get; set; }
        public decimal Quantity { get; set; }

        public Material Material { get; set; }
        public CellofWarehouse CellofWarehouse { get; set; }
        public MaterialUnit Unit { get; set; }

    }
}
