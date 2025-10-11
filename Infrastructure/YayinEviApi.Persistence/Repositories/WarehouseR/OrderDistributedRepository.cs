using YayinEviApi.Application.Repositories.IWarehouseR;
using YayinEviApi.Domain.Entities.WarehouseE;
using YayinEviApi.Persistence.Contexts;

namespace YayinEviApi.Persistence.Repositories.WarehouseR
{
    public class OrderDistributedRepository : GeneralRepository<OrderDistributed>, IOrderDistributedRepository
    {
        public OrderDistributedRepository(YayinEviApiDbContext context) : base(context)
        {
        }
    }
}
