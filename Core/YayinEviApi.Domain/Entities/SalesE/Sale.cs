using YayinEviApi.Domain.Entities.Common;
using YayinEviApi.Domain.Entities.CurrentE;
using YayinEviApi.Domain.Enum;

namespace YayinEviApi.Domain.Entities.SalesE
{
    public class Sale:BaseEntity
    {
        public string Code { get; set; }
        public Guid CurrentId { get; set; }
        public DateTime DocumentDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public Guid? DeliveryCurrentId { get; set; }
        public CurrencyType CurrencyType { get; set; }
        public Guid? PaymentMethodId { get; set; }
        public Guid? PriceListId { get; set; }
        public string CreatingUserId { get; set; }
        public string? UpdatingUserId { get; set; }
        public decimal? MainDiscountRate { get; set; }
        public decimal? FixDiscount { get; set; }
        public bool IsSendedShippingOrder { get; set; }
        public bool IsCompleted { get; set; }
        public Current Current { get; set; }
        public SubCurrent DeliveryCurrent { get; set; }
        public ICollection<SaleItem> SaleItems { get; set; }
    }
}
