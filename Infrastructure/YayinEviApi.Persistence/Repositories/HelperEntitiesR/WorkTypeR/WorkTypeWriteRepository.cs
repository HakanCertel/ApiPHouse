using YayinEviApi.Application.Repositories.IHelperEntitiesR.IWorkTypeR;
using YayinEviApi.Domain.Entities.HelperEntities;
using YayinEviApi.Persistence.Contexts;

namespace YayinEviApi.Persistence.Repositories.HelperEntitiesR
{
    public class WorkTypeWriteRepository : WriteRepository<WorkType>, IWorkTypeWriteRepository
    {
        public WorkTypeWriteRepository(YayinEviApiDbContext context) : base(context) { }
   
    }

}

