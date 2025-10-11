using YayinEviApi.Application.Repositories.AgencyFileR;
using YayinEviApi.Domain.Entities;
using YayinEviApi.Persistence.Contexts;

namespace YayinEviApi.Persistence.Repositories.AgencyFileR
{
    public class AgencyFileReadRepository : ReadRepository<AgencyFile>, IAgencyFileReadRepository
    {
        public AgencyFileReadRepository(YayinEviApiDbContext context) : base(context)
        {
        }
    }
}
