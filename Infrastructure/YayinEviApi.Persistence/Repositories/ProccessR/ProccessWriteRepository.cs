using YayinEviApi.Application.Repositories.IProccessR;
using YayinEviApi.Domain.Entities.HelperEntities.ProccessE;
using YayinEviApi.Persistence.Contexts;

namespace YayinEviApi.Persistence.Repositories.ProccessR
{
    public class ProccessWriteRepository : WriteRepository<Proccess>, IProccessWriteRepository
    {
        public ProccessWriteRepository(YayinEviApiDbContext context) : base(context)
        {
        }
    }
}
