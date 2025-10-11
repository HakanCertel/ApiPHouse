using YayinEviApi.Application.Repositories.IWorkOrderR;
using YayinEviApi.Domain.Entities.WorkOrderE;
using YayinEviApi.Persistence.Contexts;

namespace YayinEviApi.Persistence.Repositories.WorkOrderR
{
    public class WorkOrderMessagesWriteRepository : WriteRepository<WorkOrderMessages>, IWorkOrderMessagesWriteRepository
    {
        public WorkOrderMessagesWriteRepository(YayinEviApiDbContext context) : base(context)
        {
        }
    }
}
