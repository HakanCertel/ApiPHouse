using YayinEviApi.Application.Repositories.IWorkR;
using YayinEviApi.Domain.Entities.WorkE;
using YayinEviApi.Persistence.Contexts;

namespace YayinEviApi.Persistence.Repositories.WorkR
{
    public class ProccessForWorkWriteRepository : WriteRepository<ProccessForWork>, IProccessForWorkWriteRepository
    {
        public ProccessForWorkWriteRepository(YayinEviApiDbContext context) : base(context)
        {
        }
    }
}
