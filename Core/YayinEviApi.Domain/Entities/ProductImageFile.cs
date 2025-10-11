using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YayinEviApi.Domain.Entities
{
    public class ProductImageFile:FileManagement
    {
        public bool Showcase { get; set; }
        public ICollection<Product> Products { get; set; }

    }
}
