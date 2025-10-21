using System.ComponentModel.DataAnnotations.Schema;
using YayinEviApi.Domain.Entities.Common;

namespace YayinEviApi.Domain.Entities.WarehouseE
{
    public class TransferDemandBetweenWarehouses:BaseEntity
    {
        public string? DocumentCode { get; set; }
        public DateTime DocumentDate { get; set; }
        public DateTime? DemandedDate { get; set; }
        public DateTime? ConfirmedDate { get; set; }
        public bool IsConfirmed { get; set; }
        public Guid? TransferingCellofWarehouseId { get; set; }
        public Guid? TransferedCellofWarehouseId { get; set; }
        public string CreatingUserId { get; set; }
        public string? UpdatingUserId { get; set; }
        public string? Description { get; set; }

        public CellofWarehouse TransferingCellofWarehouse { get; set; }
        public CellofWarehouse TransferedCellofWarehouse { get; set; }

        [NotMapped]
        public override string Code { get; set; }

    }
}
