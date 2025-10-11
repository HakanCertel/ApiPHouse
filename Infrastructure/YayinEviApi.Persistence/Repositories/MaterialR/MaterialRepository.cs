using YayinEviApi.Application.Repositories.IMaterialR;
using YayinEviApi.Domain.Entities.MaterialE;
using YayinEviApi.Persistence.Contexts;

namespace YayinEviApi.Persistence.Repositories.MaterialR
{
    public class MaterialRepository : GeneralRepository<Material>, IMaterialRepository
    {
        public MaterialRepository(YayinEviApiDbContext context) : base(context)
        {
        }
    }
}
