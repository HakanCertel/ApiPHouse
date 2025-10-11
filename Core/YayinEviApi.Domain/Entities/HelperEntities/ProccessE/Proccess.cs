using YayinEviApi.Domain.Entities.Common;
using YayinEviApi.Domain.Entities.HelperEntities.ProccessCategoryE;

namespace YayinEviApi.Domain.Entities.HelperEntities.ProccessE
{
    public class Proccess:BaseEntity
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public Guid? ProccessCategoryId { get; set; }

        public ProccessCategory ProccessCategory { get; set; }
    }
}
