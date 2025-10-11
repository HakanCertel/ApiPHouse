namespace YayinEviApi.Application.DTOs.WarehouseDtos
{
    public class StockDto
    {
        public string? Id { get; set; }
        public string? MaterialId { get; set; }
        public string? MaterialCode { get; set; }
        public string? MaterialName { get; set; }
        public string? CellofWarehouseId { get; set; }
        public string? CellofWarehouseCode { get; set; }
        public string? CellofWarehouseName { get; set; }
        public string? ShelfofWarehouseId { get; set; }
        public string? ShelfofWarehouseCode { get; set; }
        public string? ShelfofWarehouseName { get; set; }
        public string? HallofWarehouseId { get; set; }
        public string? HallofWarehouseCode { get; set; }
        public string? HallofWarehouseName { get; set; }
        public string? WarehouseId { get; set; }
        public string? WarehouseCode { get; set; }
        public string? WarehouseName { get; set; }
        public string? UnitId { get; set; }
        public string? UnitCode { get; set; }
        public string? UnitName { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? RezervationQuantity { get; set; }
        public decimal? UsableQuantity { get; set; }
    }
}
