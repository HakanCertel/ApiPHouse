using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YayinEviApi.Application.Repositories;
using YayinEviApi.Domain.Entities;
using YayinEviApi.Persistence.Contexts;

namespace YayinEviApi.Persistence.Repositories.ProductImageFileR
{
    public class ProductImageFileReadRepository : ReadRepository<ProductImageFile>, IProductImageFileReadRepository
    {
        public ProductImageFileReadRepository(YayinEviApiDbContext context) : base(context)
        {
        }
    }
}
