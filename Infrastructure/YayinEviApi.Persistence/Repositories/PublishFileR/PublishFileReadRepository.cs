using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YayinEviApi.Application.Repositories;
using YayinEviApi.Domain.Entities;
using YayinEviApi.Persistence.Contexts;

namespace YayinEviApi.Persistence.Repositories.PublishFileR
{
    public class PublishFileReadRepository : ReadRepository<PublishFile>, IPublishFileReadRepository
    {
        public PublishFileReadRepository(YayinEviApiDbContext context) : base(context)
        {
        }
    }
}
