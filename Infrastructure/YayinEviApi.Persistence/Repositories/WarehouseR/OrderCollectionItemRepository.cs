using YayinEviApi.Application.Repositories.IWarehouseR;
using YayinEviApi.Domain.Entities.WarehouseE;
using YayinEviApi.Persistence.Contexts;

namespace YayinEviApi.Persistence.Repositories.WarehouseR
{
    public class OrderCollectionItemRepository : GeneralRepository<OrderCollectionItem>, IOrderCollectionItemRepository
    {
        public OrderCollectionItemRepository(YayinEviApiDbContext context) : base(context)
        {
        }
    }
}
