using YayinEviApi.Domain.Entities.Common;
using YayinEviApi.Domain.Entities.MaterialE;
using YayinEviApi.Domain.Entities.UnitE;

namespace YayinEviApi.Domain.Entities.WarehouseE
{
    public class StockMovement:BaseEntity
    {
        public Guid? EnteringCellId { get; set; }
        public Guid? OutgoingCellId { get; set; }
        public Guid MaterialId { get; set; }
        public Guid? UnitId { get; set; }
        public decimal MovementQuantity { get; set; }
        public string CreatingUserId { get; set; }
        public string MovementClass { get; set; }
        public string MovementClassId { get; set; }
        public string? MovementClassCode { get; set; }

        public Material Material { get; set; }
        public MaterialUnit Unit { get; set; }
        public CellofWarehouse EnteringCell { get; set; }
        public CellofWarehouse OutgoingCell { get; set; }
    }
}
