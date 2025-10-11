using System.ComponentModel.DataAnnotations.Schema;
using YayinEviApi.Domain.Entities.Common;

namespace YayinEviApi.Domain.Entities
{
    public class FileManagement:BaseEntity
    {
        public string FileName { get; set; }
        public string Path { get; set; }
        public string? Storage { get; set; }
        public bool IsActive { get; set; }
        public string? EntityId { get; set; }
        public string? WhichPage { get; set; }
        public string? WhichClass { get; set; }
        public string? AddingUserId { get; set; }

        [NotMapped]
        public override DateTime UpdatedDate { get => base.UpdatedDate; set => base.UpdatedDate = value; }
    }
}
