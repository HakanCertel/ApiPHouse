using YayinEviApi.Application.Repositories.IHelperEntitiesR.IDepartmentR;
using YayinEviApi.Domain.Entities.HelperEntities;
using YayinEviApi.Persistence.Contexts;

namespace YayinEviApi.Persistence.Repositories.HelperEntitiesR.DepartmentR
{
    public class DepartmentWriteRepository : WriteRepository<Department>,IDepartmentWriteRepository
    {
        public DepartmentWriteRepository(YayinEviApiDbContext context) : base(context)
        {
        }
    }
}
