using YayinEviApi.Application.Repositories.IWorkOrderR;
using YayinEviApi.Domain.Entities.WorkOrderE;
using YayinEviApi.Persistence.Contexts;

namespace YayinEviApi.Persistence.Repositories.WorkOrderR
{
    public class WorkOrderMessagesReadRepository : ReadRepository<WorkOrderMessages>, IWorkOrderMessagesReadRepository
    {
        public WorkOrderMessagesReadRepository(YayinEviApiDbContext context) : base(context)
        {
        }
    }
}
