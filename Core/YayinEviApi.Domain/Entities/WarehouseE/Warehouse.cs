using YayinEviApi.Domain.Entities.Common;

namespace YayinEviApi.Domain.Entities.WarehouseE
{
    public class Warehouse:BaseEntity
    {
        public string? Code { get; set; }
        public string? Name { get; set; }
        public bool IsGoodsAcceptWareHouse { get; set; }
        public bool IsShippingWareHouse { get; set; }

        public ICollection<HallofWarehouse> HallofWarehouses { get; set; }
    }
}
