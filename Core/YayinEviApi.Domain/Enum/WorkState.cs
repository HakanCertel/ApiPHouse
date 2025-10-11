using System.ComponentModel;

namespace YayinEviApi.Domain.Enum
{
    public enum WorkState:byte
    {
        [Description("Onaylandı")]
        Confirmed=1,
        [Description("Beklemede")]
        Waiting =2,
        [Description("Onay Bekliyor")]
        ConfirmWaiting = 3,
        [Description("Tamamlandı")]
        Completed =4,
        [Description("İşleme Alındı")]
        Proccessed =5,
        [Description("Hazır Değil")]
        NotReady = 6,
    }
}
