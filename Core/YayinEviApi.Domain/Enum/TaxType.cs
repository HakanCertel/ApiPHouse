using System.ComponentModel;

namespace YayinEviApi.Domain.Enum
{
    public enum TaxType : byte
    {
        [Description("%1")]
        Percent1=1,
        [Description("%10")]
        Percent10 = 2,
        [Description("%20")]
        Percent20 = 3
    }
}
