using System.ComponentModel.DataAnnotations.Schema;
using YayinEviApi.Domain.Entities.Common;
using YayinEviApi.Domain.Entities.MaterialE;
using YayinEviApi.Domain.Entities.SalesE;

namespace YayinEviApi.Domain.Entities.WarehouseE
{
    public class OrderCollectionItem:BaseEntity
    {
        public Guid ParentId { get; set; }
        public Guid ShippingOrderId { get; set; }
        public Guid SaleOrderId { get; set; }
        public Guid SaleItemId { get; set; }
        public Guid MaterialId { get; set; }
        public decimal CollectedQuantity { get; set; }
        public Guid CellId { get; set; }

        //public ShippingOrder ShippingOrder { get; set; }
        public OrderCollection Parent { get; set; }
        public SaleItem SaleItem { get; set; }
        public CellofWarehouse Cell { get; set; }
        public Material Material { get; set; }

        [NotMapped]
        public override string Code { get; set; }
    }
}
