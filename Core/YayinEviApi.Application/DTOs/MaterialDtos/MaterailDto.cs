using YayinEviApi.Application.DTOs.FileManagemenDtos;
using YayinEviApi.Domain.Entities;
using YayinEviApi.Domain.Entities.WarehouseE;

namespace YayinEviApi.Application.DTOs.MaterialDtos
{
    public class MaterailDto
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
        public string? Serie { get; set; }
        public IList<FileManagement>? MaterialFiles { get; set; }
    }
}
