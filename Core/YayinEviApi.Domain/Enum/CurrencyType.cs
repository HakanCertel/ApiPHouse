using System.ComponentModel;

namespace YayinEviApi.Domain.Enum
{
    public enum CurrencyType: byte
    {
        [Description("Dolar")]
        Dolar = 1,
        [Description("Euro")]
        Euro = 2,
        [Description("Türk Lirası")]
        Tl = 3,
    }
}
