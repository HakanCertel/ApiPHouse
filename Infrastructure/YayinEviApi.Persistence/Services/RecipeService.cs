using YayinEviApi.Application.Abstractions.Services;
using YayinEviApi.Application.DTOs.ProductionManagementDtos.RecipeDtos;
using YayinEviApi.Application.Repositories;
using YayinEviApi.Application.RequestParameters;
using YayinEviApi.Domain.Entities.MaterialE;
using YayinEviApi.Domain.Entities.ProductionManagementE.RecipeE;
using YayinEviApi.Domain.Entities.WarehouseE;
using YayinEviApi.Infrastructure.Operations;

namespace YayinEviApi.Persistence.Services
{
    public class RecipeService : IRecipeService
    {
        //readonly IRecipeRepository _repository;
        //readonly IRecipeItemsRepository _repositoryItems;
        readonly IUserService _userService;
        readonly IFileManagementReadRepository _fileManagementReadRepository;
        readonly IFileManagementWriteRepository _fileManagementWriteRepository;
        readonly IGetNewCode<Recipe> _generalRepository;
        readonly IGeneralRepository<RecipeItems> _generalItemsRepository;
        public RecipeService(IUserService userService, IFileManagementWriteRepository fileManagementWriteRepository, IFileManagementReadRepository fileManagementReadRepository, IGetNewCode<Recipe> generalRepository, IGeneralRepository<RecipeItems> generalItemsRepository)
        {
            _userService = userService;
            _fileManagementWriteRepository = fileManagementWriteRepository;
            _fileManagementReadRepository = fileManagementReadRepository;
            _generalRepository = generalRepository;
            _generalItemsRepository = generalItemsRepository;
        }

        public async Task<RecipeDto> Add(RecipeDto entity)
        {
            if (_generalRepository.Select(x => x.Code == entity.Code, x => x).Any())
            {
                entity.Code = _generalRepository.GetNewCodeAsync(entity.Serie, x => x.Code).Result?.ToString();
            }
            var recipe = entity.EntityCovert<Recipe>();

            await _generalRepository.AddAsync(recipe);
            await _generalRepository.SaveAsync();
            entity.Id=recipe.Id.ToString();
            return entity;
        }
        public async Task<RecipeDto> Edit(RecipeDto entity)
        {

            var currentEntity = entity.EntityCovert<Recipe>();
            _generalRepository.Update(currentEntity);

            await _generalRepository.SaveAsync();
            
            return entity;
        }
        public async Task Delete(string id)
        {
            var countItems = _generalItemsRepository.GetWhere(x => x.ParentId.ToString() == id);
            foreach (var item in countItems)
            {
                _generalItemsRepository.Remove(item);
            }

            await _generalItemsRepository.SaveAsync();
            await _generalRepository.RemoveAsync(id);
            await _generalRepository.SaveAsync();
        }

        public async Task<bool> AddItems(IEnumerable<RecipeItemsDto> entities)
        {
            var entityList = entities.EntityListConvert<RecipeItems>().ToList();

            await _generalItemsRepository.AddRangeAsync(entityList);
            await _generalItemsRepository.SaveAsync();
            return true;
        }

        public async Task<bool> EditItems(IEnumerable<RecipeItemsDto> entities)
        {
            var entityList = entities.EntityListConvert<RecipeItems>().ToList();

            _generalItemsRepository.UpdateRange(entityList);
            
            await _generalItemsRepository.SaveAsync();
            return false;
        }

        public async Task<bool> DeleteItems(string id)
        {
            await _generalItemsRepository.RemoveAsync(id);
            await _generalItemsRepository.SaveAsync();
            return true;
        }

