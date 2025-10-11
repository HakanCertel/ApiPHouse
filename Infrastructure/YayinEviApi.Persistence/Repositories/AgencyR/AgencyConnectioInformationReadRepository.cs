using YayinEviApi.Application.Repositories.AgencyR;
using YayinEviApi.Domain.Entities.AgencyE;
using YayinEviApi.Persistence.Contexts;

namespace YayinEviApi.Persistence.Repositories.AgencyR
{
    public class AgencyConnectionInformationReadRepository : ReadRepository<AgencyConnectionInformation>, IAgencyConnectionInformationReadRepository
    {
        public AgencyConnectionInformationReadRepository(YayinEviApiDbContext context) : base(context)
        {
        }
    }
}
