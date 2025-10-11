using YayinEviApi.Application.Repositories.IWorkR;
using YayinEviApi.Domain.Entities.WorkE;
using YayinEviApi.Persistence.Contexts;

namespace YayinEviApi.Persistence.Repositories.WorkR
{
    public class WorkWriteRepository : WriteRepository<Work>, IWorkWriteRepository
    {
        public WorkWriteRepository(YayinEviApiDbContext context) : base(context)
        {
        }
    }
}
