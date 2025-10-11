using YayinEviApi.Domain.Entities.GoodsAccepE;

namespace YayinEviApi.Application.DTOs.GoodsAcceptDtos
{
    public class GoodsAcceptItemDto
    {
        public string? Id { get; set; }
        public string? ParentId { get; set; }
        public string? ParentCode { get; set; }
        public string? EnteringCellName { get; set; }
        public string? EnteringCellId { get; set; }
        public string? EnteringWarehouseName { get; set; }
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
    }
}
