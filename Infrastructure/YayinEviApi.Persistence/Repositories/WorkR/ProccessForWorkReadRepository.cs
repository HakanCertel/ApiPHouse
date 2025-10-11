using YayinEviApi.Application.Repositories.IWorkR;
using YayinEviApi.Domain.Entities.WorkE;
using YayinEviApi.Persistence.Contexts;

namespace YayinEviApi.Persistence.Repositories.WorkR
{
    public class ProccessForWorkReadRepository : ReadRepository<ProccessForWork>, IProccessForWorkReadRepository
    {
        public ProccessForWorkReadRepository(YayinEviApiDbContext context) : base(context)
        {
        }
    }
}
