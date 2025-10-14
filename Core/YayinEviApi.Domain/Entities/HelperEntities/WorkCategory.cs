using YayinEviApi.Domain.Entities.Common;

namespace YayinEviApi.Domain.Entities.HelperEntities
{
    public class WorkCategory:BaseEntity
    {
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? CreatingUserId { get; set; }
        public string? UpdatingUserId { get; set; }

    }
}
