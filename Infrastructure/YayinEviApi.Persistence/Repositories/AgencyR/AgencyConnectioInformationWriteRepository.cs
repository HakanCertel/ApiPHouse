using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YayinEviApi.Application.Repositories.AgencyR;
using YayinEviApi.Domain.Entities.AgencyE;
using YayinEviApi.Persistence.Contexts;

namespace YayinEviApi.Persistence.Repositories.AgencyR
{
    public class AgencyConnectioInformationWriteRepository : WriteRepository<AgencyConnectionInformation>, IAgencyConnectionInformationWriteRepository
    {
        public AgencyConnectioInformationWriteRepository(YayinEviApiDbContext context) : base(context)
        {
        }
    }
}
