using System.ComponentModel;

namespace YayinEviApi.Domain.Enum
{
    public enum CurrentStatus:byte
    {
        [Description("Müşteri")]
        Customer = 1,
        [Description("Tedarikci")]
        Supplier = 2,
    }
}
