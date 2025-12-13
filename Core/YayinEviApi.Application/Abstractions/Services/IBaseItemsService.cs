using YayinEviApi.Domain.Entities.Common;
using YayinEviApi.Domain.Interfaces;

namespace YayinEviApi.Application.Abstractions.Services
{
    public interface IBaseItemsService<T> where T : IBaseEntity
    {
        Task<bool>AddItems(IEnumerable<T> entities);
        Task<bool> EditItems(IEnumerable<T> entities);
        Task<bool> DeleteItems(string id);
        Task<IEnumerable<T>> GetAllItemsByParentId(string id);
    }
}
