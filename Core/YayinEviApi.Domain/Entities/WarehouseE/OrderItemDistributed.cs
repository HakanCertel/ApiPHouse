using YayinEviApi.Domain.Entities.Common;
using YayinEviApi.Domain.Entities.MaterialE;
using YayinEviApi.Domain.Entities.SalesE;

namespace YayinEviApi.Domain.Entities.WarehouseE
{
    public class OrderItemDistributed:BaseEntity
    {
        public Guid ParentId { get; set; }
        public Guid SaleOrderId { get; set; }
        public Guid SaleItemId { get; set; }
        public Guid MaterialId { get; set; }
        public Guid DistributedCellId { get; set; }
        public decimal CollectedQuantity { get; set; }
       
        public OrderDistributed Parent { get; set; }
        public CellofWarehouse DistributedCell { get; set; }
        public SaleItem SaleItem { get; set; }
        public Material Material { get; set; }
    }
}
