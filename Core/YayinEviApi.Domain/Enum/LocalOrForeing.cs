using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YayinEviApi.Infrastructure.Enums
{
    public enum LocalOrForeing : byte
    {
        [Description("Yerli")]
        Local=1,
        [Description("Yabancı")]
        Foreing = 2
    }
}
