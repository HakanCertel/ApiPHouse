using YayinEviApi.Application.Repositories.IWorkOrderR;
using YayinEviApi.Domain.Entities.WorkOrderE;
using YayinEviApi.Persistence.Contexts;

namespace YayinEviApi.Persistence.Repositories.WorkOrderR
{
    public class WorkAssignedUsersWriteRepository : WriteRepository<WorkAssignedUsers>,IWorkAssignedUsersWriteRepository
    {
        public WorkAssignedUsersWriteRepository(YayinEviApiDbContext context) : base(context)
        {
        }
    }
}
