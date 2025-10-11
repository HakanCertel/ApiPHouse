using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YayinEviApi.Domain.Entities.Common;

namespace YayinEviApi.Domain.Entities.Şube
{
    public class Sube:BaseEntity
    {
        public string  Code{ get; set; } 
        public string  Name { get; set; } 
    }
}
