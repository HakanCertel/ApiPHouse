using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using YayinEviApi.Domain.Entities.Common;

namespace YayinEviApi.Application.Repositories
{
    public interface IReadRepository<T>:IRepository<T> where T : BaseEntity
    {
        IQueryable<T> GetAll(bool tracking=true);
        IQueryable<T> GetWhere(Expression<Func<T,bool>> method, bool tracking = true);
        Task<T> GetSingleAsync(Expression<Func<T,bool>> method, bool tracking = true);
        Task<T> GetByIdAsync(string id, bool tracking = true);
        Task<TResult> FindAsync<TResult>(Expression<Func<T, bool>> filter, Expression<Func<T, TResult>> selector, bool tracking = true);
        IQueryable<TResult> Select<TResult>(Expression<Func<T, bool>> filter, Expression<Func<T, TResult>> selector);
    }
}
