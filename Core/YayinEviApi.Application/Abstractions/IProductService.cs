using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YayinEviApi.Domain.Entities;

namespace YayinEviApi.Application.Abstractions
{
    public interface IProductService
    {
        List<Product> GetProducts();
    }
}
