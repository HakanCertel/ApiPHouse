using YayinEviApi.Domain.Entities.Common;

namespace YayinEviApi.Domain.Entities.WarehouseE
{
    public class OrderCollection:BaseEntity
    {
        public string? Code { get; set; }
        public string CreatingUserId { get; set; }
        public string? UpdatingUserId { get; set; }
        public bool IsCollectionCompleted { get; set; }

        public ICollection<OrderCollectionItem> OrderCollectionItems { get; set; }
    }
}
