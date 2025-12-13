using YayinEviApi.Domain.Interfaces;

namespace YayinEviApi.Application.DTOs.ProductionManagementDtos.RecipeDtos
{
    public class RecipeItemsDto:IBaseEntity
    {
        public string? Id { get; set; }
        public string? ConsumptionWarehouseCellId { get; set; }
        public string? ConsumptionWarehouseCellName { get; set; }
        public string? ConsumptionUnitId { get; set; }
        public string? ConsumptionUnitCode { get; set; }
        public string? ConsumptionUnitName { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? RateofOutage { get; set; }
        public string? Description { get; set; }
        public string? ImagePath { get; set; }
        public string? ParentId { get; set; }
        public  string? ParentCode { get; set; }
        public  string? ParentName { get; set; }
        public decimal? ParentQuantity { get; set; }
        public decimal? ParentRateofOutage { get; set; }
        public string? ParentMaterialId { get; set; }
        public string? ParentMaterialCode { get; set; }
        public string? ParentMaterialName { get; set; }
        public string? ParentMaterialUnitId { get; set; }
        public string? ParentMaterialUnitName { get; set; }
        public string? MaterialId { get; set; }
        public string? MaterialCode { get; set; }
        public string? MaterialName { get; set; }
        public string? MaterialUnitId { get; set; }
        public string? MaterialUnitCode { get; set; }
        public string? MaterialUnitName { get; set; }
        public string? MaterialCellId { get; set; }
        public string? MaterialCellCode { get; set; }
        public string? MaterialCellName { get; set; }
    }
}
