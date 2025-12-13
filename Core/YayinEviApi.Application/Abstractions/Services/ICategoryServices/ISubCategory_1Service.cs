using YayinEviApi.Application.Repositories;
using YayinEviApi.Domain.Entities.CategoriesE;
using YayinEviApi.Domain.Entities.Common;

namespace YayinEviApi.Application.Abstractions.Services.ICategoryServices
{
    public interface ISubCategory_1Service<T> : IGetNewCode<T> where T : BaseEntity
    {
    }
}
