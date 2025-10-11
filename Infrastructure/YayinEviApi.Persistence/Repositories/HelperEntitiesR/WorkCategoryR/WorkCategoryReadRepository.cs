using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using YayinEviApi.Application.DTOs.HelperEntityDtos;
using YayinEviApi.Application.Repositories.IHelperEntitiesR.IWorkCategoryR;
using YayinEviApi.Domain.Entities.Common;
using YayinEviApi.Domain.Entities.HelperEntities;
using YayinEviApi.Persistence.Contexts;

namespace YayinEviApi.Persistence.Repositories.HelperEntitiesR.WorkCategoryR
{
    public class WorkCategoryReadRepository : BaseReadRepository<WorkCategory>,IWorkCategoryReadRepository
    {
        public WorkCategoryReadRepository(YayinEviApiDbContext context) : base(context)
        {
        }
        public override async Task<BaseEntity> Single(Expression<Func<WorkCategory, bool>> filter)
        {
            return await FindAsync(filter, x=> new WorkCategoryS
            {
                Id = x.Id,
                Name = x.Name,
                Code = x.Code,
                Description = x.Description,
                WorkTypeId = x.WorkTypeId,
                WorkTypeName=x.WorkType.TypeName,
            });
        }
        //public override IEnumerable<BaseEntity> List(Expression<Func<WorkCategory, bool>> filter)
        //{
        //    return Select(filter, x => new WorkCategoryL
        //    {
        //        Id = x.Id.ToString(),
        //        Name = x.Name,
        //        Code = x.Code,
        //        Description = x.Description,
        //        WorkTypeId = x.WorkTypeId.ToString(),
        //        WorkTypeName=x.WorkType.TypeName,
        //    }).ToList();
        //}
    }
}
