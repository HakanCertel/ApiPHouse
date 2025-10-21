using System.ComponentModel.DataAnnotations.Schema;
using YayinEviApi.Domain.Entities.Common;
using YayinEviApi.Domain.Entities.GoodsAccepE;
using YayinEviApi.Domain.Entities.WarehouseE;

namespace YayinEviApi.Domain.Entities.GoodsAcceptE
{
    public class GoodsAccep : BaseEntity
    {
        public string? DocumentCode { get; set; }
        public DateTime DocumentDate { get; set; }
        public Guid AcceptCellofWarehouseId { get; set; }
        public Guid? CurrentId { get; set; }
        public string? CurrentName { get; set; }
        public string? Address { get; set; }
        public string? Country { get; set; }
        public string? County { get; set; }
        public string? Town { get; set; }
        public string CreatingUserId { get; set; }
        public string? UpdatingUserId { get; set; }

        public CellofWarehouse AcceptCellofWarehouse { get; set; }
        public ICollection<GoodsAcceptItems>? GoodsAcceptItemList { get; set; }

    }
}
