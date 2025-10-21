using System.ComponentModel.DataAnnotations.Schema;
using YayinEviApi.Domain.Entities.Common;
using YayinEviApi.Domain.Entities.MaterialE;
using YayinEviApi.Domain.Entities.WarehouseE;

namespace YayinEviApi.Domain.Entities.RezervationE
{
    public class Rezervation:BaseEntity
    {
        public Guid ParentId { get; set; }
        public string? ParentCode { get; set; }
        public Guid? ChildId { get; set; }
        public string? ChildCode { get; set; }
        public Guid MaterialId { get; set; }
        public Guid CellofWarehouseId { get; set; }
        public decimal RezervationQuantity { get; set; }
        public string? RezervationStatu { get; set; }
        public string? RezervationState { get; set; }
        public string? CreatingUserId { get; set; }
        public string? UpdatingUserId { get; set; }
        public bool IsCancel { get; set; }

        public Material Material { get; set; }
        public CellofWarehouse CellofWarehouse { get; set; }

        [NotMapped]
        public override string Code { get; set; }
    }
}
