using YayinEviApi.Application.Repositories.IWarehouseR;
using YayinEviApi.Domain.Entities.WarehouseE;
using YayinEviApi.Persistence.Contexts;

namespace YayinEviApi.Persistence.Repositories.WarehouseR
{
    public class TransferDemandBetweenWarehousesRepository : GetNewCodeRepository<TransferDemandBetweenWarehouses>,ITransferDemandBetweenWarehousesRepository
    {
        public TransferDemandBetweenWarehousesRepository(YayinEviApiDbContext context) : base(context)
        {
        }
    }
}
