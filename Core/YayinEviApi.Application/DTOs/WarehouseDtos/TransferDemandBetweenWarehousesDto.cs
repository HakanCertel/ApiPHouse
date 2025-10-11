namespace YayinEviApi.Application.DTOs.WarehouseDtos
{
    public class TransferDemandBetweenWarehousesDto
    {
        public string? Id { get; set; }
        public string? DocumentCode { get; set; }
        public DateTime? DocumentDate { get; set; }
        public bool IsConfirmed { get; set; }
        public bool IsActive { get; set; }
        public DateTime? ConfirmedDate { get; set; }
        public DateTime? DemandedDate { get; set; }
        public string? TransferingCellofWarehouseId { get; set; }
        public string? TransferingCellofWarehouseName { get; set; }
        public string? TransferingWarehouseName { get; set; }
        public string? TransferedCellofWarehouseId { get; set; }
        public string? TransferedCellofWarehouseName { get; set; }
        public string? TransferedWarehouseName { get; set; }
        public string? CreatingUserId { get; set; }
        public string? CreatingUserName { get; set; }
        public string? UpdatingUserId { get; set; }
        public string? UpdatingUserName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? Description { get; set; }

    }
}
