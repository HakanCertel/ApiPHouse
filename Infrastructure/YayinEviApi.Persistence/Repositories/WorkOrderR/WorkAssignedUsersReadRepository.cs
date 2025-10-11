using YayinEviApi.Application.Repositories.IWorkOrderR;
using YayinEviApi.Domain.Entities.WorkOrderE;
using YayinEviApi.Persistence.Contexts;

namespace YayinEviApi.Persistence.Repositories.WorkOrderR
{
    public class WorkAssignedUsersReadRepository : ReadRepository<WorkAssignedUsers>,IWorkAssignedUsersReadRepository
    {
        public WorkAssignedUsersReadRepository(YayinEviApiDbContext context) : base(context)
        {
        }
    }
}
