using YayinEviApi.Domain.Entities.AgencyE;

namespace YayinEviApi.Domain.Entities
{
    public class AgencyFile:FileManagement
    {
        public bool Showcase { get; set; }
        public ICollection<Agency> Agencies { get; set; }
    }
}
