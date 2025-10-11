using YayinEviApi.Application.Repositories.ICurrentR;
using YayinEviApi.Domain.Entities.CurrentE;
using YayinEviApi.Persistence.Contexts;

namespace YayinEviApi.Persistence.Repositories.CurrentR
{
    public class CurrentRepository : GeneralRepository<Current>, ICurrentRepository
    {
        public CurrentRepository(YayinEviApiDbContext context) : base(context)
        {
        }
    }
}
