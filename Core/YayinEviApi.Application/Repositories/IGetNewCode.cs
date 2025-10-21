using System.Linq.Expressions;
using YayinEviApi.Domain.Entities.Common;

namespace YayinEviApi.Application.Repositories
{
    public interface IGetNewCode<T>:IGeneralRepository<T> where T:BaseEntity
    {
        Task<string> GetNewCodeAsync(string serie,Expression<Func<T,string>> filter);
    }
}
