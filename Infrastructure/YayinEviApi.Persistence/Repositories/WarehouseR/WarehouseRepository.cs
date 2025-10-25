using YayinEviApi.Application.Repositories.IWarehouseR;
using YayinEviApi.Domain.Entities.WarehouseE;
using YayinEviApi.Persistence.Contexts;

namespace YayinEviApi.Persistence.Repositories.WarehouseR
{
    public class WarehouseRepository : GetNewCodeRepository<Warehouse>, IWarehouseRepository
    {
        public WarehouseRepository(YayinEviApiDbContext context) : base(context)
        {
        }
    }
}
