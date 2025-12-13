using YayinEviApi.Application.DTOs.MaterialDtos;
using YayinEviApi.Domain.Entities.CategoriesE;

namespace YayinEviApi.Application.Abstractions.Services
{
    public interface IMaterialService:IBaseService<MaterailDto>
    {
        Task<IEnumerable<MainCategory>> GetAllMainCategory();
    }
}
