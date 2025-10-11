using YayinEviApi.Application.Repositories.IUnitR;
using YayinEviApi.Domain.Entities.UnitE;
using YayinEviApi.Persistence.Contexts;

namespace YayinEviApi.Persistence.Repositories.UnitR
{
    public class UnitRepository : GeneralRepository<MaterialUnit>,IUnitRpository
    {
        public UnitRepository(YayinEviApiDbContext context) : base(context)
        {
        }
    }
}
