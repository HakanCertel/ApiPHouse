using YayinEviApi.Application.Repositories.IHelperEntitiesR.IDepartmentR;
using YayinEviApi.Domain.Entities.HelperEntities;
using YayinEviApi.Persistence.Contexts;

namespace YayinEviApi.Persistence.Repositories.HelperEntitiesR.DepartmentR
{
    public class DepartmentReadRepository : ReadRepository<Department>,IDepartmentReadRepository
    {
        public DepartmentReadRepository(YayinEviApiDbContext context) : base(context)
        {
        }
    }
}
