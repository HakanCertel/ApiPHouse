using YayinEviApi.Domain.Entities.GoodsAccepE;

namespace YayinEviApi.Application.DTOs.GoodsAcceptDtos
{
    public class GoodsAcceptDto
    {
        public string? Id { get; set; }
        public string? Code { get; set; }
        public string? AcceptCellofWarehouseId { get; set; }
        public string? AcceptCellofWarehouseName { get; set; }
        public string? WarehouseName { get; set; }
        public string? DocumentCode { get; set; }
        public DateTime? DocumentDate { get; set; }
        public string? CurrentId { get; set; }
        public string? CurrentName { get; set; }
        public string? Address { get; set; }
        public string? Country { get; set; }
        public string? County { get; set; }
        public string? Town { get; set; }
        public string? CreatingUserId { get; set; }
        public string? CreatingUserName { get; set; }
        public string? UpdatingUserId { get; set; }
        public string? UpdatingUserName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public ICollection<GoodsAcceptItems>? GoodsAcceptItemList { get; set; }
        public string? Serie { get; set; }

    }
}
