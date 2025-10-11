using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using YayinEviApi.Application.Repositories;
using YayinEviApi.Domain.Entities.Common;
using YayinEviApi.Persistence.Contexts;

namespace YayinEviApi.Persistence.Repositories
{
    public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity
    {
        private readonly YayinEviApiDbContext _context;

        public ReadRepository(YayinEviApiDbContext context)
        {
            _context = context;
        }
        public DbSet<T> Table => _context.Set<T>();

        public IQueryable<T> GetAll(bool tracking = true)
        {
            var query=Table.AsQueryable();
            if(!tracking)
                query=query.AsNoTracking();
            return query;
        }
            

        public IQueryable<T> GetWhere(Expression<Func<T, bool>> method, bool tracking = true)
        {
            var query = Table.Where(method).AsQueryable();
            if (!tracking)
                query = query.AsNoTracking();
            return query;
        }
        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> method, bool tracking = true)
        //=> await Table.FirstOrDefaultAsync(method);
        {
            var query = Table.AsQueryable();
            if(!tracking)
                query = query.AsNoTracking();
            return await query.FirstOrDefaultAsync(method);
        }
        public async Task<T> GetByIdAsync(string id, bool tracking = true)
        //=>await Table.FirstOrDefaultAsync(data=>data.Id==Guid.Parse(id));
        //=> await Table.FindAsync(Guid.Parse(id));
        {
            var query = Table.AsQueryable();
            if(!tracking)
                query = query.AsNoTracking();
            return await query.FirstOrDefaultAsync(data=>data.Id==Guid.Parse(id));
        }

        public  Task<TResult>  FindAsync<TResult>(Expression<Func<T, bool>> filter, Expression<Func<T, TResult>> selector, bool tracking = true)
        {
            return  filter == null ? Table.Select(selector)?.FirstOrDefaultAsync() : Table.Where(filter).Select(selector)?.FirstOrDefaultAsync();
        }

        public  IQueryable<TResult> Select<TResult>(Expression<Func<T, bool>> filter, Expression<Func<T, TResult>> selector)
        {
            return  filter == null ? Table.Select(selector) : Table.Where(filter).Select(selector);
        }
    }
}
