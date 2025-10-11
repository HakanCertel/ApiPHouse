using YayinEviApi.Domain.Entities.Common;

namespace YayinEviApi.Domain.Entities.WarehouseE
{
    public class StockCount:BaseEntity
    {
        public string DocumentCode { get; set; }
        public DateTime DocumentDate { get; set; }
        public bool IsConfirmed { get; set; }
        public DateTime? ConfirmDate { get; set; }
        public string CreatingUserId { get; set; }
        public string? UpdatingUserId { get; set; }
       
    }
}
