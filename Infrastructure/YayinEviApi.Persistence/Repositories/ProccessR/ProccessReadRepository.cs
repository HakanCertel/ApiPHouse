using YayinEviApi.Application.Repositories.IProccessR;
using YayinEviApi.Domain.Entities.HelperEntities.ProccessE;
using YayinEviApi.Persistence.Contexts;

namespace YayinEviApi.Persistence.Repositories.ProccessR
{
    public class ProccessReadRepository : ReadRepository<Proccess>, IProccessReadRepository
    {
        public ProccessReadRepository(YayinEviApiDbContext context) : base(context)
        {
        }
    }
}
