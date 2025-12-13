using YayinEviApi.Application.Repositories.IProductionManagementR.IRecipeR;
using YayinEviApi.Domain.Entities.ProductionManagementE.RecipeE;
using YayinEviApi.Persistence.Contexts;

namespace YayinEviApi.Persistence.Repositories.ProductionManagementR.RecipeR
{
    public class RecipeRepository : GetNewCodeRepository<Recipe>, IRecipeRepository
    {
        public RecipeRepository(YayinEviApiDbContext context) : base(context)
        {
        }
    }
}
