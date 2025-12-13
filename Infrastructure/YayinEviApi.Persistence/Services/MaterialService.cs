using YayinEviApi.Application.Abstractions.Services;
using YayinEviApi.Application.DTOs.MaterialDtos;
using YayinEviApi.Application.Repositories;
using YayinEviApi.Application.RequestParameters;
using YayinEviApi.Domain.Entities.CategoriesE;
using YayinEviApi.Domain.Entities.MaterialE;
using YayinEviApi.Domain.Enum;
using YayinEviApi.Infrastructure.Operations;

namespace YayinEviApi.Persistence.Services
{
    public class MaterialService : IMaterialService
    {
        readonly IGetNewCode<Material> _generalRepository;
        readonly IGetNewCode<MainCategory> _mainCategoryRepository;
        readonly IFileManagementReadRepository _fileManagementReadRepository;
        readonly IFileManagementWriteRepository _fileManagementWriteRepository;
        public MaterialService(IGetNewCode<Material> generalRepository, IGetNewCode<MainCategory> mainCategoryRepository)
        {
            _generalRepository = generalRepository;
            _mainCategoryRepository = mainCategoryRepository;
        }

        public async Task<MaterailDto> Add(MaterailDto entity)
        {
            if (_generalRepository.Select(x => x.Code == entity.Code, x => x).Any())
            {
                entity.Code = _generalRepository.GetNewCodeAsync(entity.Serie, x => x.Code).Result?.ToString();
            }
            var material = entity.EntityCovert<Material>();

            await _generalRepository.AddAsync(material);

            await _generalRepository.SaveAsync();
            entity.Id = material.Id.ToString();
            return entity;
        }

        public Task Delete(string id)
        {
            throw new NotImplementedException();
        }

        public Task<MaterailDto> Edit(MaterailDto entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<MaterailDto>> GetAll(Pagination? pagination)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<MainCategory>> GetAllMainCategory()
        {
            var categories=  _mainCategoryRepository.GetWhere(x=>x.EntityType=="Malzeme".GetEnum<EntityType>());
            return categories;
        }

        public Task<MaterailDto> GetByIdAsync(string? id)
        {
            throw new NotImplementedException();
        }

        public int TotalCount()
        {
            throw new NotImplementedException();
        }
    }
}
