using System.ComponentModel;

namespace YayinEviApi.Infrastructure.Enums
{
    public enum CurrentState:byte
    {
        [Description("Açık")]
        Active =2,
        [Description("Kapalı")]
        Closed = 1,
        [Description("Potansiyel")]
        Potencial =2,
        [Description("Kara Liste")]
        BlackList =3,   
    }
}
