namespace YayinEviApi.Application.DTOs.WarehouseDtos
{
    public class CellofWarehouseDto
    {
        public string? Id { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? ShelfofWarehouseId { get; set; }
        public string? ShelfofWarehouseCode { get; set; }
        public string? ShelfofWarehouseName { get; set; }
        public string? HallofWarehouseId { get; set; }
        public string? HallofWarehouseCode { get; set; }
        public string? HallofWarehouseName { get; set; }
        public string? WarehouseId { get; set; }
        public string? WarehouseCode { get; set; }
        public string? WarehouseName { get; set; }
        public bool IsShippingWarehouse { get; set; }
        public bool IsDefaultCell { get; set; }
        public bool IsActive { get; set; }
    }
}
