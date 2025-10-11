namespace YayinEviApi.Application.DTOs.WarehouseDtos
{
    public class StockMovementDto
    {
        public string? Id { get; set; }
        public decimal? MovementQuantity { get; set; }
        public string? CreatingUserId { get; set; }
        public string? CreatingUserNameSurname { get; set; }
        public string? MovementClass { get; set; }
        public string? MovementClassId { get; set; }
        public string? MovementClassCode { get; set; }
        public string? MaterialId { get; set; }
        public string? MaterialCode { get; set; }
        public string? MaterialName { get; set; }
        public string? EnteringCellofWarehouseId { get; set; }
        public string? EnteringCellofWarehouseCode { get; set; }
        public string? EnteringCellofWarehouseName { get; set; }
        public string? EnteringShelfofWarehouseId { get; set; }
        public string? EnteringShelfofWarehouseCode { get; set; }
        public string? EnteringShelfofWarehouseName { get; set; }
        public string? EnteringHallofWarehouseId { get; set; }
        public string? EnteringHallofWarehouseCode { get; set; }
        public string? EnteringHallofWarehouseName { get; set; }
        public string? EnteringWarehouseId { get; set; }
        public string? EnteringWarehouseCode { get; set; }
        public string? EnteringWarehouseName { get; set; }
        public string? OutgoingCellofWarehouseId { get; set; }
        public string? OutgoingCellofWarehouseCode { get; set; }
        public string? OutgoingCellofWarehouseName { get; set; }
        public string? OutgoingShelfofWarehouseId { get; set; }
        public string? OutgoingShelfofWarehouseCode { get; set; }
        public string? OutgoingShelfofWarehouseName { get; set; }
        public string? OutgoingHallofWarehouseId { get; set; }
        public string? OutgoingHallofWarehouseCode { get; set; }
        public string? OutgoingHallofWarehouseName { get; set; }
        public string? OutgoingWarehouseId { get; set; }
        public string? OutgoingWarehouseCode { get; set; }
        public string? OutgoingWarehouseName { get; set; }
        public string? UnitId { get; set; }
        public string? UnitCode { get; set; }
        public string? UnitName { get; set; }
        public DateTime? MovementDate { get; set; }
    }
}
