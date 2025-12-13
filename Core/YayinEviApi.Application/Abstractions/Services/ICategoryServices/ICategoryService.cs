using YayinEviApi.Application.Repositories;
using YayinEviApi.Domain.Entities.Common;

namespace YayinEviApi.Application.Abstractions.Services.ICategoryServices
{
    public interface ICategoryService<TService,T> where TService : IGetNewCode<T> where T : BaseEntity
    {
    }
}
