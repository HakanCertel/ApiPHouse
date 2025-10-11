using YayinEviApi.Application.Repositories.IWarehouseR;
using YayinEviApi.Domain.Entities.WarehouseE;
using YayinEviApi.Persistence.Contexts;

namespace YayinEviApi.Persistence.Repositories.WarehouseR
{
    public class OrderCollectionRepository : GeneralRepository<OrderCollection>, IOrderCollectionRepository
    {
        public OrderCollectionRepository(YayinEviApiDbContext context) : base(context)
        {
        }
    }
}
