using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YayinEviApi.Domain.Entities.MaterialE
{
    public class MaterialFile:FileManagement
    {
        public bool Showcase { get; set; }
        public ICollection<Material>? Materials { get; set; }
    }
}
