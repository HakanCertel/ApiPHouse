using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YayinEviApi.Domain.Entities.Common;
using YayinEviApi.Domain.Entities.HelperEntities.ProccessE;
using YayinEviApi.Domain.Entities.ProjectE;
using YayinEviApi.Domain.Enum;

namespace YayinEviApi.Domain.Entities.WorkOrderE
{
    public class WorkOrder:BaseEntity
    {
        public string? Code { get; set; }
        public Guid? ProjectId { get; set; }
        public Guid? ProccessId { get; set; }
        public string? AssignedUserId { get; set; }
        public string? CreatingUserId { get; set; }
        public string? UpdatingUserId { get; set; }
        public DateTime? FinishedDate { get; set; } = DateTime.Now;
        public WorkState WorkState { get; set; }
        public string? Description { get; set; }

        public Project Project { get; set; }
        public Proccess Proccess { get; set; }

        public ICollection<WorkAssignedUsers> WorkAssignedUsers { get; set; }
        public ICollection<WorkOrderMessages> WorkOrderMessages { get; set; }
    }
}
