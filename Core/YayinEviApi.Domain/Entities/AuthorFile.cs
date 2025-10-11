using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YayinEviApi.Domain.Entities.WorkE;

namespace YayinEviApi.Domain.Entities
{
    public class AuthorFile:FileManagement
    {
        public bool Showcase { get; set; }
        public ICollection<Author>? Authors { get; set; }

    }
}
