using YayinEviApi.Domain.Entities.Common;
using YayinEviApi.Domain.Enum;
using YayinEviApi.Infrastructure.Enums;

namespace YayinEviApi.Domain.Entities.CurrentE
{
    public class Current:BaseEntity
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string? Appellation { get; set; }
        public CurrentStatus CurrentStatu { get; set; }
        public LocalOrForeing LocalOrForeing { get; set; }
        public CurrentState CurrentState { get; set; }
        public string? TaxDepartment { get; set; }
        public string? TaxNumber { get; set; }
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
        public ICollection<SubCurrent> SubCurrents { get; set; }
        public ICollection<SubAddress> SubAddresses { get; set; }

    }
}
