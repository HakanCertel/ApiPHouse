using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using YayinEviApi.Domain.Entities.Common;
using YayinEviApi.Persistence.Contexts;

namespace YayinEviApi.Persistence.Repositories
{
    public class BaseReadRepository<T> : ReadRepository<T> where T : BaseEntity 
    {
        public BaseReadRepository(YayinEviApiDbContext context) : base(context)
        {
        }
        public virtual IEnumerable<BaseEntity> List(Expression<Func<T, bool>> filter)
        {
            return Select(filter, x => x);
        }
        public virtual async Task<BaseEntity> Single(Expression<Func<T, bool>> filter)
        {
            return await FindAsync(filter, x => x);
        }

    }
}
