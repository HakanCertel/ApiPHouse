using YayinEviApi.Domain.Entities.SalesE;

namespace YayinEviApi.Application.DTOs.WarehouseDtos
{
    public class ShippingOrderDto
    {
        public string? Id { get; set; }
        public string? SaleOrderId { get; set; }
        public string? SaleOrderCode { get; set; }
        public string? ShippingOrderState { get; set; }
        public string? SaleItemId { get; set; }
        public List<SaleItem>? SaleItems { get; set; }
        public string? RezervationItemId { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? RezervedQuantity { get; set; }
        public decimal? ProccessedQuantity { get; set; }
        public decimal? RemainderQuantity { get; set; }
        public decimal? TotalStockQuantity { get; set; }
        public decimal? TotalRezervationQuantity { get; set; }
        public decimal? UsableStockQuantity { get; set; }
        public decimal? CollectedQuantity { get; set; }
        public string? MaterialId { get; set; }
        public string? MaterialCode { get; set; }
        public string? MaterialName { get; set; }
        public string? MaterialUnitId { get; set; }
        public string? MaterialUnitName { get; set; }
        public string? RezervedCellofWarehouseId { get; set; }
        public string? RezervedCellofWarehouseName { get; set; }
        public string? RezervedShelfofWarehouseName { get; set; }
        public string? RezervedHallofWarehouseName { get; set; }
        public string? RezervedWarehouseName { get; set; }
        public string? CurrentId { get; set; }
        public string? CurrentCode { get; set; }
        public string? CurrentName { get; set; }
        public string? CurrentAddress { get; set; }
        public string? CurrentCountry { get; set; }
        public string? CurrentCounty { get; set; }
        public DateTime? OrderCollectionDate { get; set; }
        public DateTime? OrderPackingDate { get; set; }
        public DateTime? ShippingDate { get; set; }
        public bool IsShipped { get; set; }
        public bool IsActive { get; set; }
        public bool IsStartedCollection { get; set; }
        public bool IsComplededCollection { get; set; }
        public bool IsStartedPacking { get; set; }
        public bool IsCompletedPacking { get; set; }
        public string? AssignedUserId { get; set; }
        public string? AssignedUserName { get; set; }
        public string? CreatingUserId { get; set; }
        public string? CreatingUserName { get; set; }
    }
}
