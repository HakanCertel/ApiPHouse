using YayinEviApi.Application.Repositories.ISaleR;
using YayinEviApi.Domain.Entities.SalesE;
using YayinEviApi.Persistence.Contexts;

namespace YayinEviApi.Persistence.Repositories.SaleR
{
    public class SaleRepository : GeneralRepository<Sale>, ISaleRepository
    {
        public SaleRepository(YayinEviApiDbContext context) : base(context)
        {
        }
    }
}
