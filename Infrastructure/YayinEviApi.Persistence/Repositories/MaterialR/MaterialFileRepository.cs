using YayinEviApi.Application.Repositories.IMaterialR;
using YayinEviApi.Domain.Entities.MaterialE;
using YayinEviApi.Persistence.Contexts;

namespace YayinEviApi.Persistence.Repositories.MaterialR
{
    public class MaterialFileRepository : GeneralRepository<MaterialFile>, IMaterialFileRepository
    {
        public MaterialFileRepository(YayinEviApiDbContext context) : base(context)
        {
        }
    }
}
