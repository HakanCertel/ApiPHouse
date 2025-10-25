namespace YayinEviApi.Application.DTOs.WarehouseDtos
{
    public class Warehousedto
    {
        public string? Id { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public bool IsShippingWareHouse { get; set; }
        public bool IsGoodsAcceptWareHouse { get; set; }
        public bool IsActive { get; set; }
        public string Serie { get; set; }
    }
}
