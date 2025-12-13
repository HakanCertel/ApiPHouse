using System.ComponentModel.DataAnnotations.Schema;
using YayinEviApi.Domain.Entities.Common;
using YayinEviApi.Domain.Entities.MaterialE;
using YayinEviApi.Domain.Entities.UnitE;
using YayinEviApi.Domain.Entities.WarehouseE;

namespace YayinEviApi.Domain.Entities.ProductionManagementE.RecipeE
{
    public class RecipeItems:BaseEntity
    {
        public Guid ParentId { get; set; }
        public Guid MaterialId { get; set; }
        public Guid? ConsumptionUnitId { get; set; }
        public Guid ConsumptionWarehouseCellId { get; set; }
        public decimal Quantity { get; set; }
        public decimal RateofOutage { get; set; } = 0;
        public string? Description { get; set; }

        public Recipe Parent { get; set; }
        public Material Material { get; set; }
        public CellofWarehouse ConsumptionWarehouseCell { get; set; }
        public MaterialUnit ConsumptionUnit { get; set; }
        [NotMapped]
        public override string? Code { get ; set; }
    }
}
