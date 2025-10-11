using YayinEviApi.Application.Repositories.IWarehouseR;
using YayinEviApi.Domain.Entities.WarehouseE;
using YayinEviApi.Persistence.Contexts;

namespace YayinEviApi.Persistence.Repositories.WarehouseR
{
    public class StockCountItemsRepository : GeneralRepository<StockCountItems>,IStockCountItemsRepository
    {
        public StockCountItemsRepository(YayinEviApiDbContext context) : base(context)
        {
        }
    }
}
