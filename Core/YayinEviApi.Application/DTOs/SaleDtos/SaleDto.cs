using YayinEviApi.Domain.Entities.SalesE;

namespace YayinEviApi.Application.DTOs.SaleDtos
{
    public class SaleDto
    {
        public string? Id { get; set; }
        public string? Code { get; set; }
        public string? PaymentMethodId { get; set; }
        public string? PaymentMethodName { get; set; }
        public string? PriceListId { get; set; }
        public string? PriceListName { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public DateTime? DocumentDate { get; set; }
        public string? CurrentId { get; set; }
        public string? CurrentCode { get; set; }
        public string? CurrentName { get; set; }
        public string? CurrentLocalOrForeing { get; set; }
        public string? CurrentAddress { get; set; }
        public string? CurrentCountry { get; set; }
        public string? CurrentCounty { get; set; }
        public string? DeliveryCurrentId { get; set; }
        public string? DeliveryCurrentCode { get; set; }
        public string? DeliveryCurrentName { get; set; }
        public string? DeliveryLocalOrForeing { get; set; }
        public string? DeliveryCurrentAddress { get; set; }
        public string? DeliveryCurrentCountry { get; set; }
        public string? DeliveryCurrentCounty { get; set; }
        public string? CurrencyType { get; set; }
        public string? CreatingUserId { get; set; }
        public string? CreatingUserName { get; set; }
        public string? UpdatingUserId { get; set; }
        public string? UpdatingUserName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsSendedShippingOrder { get; set; }
        public decimal? MainDiscount { get; set; }
        public decimal? FixDiscount { get; set; }
        public ICollection<SaleItem>? SaleItems { get; set; }

    }
}
