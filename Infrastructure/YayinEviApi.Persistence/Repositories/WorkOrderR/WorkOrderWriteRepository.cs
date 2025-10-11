using YayinEviApi.Application.Repositories.IWorkOrderR;
using YayinEviApi.Domain.Entities.WorkOrderE;
using YayinEviApi.Persistence.Contexts;

namespace YayinEviApi.Persistence.Repositories.WorkOrderR
{
    public class WorkOrderWriteRepository : WriteRepository<WorkOrder>, IWorkOrderWriteRepository
    {
        public WorkOrderWriteRepository(YayinEviApiDbContext context) : base(context)
        {
        }
    }
}
