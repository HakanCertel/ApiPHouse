namespace YayinEviApi.Application.DTOs.WarehouseDtos
{
    public class StockCountItemsDto
    {
        public string? Id { get; set; }
        public string? StockCounttId { get; set; }
        public string? StockCountCode { get; set; }
        public DateTime? DocumentDate { get; set; }
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
        public string? CreatingUserId { get; set; }
        public string? CreatingUserName { get; set; }
        public string? UpdatingUserId { get; set; }
        public string? UpdatingUserName { get; set; }
    }
}
