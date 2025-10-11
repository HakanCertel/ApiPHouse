using YayinEviApi.Application.Repositories.IWarehouseR;
using YayinEviApi.Domain.Entities.WarehouseE;
using YayinEviApi.Persistence.Contexts;

namespace YayinEviApi.Persistence.Repositories.WarehouseR
{
    public class HallofWarehouseRepository : GeneralRepository<HallofWarehouse>, IHallofWarehouseRepository
    {
        public HallofWarehouseRepository(YayinEviApiDbContext context) : base(context)
        {
        }
    }
}
