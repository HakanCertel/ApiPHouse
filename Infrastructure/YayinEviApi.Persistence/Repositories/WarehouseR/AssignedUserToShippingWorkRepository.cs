using YayinEviApi.Application.Repositories.IWarehouseR;
using YayinEviApi.Domain.Entities.WarehouseE;
using YayinEviApi.Persistence.Contexts;

namespace YayinEviApi.Persistence.Repositories.WarehouseR
{
    public class AssignedUserToShippingWorkRepository : GeneralRepository<AssignedUserToShippingWork>, IAssignedUserToShippingWorkRepository
    {
        public AssignedUserToShippingWorkRepository(YayinEviApiDbContext context) : base(context)
        {
        }
    }
}
