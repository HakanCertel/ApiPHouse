using YayinEviApi.Domain.Entities.Common;

namespace YayinEviApi.Domain.Entities.WarehouseE
{
    public class HallofWarehouse:BaseEntity
    {
        public Guid WarehouseId { get; set; }
        public string Name { get; set; }
        public string? Code { get; set; }

        public Warehouse Warehouse { get; set; }
        public ICollection<HallofWarehouse> HallofWarehouses { get; set; }
    }
}
