using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YayinEviApi.Application.Abstractions.Services;
using YayinEviApi.Application.Abstractions.Storage;
using YayinEviApi.Application.DTOs.ProductionManagementDtos.RecipeDtos;
using YayinEviApi.Application.DTOs.User;
using YayinEviApi.Application.Repositories;
using YayinEviApi.Application.Repositories.IProductionManagementR.IRecipeR;
using YayinEviApi.Application.RequestParameters;
using YayinEviApi.Domain.Entities.MaterialE;
using YayinEviApi.Domain.Entities.ProductionManagementE.RecipeE;
using YayinEviApi.Domain.Entities.WarehouseE;
using YayinEviApi.Infrastructure.Operations;

namespace YayinEviApi.API.Controllers.ProductionManagementControllers.RecipeControllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")]
    public class RecipeController : ControllerBase
    {
        readonly IFileManagementReadRepository _fileManagementReadRepository;
        readonly IFileManagementWriteRepository _fileManagementWriteRepository;
        readonly IRecipeRepository _recipeRepository;
        readonly IRecipeItemsRepository _recipeItemsRepository;
        readonly IStorageService _storageService;
        readonly IRecipeService _recipeService;
        private CreateUser _user;
        private IUserService _userService;
        private RecipeDto _oldEntity;
        public RecipeController(IRecipeItemsRepository recipeItemsRepository, IRecipeRepository recipeRepository, IFileManagementWriteRepository fileManagementWriteRepository, IFileManagementReadRepository fileManagementReadRepository, IStorageService storageService, IUserService userService, IRecipeService recipeService)
        {
            _recipeItemsRepository = recipeItemsRepository;
            _recipeRepository = recipeRepository;
            _fileManagementWriteRepository = fileManagementWriteRepository;
            _fileManagementReadRepository = fileManagementReadRepository;
            _storageService = storageService;
            _recipeService = recipeService;
            _userService = userService;

            _user = _userService.GetUser().Result;
        }

        [HttpPost()]
        public async Task<IActionResult> Add(RecipeDto recipe)
        {
            var result=await _recipeService.Add(recipe);
            
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Edit(RecipeDto recipe)
        {
            await _recipeService.Edit(recipe);

            return Ok(recipe);
        }
        
        [HttpDelete("{Id}")]
        [Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> Delete(string id)
        {
           await _recipeService.Delete(id);

            return Ok();
        }

        [HttpPost(("[action]"))]
        public async Task<IActionResult> AddItems(List<RecipeItemsDto> recipeItems)
        {
           
            var result=await _recipeService.AddItems(recipeItems);

            return Ok(recipeItems);
        }

        [HttpPut(("[action]"))]
        public async Task<IActionResult> EditItems(List<RecipeItemsDto> recipeItems)
        {

            await _recipeService.EditItems(recipeItems);

            return Ok(recipeItems);
        }


        [HttpDelete("[action]/{Id}")]
        [Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> DeleteItem(string id)
        {
            _recipeService.DeleteItems(id);
            return Ok();
        }
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] Pagination? pagination)
        {
            var totalRecipeCount = _recipeService.TotalCount();

            var materials = _recipeService.GetAll(pagination);
            
            return Ok(new { totalRecipeCount, materials });

        }

        [HttpGet("[action]/{id?}")]
        public async Task<IActionResult> GetById(string? id)
        {
            return Ok(_recipeService.GetByIdAsync(id));
        }

        [HttpGet("[action]/{id?}")]
        public async Task<IActionResult> GetAllByParentId(string? id)
        {
            var items = await _recipeService.GetAllItemsByParentId(id);

            return Ok(items);
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetNewCode(string serie = "RCP")
        {
            var newCode = await _recipeRepository.GetNewCodeAsync(serie, x => x.Code);

            return Ok(new { newCode });
        }
    }
}
