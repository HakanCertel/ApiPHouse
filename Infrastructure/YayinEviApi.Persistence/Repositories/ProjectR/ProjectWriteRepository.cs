using YayinEviApi.Application.Repositories.ProjectR;
using YayinEviApi.Domain.Entities.ProjectE;
using YayinEviApi.Persistence.Contexts;

namespace YayinEviApi.Persistence.Repositories.ProjectR
{
    public class ProjectWriteRepository : WriteRepository<Project>, IProjectWriteRepository
    {
        public ProjectWriteRepository(YayinEviApiDbContext context) : base(context)
        {
        }
    }
}
