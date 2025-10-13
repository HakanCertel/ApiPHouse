using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YayinEviApi.Domain.Entities.AgencyE;
using YayinEviApi.Domain.Entities.Common;
using YayinEviApi.Domain.Enum;

namespace YayinEviApi.Domain.Entities
{
    public class Author:BaseEntity
    {
        public Guid? AgencyId { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Language { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public Gender Gender{ get; set; }=Gender.Female;
        public string? CreatingUserId { get; set; }
        public string? UpdatingUserId { get; set; }

        public Agency Agency { get; set; }
        public ICollection<AuthorFile> AuthorFiles { get; set; }

    }
}
