using System.ComponentModel.DataAnnotations.Schema;
using YayinEviApi.Domain.Entities.Common;
using YayinEviApi.Domain.Entities.SalesE;
using YayinEviApi.Domain.Enum;

namespace YayinEviApi.Domain.Entities.WarehouseE
{
    public class ShippingOrder:BaseEntity
    {
        public Guid SaleItemId { get; set; }
        public Guid? SaleId { get; set; }
        public string? AssignedUserId { get; set; }
        public DateTime? FinishedDate { get; set; } = DateTime.Now;
        public string CreatingUserId { get; set; }
        public string? UpdatingUserId { get; set; }
        public bool IsStartedCollection { get; set; }
        public bool IsComplededCollection { get; set; }
        public bool IsStartedPacking { get; set; }
        public bool IsCompletedPacking { get; set; }
        public bool IsShipped { get; set; }
        public DateTime? OrderCollectionDate { get; set; }
        public DateTime? OrderPackingDate { get; set; }
        public DateTime ShippingDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public WorkState ShippingOrderState { get; set; }
        public decimal ProccessedQuantity { get; set; } = 0;
        public SaleItem SaleItem { get; set; }
        public Sale Sale { get; set; }
        public ICollection<AssignedUserToShippingWork> AssignedUserToShippingWorks { get; set; }

        [NotMapped]
        public override string? Code { get ; set ; }

    }
}
