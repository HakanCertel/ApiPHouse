using YayinEviApi.Application.Repositories.IShipR;
using YayinEviApi.Domain.Entities.ShipE;
using YayinEviApi.Persistence.Contexts;

namespace YayinEviApi.Persistence.Repositories.GoodsAcceptR
{
    public class ShipItemRepository : GeneralRepository<ShipItem>, IShipItemRepository
    {
        public ShipItemRepository(YayinEviApiDbContext context) : base(context)
        {
        }
    }
}
