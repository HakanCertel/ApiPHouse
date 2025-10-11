using YayinEviApi.Domain.Entities.Common;
using YayinEviApi.Domain.Entities.WorkE;
using YayinEviApi.Infrastructure.Enums;

namespace YayinEviApi.Domain.Entities.AgencyE
{
    public class Agency:BaseEntity
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
        public string? Town { get; set; }
        public string? Address { get; set; }
        public string TaxDeparmant { get; set; }
        public string TaxNumber { get; set; }
        public string Description { get; set; }
        public string? PhoneNumber_1 { get; set; }
        public string? PhoneNumber_2 { get; set; }
        public string? ResponsibleName { get; set; }
        public string? Mail { get; set; }
        public string? WebSite { get; set; }
        public CurrentState? State { get; set; }
        public LocalOrForeing? LocalOrForeing { get; set; }

        public ICollection<AgencyFile> AgencyFiles { get; set; }

        public ICollection<AgencyConnectionInformation> AgencyConnectionInformations { get; set; }
        public ICollection<Author> Authors { get; set; }
        public ICollection<Work> Works { get; set; }

    }
   
}
