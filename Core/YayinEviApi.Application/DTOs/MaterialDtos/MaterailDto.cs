using YayinEviApi.Domain.Entities;
using YayinEviApi.Domain.Entities.Common;
using YayinEviApi.Domain.Interfaces;

namespace YayinEviApi.Application.DTOs.MaterialDtos
{
    public class MaterailDto:BaseEntity
    {
        public string? Id { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? MaterialType { get; set; } = "Mamül/Ürün";
        public bool IsActive { get; set; }
        public string? CellofWarehouseId { get; set; }
        public string? CellofWarehouseCode { get; set; }
        public string? CellofWarehouseName { get; set; }
        public string? ShelfofWarehouseId { get; set; }
        public string? ShelfofWarehouseCode { get; set; }
        public string? ShelfofWarehouseName { get; set; }
        public string? HallofWarehouseId { get; set; }
        public string? HallofWarehouseCode { get; set; }
        public string? HallofWarehouseName { get; set; }
        public string? WarehouseId { get; set; }
        public string? WarehouseCode { get; set; }
        public string? WarehouseName { get; set; }
        public string? ImagePath { get; set; }
        public string? UnitId { get; set; }
        public string? UnitName { get; set; }
        public string MainCategoryName { get; set; }
        public string MainCategoryCode { get; set; }
        public string MainCategoryId { get; set; }
        public string SubCategory_1Name { get; set; }
        public string SubCategory_1Code { get; set; }
        public string SubCategory_1Id { get; set; }
        public string SubCategory_2Name { get; set; }
        public string SubCategory_2Code { get; set; }
        public string SubCategory_2Id { get; set; }
        public string? Serie { get; set; }
        public List<FileManagement>? MaterialFiles { get; set; }
    }
}
