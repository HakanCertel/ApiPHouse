using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YayinEviApi.Application.DTOs.WarehouseDtos
{
    public class ShelfofWarehouseDto
    {
        public string? Id { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? HallofWarehouseId { get; set; }
        public string? HallofWarehouseCode { get; set; }
        public string? HallofWarehouseName { get; set; }
        public string? WarehouseId { get; set; }
        public string? WarehouseCode { get; set; }
        public string? WarehouseName { get; set; }
        public bool IsShippingWarehouse { get; set; }
        public bool IsGoodAcceptWarehouse { get; set; }
        public bool IsActive { get; set; }
    }
}
