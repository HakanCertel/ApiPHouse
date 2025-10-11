using System.ComponentModel;

namespace YayinEviApi.Domain.Enum
{
    public enum MaterialTypes:byte
    {
        [Description("Mamül/Ürün")]
        product =0,
        [Description("YarıMamül")]
        semiProduct = 1,
        [Description("Hammadde")]
        rawMaterials=2,
        [Description("Sarf Malzeme")]
        consumable = 3,
        [Description("Ticari Malzeme")]
        commercialMaterial = 4,
        [Description("Yardımcı Malzeme")]
        auxiliaryMaterial=5
    }
}
