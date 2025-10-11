using YayinEviApi.Application.Repositories.IProccessCategoryR;
using YayinEviApi.Domain.Entities.HelperEntities.ProccessCategoryE;
using YayinEviApi.Persistence.Contexts;
using YayinEviApi.Persistence.Repositories;

namespace YayinEviApi.Application.Repositories.ProccessCategoryR
{
    public class ProccessCategoryReadRepository : ReadRepository<ProccessCategory>, IProccessCategoryReadRepository
    {
        public ProccessCategoryReadRepository(YayinEviApiDbContext context) : base(context)
        {
        }
    }
}
