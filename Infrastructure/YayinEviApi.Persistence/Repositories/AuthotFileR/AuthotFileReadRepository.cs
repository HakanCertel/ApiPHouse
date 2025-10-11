using YayinEviApi.Application.Repositories.IAuthotFileR;
using YayinEviApi.Domain.Entities;
using YayinEviApi.Persistence.Contexts;

namespace YayinEviApi.Persistence.Repositories.AuthotFileR
{
    public class AuthorFileReadRepository : ReadRepository<AuthorFile>, IAuthorFileReadRepository
    {
        public AuthorFileReadRepository(YayinEviApiDbContext context) : base(context)
        {
        }
    }
}
