using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using YayinEviApi.Application.DTOs.HelperEntityDtos;
using YayinEviApi.Application.Repositories.IHelperEntitiesR.IWorkCategoryR;
using YayinEviApi.Application.Repositories.IHelperEntitiesR.IWorkTypeR;
using YayinEviApi.Domain.Entities.Common;
using YayinEviApi.Domain.Entities.HelperEntities;
using YayinEviApi.Persistence.Contexts;

namespace YayinEviApi.Persistence.Repositories.HelperEntitiesR
{
    public class WorkTypeReadRepository : BaseReadRepository<WorkType>, IWorkTypeReadRepository
    {
        public WorkTypeReadRepository(YayinEviApiDbContext context) : base(context)
        {
        }
        public override async Task<BaseEntity> Single(Expression<Func<WorkType, bool>> filter)
        {
            return await FindAsync(filter, x => new WorkType
            {
                Id = x.Id,
                TypeName = x.TypeName,
                TypeCode = x.TypeCode,
                Description = x.Description,
            });
        }
        public override IEnumerable<BaseEntity> List(Expression<Func<WorkType, bool>> filter)
        {
            return Select(filter,x=>x);
        }
    }
}