using YayinEviApi.Domain.Entities.Common;

namespace YayinEviApi.Domain.Entities.HelperEntities
{
    public class WorkType:BaseEntity
    {
        public string TypeCode { get; set; }
        public string TypeName { get; set; }
        public string Description { get; set; }
    }
}
