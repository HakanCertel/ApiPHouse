using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YayinEviApi.Domain.Entities.Common;

namespace YayinEviApi.Domain.Entities.HelperEntities.ProccessCategoryE
{
    public class ProccessCategory:BaseEntity
    {
        public string? Code { get; set; }
        public string Name { get; set; }
        public string? CreatingUserId { get; set; }
        public string? UpdatingUserId { get; set; }
    }
}
