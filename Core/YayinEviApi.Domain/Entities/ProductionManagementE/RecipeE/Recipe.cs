using YayinEviApi.Domain.Entities.Common;
using YayinEviApi.Domain.Entities.MaterialE;
using YayinEviApi.Domain.Entities.UnitE;
using YayinEviApi.Domain.Entities.WarehouseE;

namespace YayinEviApi.Domain.Entities.ProductionManagementE.RecipeE
{
    public class Recipe:BaseEntity
    {
        public string Name { get; set; }
        public Guid MaterialId { get; set; }
        public Guid WarehouseCellId { get; set; }
        public Guid UnitId { get; set; }
        public decimal Quantity { get; set; } = 0;
        public decimal RateofOutage { get; set; } = 0;
        public string? Description { get; set; }
        public bool Default { get; set; }

        public Material Material{ get; set; }
        public CellofWarehouse WarehouseCell { get; set; }
        public MaterialUnit Unit { get; set; }
        public ICollection<RecipeItems> RecipeItems { get; set; }
    }
}
