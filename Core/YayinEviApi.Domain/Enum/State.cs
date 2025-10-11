using System.ComponentModel;

namespace YayinEviApi.Domain.Enum
{
    public enum State:byte
    {
        [Description("Onaylandı")]
        Confirmed=1,
        [Description("Beklemede")]
        Waiting =2,
        [Description("Teklif Aşamasında")]
        InOfferStage =3,
        [Description("Tamamlandı")]
        Completed =4,
        [Description("İşleme Alındı")]
        Proccessed =5
    }
}
