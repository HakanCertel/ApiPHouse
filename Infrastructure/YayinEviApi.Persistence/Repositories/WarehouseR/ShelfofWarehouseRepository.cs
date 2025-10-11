using YayinEviApi.Application.Repositories.IWarehouseR;
using YayinEviApi.Domain.Entities.WarehouseE;
using YayinEviApi.Persistence.Contexts;

namespace YayinEviApi.Persistence.Repositories.WarehouseR
{
    public class ShelfofWarehouseRepository : GeneralRepository<ShelfofWarehouse>,IShelfofWarehouseRepository
    {
        public ShelfofWarehouseRepository(YayinEviApiDbContext context) : base(context)
        {
        }
    }
}
