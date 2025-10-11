using YayinEviApi.Domain.Entities.Common;
using YayinEviApi.Domain.Entities.CurrentE;
using YayinEviApi.Domain.Entities.GoodsAccepE;
using YayinEviApi.Domain.Entities.WarehouseE;

namespace YayinEviApi.Domain.Entities.ShipE
{
    public class Ship : BaseEntity
    {
        public string? DocumentCode { get; set; }
        public DateTime DocumentDate { get; set; }
        public Guid ShipCellofWarehouseId { get; set; }
        public Guid CurrentId { get; set; }
        public string? CurrentName { get; set; }
        public string? Address { get; set; }
        public string? Country { get; set; }
        public string? County { get; set; }
        public string? Town { get; set; }
        public string CreatingUserId { get; set; }
        public string? UpdatingUserId { get; set; }

        public CellofWarehouse ShipCellofWarehouse { get; set; }
        public Current Current { get; set; }
        public ICollection<ShipItem>? ShipItemList { get; set; }


    }
}
