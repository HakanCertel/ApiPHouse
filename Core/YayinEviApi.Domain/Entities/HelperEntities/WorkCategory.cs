using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YayinEviApi.Domain.Entities.Common;

namespace YayinEviApi.Domain.Entities.HelperEntities
{
    public class WorkCategory:BaseEntity
    {
        public Guid WorkTypeId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public WorkType WorkType { get; set; }

    }
}
