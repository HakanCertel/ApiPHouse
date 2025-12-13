using YayinEviApi.Application.RequestParameters;
using YayinEviApi.Domain.Interfaces;

namespace YayinEviApi.Application.Abstractions.Services
{
    public interface IBaseService<T> where T : IBaseEntity
    {
        Task<IEnumerable<T>> GetAll(Pagination? pagination);
        Task<T> GetByIdAsync(string? id);
        int TotalCount();
        Task<T> Add(T entity);
        Task<T> Edit(T entity);
        Task Delete(string id);
      
    }
}
