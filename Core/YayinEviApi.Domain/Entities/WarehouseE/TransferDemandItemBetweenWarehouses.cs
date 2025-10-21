using System.ComponentModel.DataAnnotations.Schema;
using YayinEviApi.Domain.Entities.Common;
using YayinEviApi.Domain.Entities.MaterialE;
using YayinEviApi.Domain.Entities.UnitE;

namespace YayinEviApi.Domain.Entities.WarehouseE
{
    public class TransferDemandItemBetweenWarehouses:BaseEntity
    {
        public Guid ParentId { get; set; }
        public bool IsConfirmed { get; set; }
        public DateTime? ConfirmedDate{ get; set; }
        public DateTime? DemandedDate{ get; set; }
        public Guid MaterialId { get; set; }
        public Guid? UnitId { get; set; }
        public decimal Quantity { get; set; }
        public string? Description { get; set; }
        public TransferDemandBetweenWarehouses Parent { get; set; }
        public Material Material { get; set; }
        public MaterialUnit Unit { get; set; }
        [NotMapped]
        public override bool IsActive { get; set; }
        [NotMapped]
        public override string Code { get; set; }
    }
}
