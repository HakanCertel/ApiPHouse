using System.Linq.Expressions;
using YayinEviApi.Application.DTOs.AuthorDto;
using YayinEviApi.Application.Repositories.IAuthorR;
using YayinEviApi.Domain.Entities;
using YayinEviApi.Domain.Entities.Common;
using YayinEviApi.Persistence.Contexts;

namespace YayinEviApi.Persistence.Repositories.AuthorR
{
    public class AuthorReadRepository : BaseReadRepository<Author>, IAuthorReadRepository
    {
        public AuthorReadRepository(YayinEviApiDbContext context) : base(context)
        {
        }
        public override IEnumerable<BaseEntity> List(Expression<Func<Author, bool>> filter)
        {
            return base.List(filter);
        }
    }
}
