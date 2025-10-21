using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YayinEviApi.Domain.Entities.Common;
using YayinEviApi.Domain.Entities.HelperEntities.ProccessE;

namespace YayinEviApi.Domain.Entities.WorkE
{
    public class ProccessForWork:BaseEntity
    {
        public Guid WorkId { get; set; }
        public Guid ProccessId { get; set; }
        public string UserId { get; set; }

        public Work Work { get; set; }
        public Proccess Proccess { get; set; }

        [NotMapped]
        public override string Code { get; set; }
    }
}
