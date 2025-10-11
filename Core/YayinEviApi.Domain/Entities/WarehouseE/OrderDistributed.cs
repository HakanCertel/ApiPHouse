using YayinEviApi.Domain.Entities.Common;

namespace YayinEviApi.Domain.Entities.WarehouseE
{
    public class OrderDistributed:BaseEntity
    {
        public string Code { get; set; }
        public string CreatingUserId { get; set; }

        public ICollection<OrderItemDistributed> OrderItemsDistributed { get; set; }

    }
}
