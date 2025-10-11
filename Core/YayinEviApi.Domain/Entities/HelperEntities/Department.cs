using YayinEviApi.Domain.Entities.Common;

namespace YayinEviApi.Domain.Entities.HelperEntities
{
    public class Department:BaseEntity
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
