using YayinEviApi.Application.Repositories.HubMessagesR;
using YayinEviApi.Domain.Entities.HubMessagesE;
using YayinEviApi.Persistence.Contexts;

namespace YayinEviApi.Persistence.Repositories.HubMessagesR
{
    public class HubMessageReadRepository : ReadRepository<HubMessage>, IHubMessageReadRepository
    {
        public HubMessageReadRepository(YayinEviApiDbContext context) : base(context)
        {
        }
    }
}
