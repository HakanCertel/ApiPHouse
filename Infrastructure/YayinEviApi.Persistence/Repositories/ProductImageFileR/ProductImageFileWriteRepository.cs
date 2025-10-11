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
    public class ProductImageFileWritedRepository : WriteRepository<ProductImageFile>, IProductImageFileWriteRepository
    {
        public ProductImageFileWritedRepository (YayinEviApiDbContext context) : base(context)
        {
        }
    }
}
