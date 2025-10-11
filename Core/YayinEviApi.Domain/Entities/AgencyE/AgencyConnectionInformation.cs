using YayinEviApi.Domain.Entities.Common;

namespace YayinEviApi.Domain.Entities.AgencyE
{
    public class AgencyConnectionInformation:BaseEntity
    {
        public Guid AgencyId { get; set; }
        public bool IsDefault { get; set; }
        public string Position { get; set; }
        public string NameSurname { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        public Agency Agency { get; set; }
    }
}
