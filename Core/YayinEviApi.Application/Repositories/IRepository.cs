using Microsoft.EntityFrameworkCore;
using YayinEviApi.Domain.Entities.Common;

namespace YayinEviApi.Application.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        DbSet<T> Table { get; }
    }
}
