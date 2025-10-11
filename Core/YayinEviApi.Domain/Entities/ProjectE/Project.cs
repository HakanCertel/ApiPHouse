using YayinEviApi.Domain.Entities.AgencyE;
using YayinEviApi.Domain.Entities.Common;
using YayinEviApi.Domain.Entities.WorkE;
using YayinEviApi.Domain.Entities.WorkOrderE;
using YayinEviApi.Domain.Enum;

namespace YayinEviApi.Domain.Entities.ProjectE
{
    public class Project:BaseEntity
    {
        public string Code { get; set; }
        public Guid? AgencyId { get; set; }
        public Guid WorkId { get; set; }
        public State State { get; set; }=State.Waiting;
        public DateTime? ContractStartDate { get; set; }
        public DateTime? ContractFinishDate { get; set; }
        public decimal? ContractPrice { get; set; }

        public Agency Agency { get; set; }
        public Work Work { get; set; }
        public ICollection<WorkOrder> WorkOrders { get; set; }

    }
}
