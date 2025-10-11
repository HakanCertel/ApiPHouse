namespace YayinEviApi.Application.Features.Queries.WarehouseQ.ShippingOrderQ
{
    public class GetAllShoppingOrderQueryRequest
    {
        public bool? Cancel { get; set; } = true;
        public string? State { get; set; }
        public bool? IsLoggingUser { get; set; }
    }
}
