using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YayinEviApi.Application.Repositories.IAuthorR;
using YayinEviApi.Domain.Entities;
using YayinEviApi.Persistence.Contexts;

namespace YayinEviApi.Persistence.Repositories.AuthorR
{
    public class AuthorWriteRepository : WriteRepository<Author>, IAuthorWriteRepository
    {
        public AuthorWriteRepository(YayinEviApiDbContext context) : base(context)
        {
        }
    }

   
}
