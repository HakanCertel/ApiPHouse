using YayinEviApi.Application.Repositories.IGoogsAcceptR;
using YayinEviApi.Domain.Entities.GoodsAcceptE;
using YayinEviApi.Persistence.Contexts;

namespace YayinEviApi.Persistence.Repositories.GoodsAcceptR
{
    public class GoodsAcceptRepository : GeneralRepository<GoodsAccep>, IGoodsAcceptRepository
    {
        public GoodsAcceptRepository(YayinEviApiDbContext context) : base(context)
        {
        }
    }
}
