using YayinEviApi.Application.Repositories.HubMessagesR;
using YayinEviApi.Domain.Entities.HubMessagesE;
using YayinEviApi.Persistence.Contexts;

namespace YayinEviApi.Persistence.Repositories.HubMessagesR
{
    public class HubMessageWriteRepository : WriteRepository<HubMessage>, IHubMessageWriteRepository
    {
        public HubMessageWriteRepository(YayinEviApiDbContext context) : base(context)
        {
        }
    }
}
