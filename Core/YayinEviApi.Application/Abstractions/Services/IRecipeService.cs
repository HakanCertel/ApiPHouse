using YayinEviApi.Application.DTOs.ProductionManagementDtos.RecipeDtos;
using YayinEviApi.Domain.Entities.Common;
using YayinEviApi.Domain.Interfaces;

namespace YayinEviApi.Application.Abstractions.Services
{
    public interface IRecipeService : IBaseService<RecipeDto> ,IBaseItemsService<RecipeItemsDto> 
    {
       
    }
}
