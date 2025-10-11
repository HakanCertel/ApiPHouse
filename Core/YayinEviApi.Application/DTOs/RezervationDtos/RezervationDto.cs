namespace YayinEviApi.Application.DTOs.RezervationDtos
{
    public class RezervationDto
    {
        public string? Id { get; set; }
        public string? ParentId { get; set; }
        public string? ParentCode { get; set; }
        public string? ChildId { get; set; }
        public string? ChildCode { get; set; }
        public string? MaterialId { get; set; }
        public string? MaterialName { get; set; }
        public string? UnitName { get; set; }
        public string? CellofWarehouseId { get; set; }
        public string? CellofWarehouseName { get; set; }
        public string? ShelfofWarehouseName { get; set; }
        public string? HallofWarehouseName { get; set; }
        public string? WarehouseName { get; set; }
        public decimal? RezervationQuantity { get; set; }
        public string? RezervationStatu { get; set; }
        public string? RezervationState { get; set; }
        public string? CreatingUserId { get; set; }
        public string? CreatingUserName { get; set; }
        public string? UpdatingUserId { get; set; }
        public string? UpdatingUserName { get; set; }
        public bool IsCancel { get; set; }
    }
}
