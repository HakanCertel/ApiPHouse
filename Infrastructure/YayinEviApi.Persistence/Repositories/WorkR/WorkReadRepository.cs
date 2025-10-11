using YayinEviApi.Application.Repositories.IWorkR;
using YayinEviApi.Domain.Entities.WorkE;
using YayinEviApi.Persistence.Contexts;

namespace YayinEviApi.Persistence.Repositories.WorkR
{
    public class WorkReadRepository : ReadRepository<Work>, IWorkReadRepository
    {
        public WorkReadRepository(YayinEviApiDbContext context) : base(context)
        {
        }
    }
}
