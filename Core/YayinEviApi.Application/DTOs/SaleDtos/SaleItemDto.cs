namespace YayinEviApi.Application.DTOs.SaleDtos
{
    public class SaleItemDto
    {
        public string? Id { get; set; }
        public string? ParentId { get; set; }
        public string? ParentCode { get; set; }
        public string? MaterialId { get; set; }
        public string? MaterialCode { get; set; }
        public string? MaterialName { get; set; }
        public string? MaterialUnitId { get; set; }
        public string? MaterialUnitName { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? RezervedQuantity { get; set; }
        public decimal? Price { get; set; }
        public decimal? ItemDiscountRate { get; set; }
        public decimal? NetTotal { get; set; }
        public decimal? TaxTotal { get; set; }
        public decimal? GeneralTotal { get; set; }
        public string? TaxType { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public DateTime? DocumentDate { get; set; }
        public string? CurrentId { get; set; }
        public string? CurrentCode { get; set; }
        public string? CurrentName { get; set; }
        public string? CurrentAddress { get; set; }
        public string? CurrentCountry { get; set; }
        public string? CurrentCounty { get; set; }
        public string? DeliveryCurrentId { get; set; }
        public string? DeliveryCurrentCode { get; set; }
        public string? DeliveryCurrentName { get; set; }
        public string? DeliveryCurrentAddress { get; set; }
        public string? DeliveryCurrentCountry { get; set; }
        public string? DeliveryCurrentCounty { get; set; }
        public string? CurrencyType { get; set; }
        public bool IsSendedShippingOrder { get; set; }
        public bool IsChangeShippingState { get; set; }
        public bool IsActive { get; set; }
    }
}