        public async Task<IEnumerable<RecipeDto>> GetAll(Pagination? pagination)
        {
            var totalRecipeCount = _generalRepository.GetAll(false).Count();
            var recipeList = _generalRepository.Table.Select(x => new
            {
                recipe = x,
                material = x.Material != null ? x.Material : new Material(),
                cell = x.WarehouseCell != null ? x.WarehouseCell : new CellofWarehouse(),
                unit = x.Unit,
                file = _fileManagementReadRepository.Table.Where(y => y.EntityId == x.Id.ToString() && y.IsActive).FirstOrDefault(),
                materialCell = x.Material.CellofWarehouse,
                materialUnit = x.Material.Unit,
            }).ToList().Select(x => new RecipeDto
            {
                Id = x.recipe.Id.ToString(),
                Code = x.recipe.Code,
                Name = x.recipe.Name,
                UnitId = x.unit.Id.ToString(),
                UnitName = x.unit.Name,
                UnitCode = x.unit.Code,
                Quantity = x.recipe.Quantity,
                Default = x.recipe.Default,
                IsActive = x.recipe.IsActive,
                Description = x.recipe.Description,
                WarehouseCellId = x.cell.Id.ToString(),
                WarehouseCellName = x.cell.Name,
                MaterialId = x.material.Id.ToString(),
                MaterialCode = x.material.Code,
                MaterialName = x.material.Name,
                MaterialType = x.material.MaterialType.toName(),
                MaterialWarehouseCellId = x.materialCell.Id.ToString(),
                MaterialWarehouseCellName = x.materialCell.Name,
                MaterialUnitId = x.materialUnit.Id.ToString(),
                MaterialUnitName = x.materialUnit.Name,
                ImagePath = x.file != null ? x.file.Path : null,
            });
            int page = 0;
            int pageSize = totalRecipeCount;
            if (pagination != null)
            {
                page = pagination.Page;
                pageSize = pagination.Size;
            }
            var materials = recipeList.Select(x => x).Skip(page * pageSize).Take(pageSize);

            return materials;
        }
        public int TotalCount()
        {
            var count=  _generalRepository.GetAll(false).Count();
            return count;
        }
        public async Task<RecipeDto> GetByIdAsync(string? id)
        {
            var recipe = _generalRepository.Table.Where(x => x.Id.ToString() == id).Select(x => new
            {
                recipe = x,
                material = x.Material != null ? x.Material : new Material(),
                cell = x.WarehouseCell != null ? x.WarehouseCell : new CellofWarehouse(),
                unit = x.Unit,
                file = _fileManagementReadRepository.Table.Where(y => y.EntityId == x.Id.ToString() && y.IsActive).FirstOrDefault(),
                materialCell = x.Material.CellofWarehouse,
                materialUnit = x.Material.Unit,
            }).ToList().Select(x => new RecipeDto
            {
                Id = x.recipe.Id.ToString(),
                Code = x.recipe.Code,
                Name = x.recipe.Name,
                UnitId = x.unit.Id.ToString(),
                UnitName = x.unit.Name,
                UnitCode = x.unit.Code,
                Quantity = x.recipe.Quantity,
                Default = x.recipe.Default,
                IsActive = x.recipe.IsActive,
                Description = x.recipe.Description,
                WarehouseCellId = x.cell.Id.ToString(),
                WarehouseCellName = x.cell.Name,
                MaterialId = x.material.Id.ToString(),
                MaterialCode = x.material.Code,
                MaterialName = x.material.Name,
                MaterialType = x.material.MaterialType.toName(),
                MaterialWarehouseCellId = x.materialCell.Id.ToString(),
                MaterialWarehouseCellName = x.materialCell.Name,
                MaterialUnitId = x.materialUnit.Id.ToString(),
                MaterialUnitName = x.materialUnit.Name,
                ImagePath = x.file != null ? x.file.Path : null,
            }).FirstOrDefault();
            
            return recipe;

        }

        public async Task<IEnumerable<RecipeItemsDto>> GetAllItemsByParentId(string id)
        {
            var items = _generalItemsRepository.Table.Where(x => x.Id.ToString() == id).Select(x => new
            {
                item = x,
                parent = x.Parent,
                parentMaterial = x.Parent.Material,
                parentUnit = x.Parent.Unit,
                parentCell = x.Parent.WarehouseCell,
                unit = x.ConsumptionUnit,
                cell = x.ConsumptionWarehouseCell,
                material = x.Material,
                materialUnit = x.Material.Unit,
                materialCell = x.Material.CellofWarehouse,
                file = _fileManagementReadRepository.Table.Where(y => y.EntityId == x.MaterialId.ToString() && y.IsActive).FirstOrDefault(),

            }).ToList().Select(x => new RecipeItemsDto
            {
                Id = x.item.Id.ToString(),
                ConsumptionUnitId = x.unit.Id.ToString(),
                ConsumptionUnitCode = x.unit.Code,
                ConsumptionUnitName = x.unit.Name,
                ConsumptionWarehouseCellId = x.cell.Id.ToString(),
                ConsumptionWarehouseCellName = x.cell.Name,
                Quantity = x.item.Quantity,
                RateofOutage = x.item.RateofOutage,
                Description = x.item.Description,
                ImagePath = x.file != null ? x.file.Path : null,
                ParentId = x.parent.Id.ToString(),
                ParentCode = x.parent.Code,
                ParentName = x.parent.Name,
                ParentMaterialId = x.parentMaterial.Id.ToString(),
                ParentMaterialCode = x.parentMaterial.Code,
                ParentMaterialName = x.parentMaterial.Name,
                ParentMaterialUnitId = x.parentUnit.Id.ToString(),
                ParentMaterialUnitName = x.parentUnit.Name,
                ParentQuantity = x.parent.Quantity,
                ParentRateofOutage = x.parent.RateofOutage,
                MaterialId = x.material.Id.ToString(),
                MaterialCode = x.material.Code,
                MaterialName = x.material.Name,
                MaterialCellId = x.materialCell.Id.ToString(),
                MaterialCellCode = x.materialCell.Code,
                MaterialCellName = x.materialCell.Name,
                MaterialUnitId = x.materialUnit.Id.ToString(),
                MaterialUnitCode = x.materialUnit.Code,
                MaterialUnitName = x.materialUnit.Name,
            });

            return items;

        }



    }
}
