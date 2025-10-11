using YayinEviApi.Domain.Entities.Common;

namespace YayinEviApi.Domain.Entities.CurrentE
{
    public class SubAddress:BaseEntity
    {
        public Guid ParentId { get; set; }
        public string? ResponsiblePerson { get; set; }
        public string? DepartmentofPerson { get; set; }
        public string? Email { get; set; }
        public string? MobilePhone { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? Country { get; set; }
        public string? County { get; set; }
        public string? Town { get; set; }
        public string? Description { get; set; }

        public Current Parent { get; set; }
    }
}
