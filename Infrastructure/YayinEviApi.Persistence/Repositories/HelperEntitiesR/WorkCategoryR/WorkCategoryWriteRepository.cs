using YayinEviApi.Application.Repositories.IHelperEntitiesR.IWorkCategoryR;
using YayinEviApi.Domain.Entities.HelperEntities;
using YayinEviApi.Persistence.Contexts;

namespace YayinEviApi.Persistence.Repositories.HelperEntitiesR.WorkCategoryR
{
    public class WorkCategoryWriteRepository : WriteRepository<WorkCategory>,IWorkCategoryWriteRepository
    {
        public WorkCategoryWriteRepository(YayinEviApiDbContext context) : base(context)
        {
        }
    }
}
