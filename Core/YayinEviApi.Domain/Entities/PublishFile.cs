using YayinEviApi.Domain.Entities.WorkE;

namespace YayinEviApi.Domain.Entities
{
    public class PublishFile:FileManagement
    {
        public bool Showcase { get; set; }
        public string? WorkId { get; set; }
        public string? DepartmentId { get; set; }
        public string? UserId { get; set; }
        //public bool IsActive { get; set; }
        public ICollection<Work> Works { get; set; }
    }
}
