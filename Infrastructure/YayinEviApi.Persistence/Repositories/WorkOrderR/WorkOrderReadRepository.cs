using YayinEviApi.Application.Repositories.IWorkOrderR;
using YayinEviApi.Domain.Entities.WorkOrderE;
using YayinEviApi.Persistence.Contexts;
using YayinEviApi.Persistence.Repositories;

namespace YayinEviApi.Application.Repositories.WorkOrderR
{
    public class WorkOrderReadRepository : ReadRepository<WorkOrder>, IWorkOrderReadRepository
    {
        public WorkOrderReadRepository(YayinEviApiDbContext context) : base(context)
        {
        }
    }
}
