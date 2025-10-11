using YayinEviApi.Application.Repositories.IWarehouseR;
using YayinEviApi.Domain.Entities.WarehouseE;
using YayinEviApi.Persistence.Contexts;

namespace YayinEviApi.Persistence.Repositories.WarehouseR
{
    public class OrderItemDistributedRepository : GeneralRepository<OrderItemDistributed>, IOrderItemDistributedRepository
    {
        public OrderItemDistributedRepository(YayinEviApiDbContext context) : base(context)
        {
        }
    }
}
