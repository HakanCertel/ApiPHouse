using YayinEviApi.Application.Repositories.IProductionManagementR.IRecipeR;
using YayinEviApi.Domain.Entities.ProductionManagementE.RecipeE;
using YayinEviApi.Persistence.Contexts;

namespace YayinEviApi.Persistence.Repositories.ProductionManagementR.RecipeR
{
    public class RecipeItemsRepository : GeneralRepository<RecipeItems>, IRecipeItemsRepository
    {
        public RecipeItemsRepository(YayinEviApiDbContext context) : base(context)
        {
        }
    }
}
