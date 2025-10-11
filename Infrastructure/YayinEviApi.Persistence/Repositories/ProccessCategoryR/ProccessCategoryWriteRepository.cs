using YayinEviApi.Application.Repositories.IProccessCategoryR;
using YayinEviApi.Domain.Entities.HelperEntities.ProccessCategoryE;
using YayinEviApi.Persistence.Contexts;

namespace YayinEviApi.Persistence.Repositories.ProccessCategoryR
{
    public class ProccessCategoryWriteRepository : WriteRepository<ProccessCategory>, IProccessCategoryWriteRepository
    {
        public ProccessCategoryWriteRepository(YayinEviApiDbContext context) : base(context)
        {
        }
    }
}
