using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YayinEviApi.Domain.Entities
{
    public class InvoiceFile:FileManagement
    {
        public int Price { get; set; }
    }
}
