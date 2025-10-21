using System.ComponentModel.DataAnnotations.Schema;
using YayinEviApi.Domain.Entities.Common;

namespace YayinEviApi.Domain.Entities.HelperEntities
{
    public class WorkType:BaseEntity
    {
        public Guid WorkCategoryId { get; set; }
        public string? TypeCode { get; set; }
        public string? TypeName { get; set; }
        public string? Description { get; set; }
        public string? CreatingUserId { get; set; }
        public string? UpdatingUserId { get; set; }

        public WorkCategory WorkCategory { get; set; }

        [NotMapped]
        public override string Code { get; set; }
    }
}
