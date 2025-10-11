using YayinEviApi.Application.Repositories;
using YayinEviApi.Domain.Entities;
using YayinEviApi.Persistence.Contexts;

namespace YayinEviApi.Persistence.Repositories.PublishFileR
{
    public class PublishFileWriteRepository : WriteRepository<PublishFile>, IPublishFileWriteRepository
    {
        public PublishFileWriteRepository(YayinEviApiDbContext context) : base(context)
        {
        }
    }
}
