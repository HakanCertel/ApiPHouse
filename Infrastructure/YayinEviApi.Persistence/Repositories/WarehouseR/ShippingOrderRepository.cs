using YayinEviApi.Application.Repositories.IWarehouseR;
using YayinEviApi.Domain.Entities.WarehouseE;
using YayinEviApi.Persistence.Contexts;

namespace YayinEviApi.Persistence.Repositories.WarehouseR
{
    public class ShippingOrderRepository : GeneralRepository<ShippingOrder>, IShippingOrderRepository
    {
        public ShippingOrderRepository(YayinEviApiDbContext context) : base(context)
        {
        }
    }
}
