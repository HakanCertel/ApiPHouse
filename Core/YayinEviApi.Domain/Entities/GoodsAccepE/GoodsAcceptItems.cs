using System.ComponentModel.DataAnnotations.Schema;
using YayinEviApi.Domain.Entities.Common;
using YayinEviApi.Domain.Entities.GoodsAcceptE;
using YayinEviApi.Domain.Entities.MaterialE;
using YayinEviApi.Domain.Entities.UnitE;

namespace YayinEviApi.Domain.Entities.GoodsAccepE
{
    public class GoodsAcceptItems:BaseEntity
    {
        public Guid ParentId { get; set; }
        public Guid MaterialId { get; set; }
        public Guid? UnitId { get; set; }
        public decimal Quantity { get; set; }
        [NotMapped]
        public override bool IsActive { get; set; }
        public Material Material { get; set; }
        public MaterialUnit Unit { get; set; }
        public GoodsAccep Parent { get; set; }
        
        [NotMapped]
        public override string Code { get; set; }
    }
}
