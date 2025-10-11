using YayinEviApi.Application.Repositories.AgencyFileR;
using YayinEviApi.Domain.Entities;
using YayinEviApi.Persistence.Contexts;

namespace YayinEviApi.Persistence.Repositories.AgencyFileR
{
    public class AgencyFileWriteRepository : WriteRepository<AgencyFile>, IAgencyFileWriteRepository
    {
        public AgencyFileWriteRepository(YayinEviApiDbContext context) : base(context)
        {
        }
    }
}
