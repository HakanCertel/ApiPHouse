using YayinEviApi.Domain.Entities.CurrentE;

namespace YayinEviApi.Application.DTOs.CurrentDtos
{
    public class CurrentDto
    {
        public string? Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string? Appellation { get; set; }
        public string? CurrentStatus { get; set; }
        public string? LocalOrForeing { get; set; }
        public string? CurrentState { get; set; }
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
        public string? Serie { get; set; }
        public ICollection<SubCurrent>? SubCurrents { get; set; }
        public ICollection<SubAddress>? SubAddresses { get; set; }
    }
}
