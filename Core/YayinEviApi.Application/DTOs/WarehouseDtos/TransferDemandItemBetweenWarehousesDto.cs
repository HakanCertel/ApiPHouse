namespace YayinEviApi.Application.DTOs.WarehouseDtos
{
    public class TransferDemandItemBetweenWarehousesDto
    {
        public string? Id { get; set; }
        public string? ParentId { get; set; }
        public string? ParentCode { get; set; }
        public string? ParentDescription { get; set; }
        public bool IsConfirmed { get; set; }
        public DateTime? ConfirmedDate{ get; set; }
        public DateTime? DemandedDate { get; set; }
        public string? TransferingCellName { get; set; }
        public string? TransferingCellId { get; set; }
        public string? TransferingWarehouseName { get; set; }
        public string? TransferedCellName { get; set; }
        public string? TransferedCellId { get; set; }
        public string? TransferedWarehouseName { get; set; }
        public string? MaterialId { get; set; }
        public string? MaterialCode { get; set; }
        public string? MaterialName { get; set; }
        public string? UnitId { get; set; }
        public string? UnitName { get; set; }
        public string? UnitCode { get; set; }
        public decimal Quantity { get; set; }
        public DateTime? DocumentDate { get; set; }
        public string? CreatingUserId { get; set; }
        public string? CreatingUserName { get; set; }
        public string? UpdatingUserName { get; set; }
        public string? UpdatingUserId { get; set; }
        public string? Description { get; set; }
    }
}
