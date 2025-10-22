namespace YayinEviApi.Application.DTOs.WarehouseDtos
{
    public class StockCountDto
    {
        public string? Id { get; set; }
        public string? Code { get; set; }
        public string? DocumentCode { get; set; }
        public DateTime? DocumenDate { get; set; }
        public bool IsConfirmed { get; set; }
        public bool IsActive { get; set; }
        public DateTime? ConfirmDate { get; set; }
        public string? CreatingUserId { get; set; }
        public string? CreatingUserName { get; set; }
        public string? UpdatingUserId { get; set; }
        public string? UpdatingUserName { get; set; }
        public string? Serie { get; set; }
    }
}
