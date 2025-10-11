using YayinEviApi.Application.Repositories.IWarehouseR;
using YayinEviApi.Domain.Entities.WarehouseE;
using YayinEviApi.Persistence.Contexts;

namespace YayinEviApi.Persistence.Repositories.WarehouseR
{
    public class StockMovementRepository : GeneralRepository<StockMovement>, IStockMovementRepository
    {
        public StockMovementRepository(YayinEviApiDbContext context) : base(context)
        {
        }
    }
}
