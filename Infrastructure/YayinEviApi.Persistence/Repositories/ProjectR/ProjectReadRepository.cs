using YayinEviApi.Application.Repositories.ProjectR;
using YayinEviApi.Domain.Entities.ProjectE;
using YayinEviApi.Persistence.Contexts;

namespace YayinEviApi.Persistence.Repositories.ProjectR
{
    public class ProjectReadRepository : ReadRepository<Project>, IProjectReadRepository
    {
        public ProjectReadRepository(YayinEviApiDbContext context) : base(context)
        {
        }
    }
}
