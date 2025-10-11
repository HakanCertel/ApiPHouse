using YayinEviApi.Application.Repositories.ISaleR;
using YayinEviApi.Domain.Entities.SalesE;
using YayinEviApi.Persistence.Contexts;

namespace YayinEviApi.Persistence.Repositories.SaleR
{
    public class SaleItemRepository : GeneralRepository<SaleItem>, ISaleItemRepository
    {
        public SaleItemRepository(YayinEviApiDbContext context) : base(context)
        {
        }
    }
}
