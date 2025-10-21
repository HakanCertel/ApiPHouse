using System.ComponentModel.DataAnnotations.Schema;
using YayinEviApi.Domain.Entities.Common;

namespace YayinEviApi.Domain.Entities.WarehouseE
{
    public class AssignedUserToShippingWork:BaseEntity
    {
        public Guid ShippingOrderId { get; set; }
        public string UserId { get; set; }

        public ShippingOrder ShippingOrder { get; set; }

        [NotMapped]
        public override string Code { get; set; }
    }
}
