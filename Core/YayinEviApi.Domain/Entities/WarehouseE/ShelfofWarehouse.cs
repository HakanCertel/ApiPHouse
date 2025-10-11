using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YayinEviApi.Domain.Entities.Common;

namespace YayinEviApi.Domain.Entities.WarehouseE
{
    public class ShelfofWarehouse:BaseEntity
    {
        public Guid HallofWarehouseId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public HallofWarehouse HallofWarehouse { get; set; }
        public ICollection<CellofWarehouse> CellofWarehouses { get; set; }
    }
}
