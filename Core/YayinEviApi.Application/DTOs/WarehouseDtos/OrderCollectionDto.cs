using YayinEviApi.Domain.Entities.WarehouseE;

namespace YayinEviApi.Application.DTOs.WarehouseDtos
{
    public class OrderCollectionDto
    {
        public string? Id { get; set; }
        public string? Code { get; set; }
        public string? CreatingUserId { get; set; }
        public string? CreatingUserName { get; set; }
        public string? UpdatingUserId { get; set; }
        public string? UpdatingUserName { get; set; }
        public bool IsCollectionCompleted { get; set; }
        public bool IsActive { get; set; }
        public ICollection<OrderCollectionItem>? OrderCollectionItems { get; set; }
    }
}
