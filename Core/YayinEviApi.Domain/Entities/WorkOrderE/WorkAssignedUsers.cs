using System.ComponentModel.DataAnnotations.Schema;
using YayinEviApi.Domain.Entities.Common;

namespace YayinEviApi.Domain.Entities.WorkOrderE
{
    public class WorkAssignedUsers:BaseEntity
    {
        public Guid WorkOrderId { get; set; }
        public string UserId { get; set; }

        public WorkOrder? WorkOrder { get; set; }

        [NotMapped]
        public override string Code { get; set; }
    }
}
