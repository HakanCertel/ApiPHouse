using YayinEviApi.Domain.Entities;
using YayinEviApi.Domain.Entities.Common;
using YayinEviApi.Domain.Entities.ProductionManagementE.RecipeE;

namespace YayinEviApi.Application.DTOs.ProductionManagementDtos.RecipeDtos
{
    public class RecipeDto:BaseEntity
    {
        public string? Id { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? UnitId { get; set; }
        public string? UnitCode { get; set; }
        public string? UnitName { get; set; }
        public decimal? Quantity { get; set; }
        public bool Default { get; set; }
        public bool IsActive { get; set; }
        public string? Description { get; set; }
        public string? WarehouseCellId { get; set; }
        public string? WarehouseCellName { get; set; }
        public string? MaterialId { get; set; }
        public string? MaterialCode { get; set; }
        public string? MaterialName { get; set; }
        public string? MaterialType{ get; set; }
        public string? MaterialUnitName { get; set; }
        public string? MaterialUnitId { get; set; }
        public string? MaterialWarehouseCellId { get; set; }
        public string? MaterialWarehouseCellName { get; set; }
        public string? Serie { get; set; }
        public string? ImagePath { get; set; }
        public List<FileManagement>? RecipeFiles { get; set; }
        public ICollection<RecipeItems>? RecipeItems { get; set; }

    }
}
