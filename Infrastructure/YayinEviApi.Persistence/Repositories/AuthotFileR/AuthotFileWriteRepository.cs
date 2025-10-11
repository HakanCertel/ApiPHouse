using YayinEviApi.Application.Repositories.IAuthotFileR;
using YayinEviApi.Domain.Entities;
using YayinEviApi.Persistence.Contexts;

namespace YayinEviApi.Persistence.Repositories.AuthotFileR
{
    public class AuthorFileWriteRepository : WriteRepository<AuthorFile>, IAuthorFileWriteRepository
    {
        public AuthorFileWriteRepository(YayinEviApiDbContext context) : base(context)
        {
        }
    }
}
