using YayinEviApi.Application.Repositories.AgencyR;
using YayinEviApi.Domain.Entities.AgencyE;
using YayinEviApi.Persistence.Contexts;

namespace YayinEviApi.Persistence.Repositories.AgencyR
{
    public class AgencyWriteRepository : WriteRepository<Agency>, IAgencyWriteRepository
    {
        public AgencyWriteRepository(YayinEviApiDbContext context) : base(context)
        {
        }
    }
}
