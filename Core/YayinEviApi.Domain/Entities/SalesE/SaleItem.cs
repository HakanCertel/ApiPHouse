using YayinEviApi.Domain.Entities.Common;
using YayinEviApi.Domain.Entities.MaterialE;
using YayinEviApi.Domain.Enum;

namespace YayinEviApi.Domain.Entities.SalesE
{
    public class SaleItem:BaseEntity
    {
        public Guid? ParentId { get; set; }
        public Guid? MaterialId { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public TaxType TaxType { get; set; }
        public decimal? ItemDiscountRate { get; set; }
        public bool IsSendedShippingOrder { get; set; }

        public Sale Parent { get; set; }
        public Material Material { get; set; }
    }
}
