using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using YayinEviApi.Application.Repositories.IMaterialR;
using YayinEviApi.Domain.Entities.MaterialE;
using YayinEviApi.Persistence.Contexts;

namespace YayinEviApi.Persistence.Repositories.MaterialR
{
    public class MaterialRepository : GetNewCodeRepository<Material>, IMaterialRepository
    {
      
        public MaterialRepository(YayinEviApiDbContext context) : base(context)
        {
        }
    }
}
