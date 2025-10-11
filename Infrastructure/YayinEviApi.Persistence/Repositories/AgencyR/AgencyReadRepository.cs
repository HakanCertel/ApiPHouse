using YayinEviApi.Application.Repositories.AgencyR;
using YayinEviApi.Domain.Entities.AgencyE;
using YayinEviApi.Persistence.Contexts;

namespace YayinEviApi.Persistence.Repositories.AgencyR
{
    public class AgencyReadRepository : ReadRepository<Agency>, IAgencyReadRepository
    {
        public AgencyReadRepository(YayinEviApiDbContext context) : base(context)
        {
        }
    }
}
