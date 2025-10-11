namespace YayinEviApi.Application.DTOs.WarehouseDtos
{
    public class HallofWarehouseDto
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public string? WarehouseId { get; set; }
        public string? WarehouseCode { get; set; }     
        public string? WarehouseName { get; set; }
        public bool IsShippingWarehouse { get; set; }
        public bool IsGoodAcceptWarehouse { get; set; }
        public bool IsActive { get; set; }
    }
}
