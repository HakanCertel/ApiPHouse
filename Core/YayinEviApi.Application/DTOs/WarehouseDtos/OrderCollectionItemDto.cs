namespace YayinEviApi.Application.DTOs.WarehouseDtos
{
    public class OrderCollectionItemDto
    {
        public string? Id { get; set; }
        public string? ParentId { get; set; }
        public string? ParentCode { get; set; }
        public string? ShippingOrderId { get; set; }
        public string? SaleOrderId { get; set; }
        public string? SaleOrderCode { get; set; }
        public string? SaleItemId { get; set; }
        public string? CurrentName { get; set; }
        public DateTime? ShippingDate { get; set; }
        public string? MaterialId { get; set; }
        public string? MaterialName { get; set; }
        public string? UnitName { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? RezervationQuantity { get; set; }
        public decimal? CollectedQuantity { get; set; }
        public string? CollectedCellId { get; set; }
        public string? CollectedCellName { get; set; }
        public bool IsActive { get; set; }
        public string? ImagePath { get; set; }
    }
}
