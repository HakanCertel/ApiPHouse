using System.ComponentModel.DataAnnotations.Schema;
using YayinEviApi.Domain.Entities.Common;
using YayinEviApi.Domain.Entities.MaterialE;
using YayinEviApi.Domain.Entities.UnitE;

namespace YayinEviApi.Domain.Entities.WarehouseE
{
    public class StockCountItems:BaseEntity
    {
        public Guid StockCountId { get; set; }
        public Guid MaterialId { get; set; }
        public Guid CellofWarehouseId { get; set; }
        public Guid? UnitId { get; set; }
        public decimal Quantity { get; set; }
        [NotMapped]
        public override bool IsActive { get ; set ; }
        public Material Material { get; set; }
        public CellofWarehouse CellofWarehouse { get; set; }
        public MaterialUnit Unit { get; set; }
        public StockCount StockCount { get; set; }
    }
}
