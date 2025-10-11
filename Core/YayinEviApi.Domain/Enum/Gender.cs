using System.ComponentModel;

namespace YayinEviApi.Domain.Enum
{
    public enum Gender:byte
    {
        [Description("Erkek")]
        Male=1,
        [Description("Kadın")]
        Female=2
    }
}
