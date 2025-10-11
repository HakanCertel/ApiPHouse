using YayinEviApi.Application.Repositories.IWarehouseR;
using YayinEviApi.Domain.Entities.WarehouseE;
using YayinEviApi.Persistence.Contexts;

namespace YayinEviApi.Persistence.Repositories.WarehouseR
{
    public class StockRepository : GeneralRepository<Stock>, IStockRepository
    {
        public StockRepository(YayinEviApiDbContext context) : base(context)
        {
        }
    }
}
