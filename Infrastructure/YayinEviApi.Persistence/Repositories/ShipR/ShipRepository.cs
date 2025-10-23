using YayinEviApi.Application.Repositories.IShipR;
using YayinEviApi.Domain.Entities.ShipE;
using YayinEviApi.Persistence.Contexts;

namespace YayinEviApi.Persistence.Repositories.GoodsAcceptR
{
    public class ShipRepository : GetNewCodeRepository<Ship>, IShipRepository
    {
        public ShipRepository(YayinEviApiDbContext context) : base(context)
        {
        }
    }
}
