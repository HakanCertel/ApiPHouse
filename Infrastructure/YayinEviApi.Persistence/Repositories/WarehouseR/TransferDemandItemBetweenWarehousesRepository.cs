using YayinEviApi.Application.Repositories.IWarehouseR;
using YayinEviApi.Domain.Entities.WarehouseE;
using YayinEviApi.Persistence.Contexts;

namespace YayinEviApi.Persistence.Repositories.WarehouseR
{
    public class TransferDemandItemBetweenWarehousesRepository : GeneralRepository<TransferDemandItemBetweenWarehouses>,ITransferDemandItemBetweenWarehousesRepository
    {
        public TransferDemandItemBetweenWarehousesRepository(YayinEviApiDbContext context) : base(context)
        {
        }
    }
}
