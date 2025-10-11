using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;
using YayinEviApi.Application.Repositories;
using YayinEviApi.Domain.Entities.Common;
using YayinEviApi.Persistence.Contexts;

namespace YayinEviApi.Persistence.Repositories
{
    public class GeneralRepository<T> : IGeneralRepository<T> where T : BaseEntity
    {
        private readonly YayinEviApiDbContext _context;

        public GeneralRepository(YayinEviApiDbContext context)
        {
            _context = context;
        }
        public DbSet<T> Table => _context.Set<T>();
        public async Task<bool> AddAsync(T model)
        {
            EntityEntry<T> entityEntry = await Table.AddAsync(model);
            return entityEntry.State == EntityState.Added;
        }
        public async Task<bool> AddRangeAsync(List<T> datas)
        {
            await Table.AddRangeAsync(datas);
            return true;
        }
        public bool Remove(T model)
        {
            EntityEntry<T> entityEntry = Table.Remove(model);
            return entityEntry.State == EntityState.Deleted;

        }
        public bool RemoveRange(List<T> datas)
        {
            Table.RemoveRange(datas);
            return true;
        }
        public async Task<bool> RemoveAsync(string id)
        {
            T model = await Table.FirstOrDefaultAsync(data => data.Id == Guid.Parse(id));
            return Remove(model);
        }
        public bool Update(T model)
        {
            EntityEntry entityEntry = Table.Update(model);
            return entityEntry.State == EntityState.Modified;
        }
        public async Task<int> SaveAsync()
            => await _context.SaveChangesAsync();

        public bool UpdateRange(List<T> datas)
        {
            Table.UpdateRange(datas);
            return true;
        }



        public IQueryable<T> GetAll(bool tracking = true)
        {
            var query = Table.AsQueryable();
            if (!tracking)
                query = query.AsNoTracking();
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
            if (!tracking)
                query = query.AsNoTracking();
            return await query.FirstOrDefaultAsync(method);
        }
        public async Task<T> GetByIdAsync(string id, bool tracking = true)
        //=>await Table.FirstOrDefaultAsync(data=>data.Id==Guid.Parse(id));
        //=> await Table.FindAsync(Guid.Parse(id));
        {
            var query = Table.AsQueryable();
            if (!tracking)
                query = query.AsNoTracking();
            return await query.FirstOrDefaultAsync(data => data.Id == Guid.Parse(id));
        }

        public Task<TResult> FindAsync<TResult>(Expression<Func<T, bool>> filter, Expression<Func<T, TResult>> selector, bool tracking = true)
        {
            return filter == null ? Table.Select(selector)?.FirstOrDefaultAsync() : Table.Where(filter).Select(selector)?.FirstOrDefaultAsync();
        }

        public IQueryable<TResult> Select<TResult>(Expression<Func<T, bool>> filter, Expression<Func<T, TResult>> selector)
        {
            return filter == null ? Table.Select(selector) : Table.Where(filter).Select(selector);
        }
    }
}
