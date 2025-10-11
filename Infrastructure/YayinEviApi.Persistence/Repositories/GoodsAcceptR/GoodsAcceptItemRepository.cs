using YayinEviApi.Application.Repositories.IGoogsAcceptR;
using YayinEviApi.Domain.Entities.GoodsAccepE;
using YayinEviApi.Persistence.Contexts;

namespace YayinEviApi.Persistence.Repositories.GoodsAcceptR
{
    public class GoodsAcceptItemRepository : GeneralRepository<GoodsAcceptItems>, IGoodsAccepItemRepository
    {
        public GoodsAcceptItemRepository(YayinEviApiDbContext context) : base(context)
        {
        }
    }
}
