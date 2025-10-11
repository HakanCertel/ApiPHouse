using YayinEviApi.Domain.Entities.Common;

namespace YayinEviApi.Domain.Entities.WarehouseE
{
    public class CellofWarehouse:BaseEntity
    {
        public string? Code { get; set; }
        public string Name { get; set; }
        public bool IsDefaultCell { get; set; }
        public Guid? ShelfofWarehouseId { get; set; }

        public ShelfofWarehouse ShelfofWarehouse { get; set; }
    }
}
