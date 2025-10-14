using YayinEviApi.Domain.Entities.AgencyE;
using YayinEviApi.Domain.Entities.Common;
using YayinEviApi.Domain.Entities.HelperEntities;

namespace YayinEviApi.Domain.Entities.WorkE
{
    public class Work:BaseEntity
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string WorkOrginalName { get; set; }
        public string Language { get; set; }
        public string isbn { get; set; }
        public string Barcode { get; set; }
        public string CertificateNumber { get; set; }
        public Guid? AuthorId { get; set; }
        public Guid? CategoryId { get; set; }
        public Guid? AgencyId { get; set; }
        public string Bandrol { get; set; }
        public string? Subject { get; set; }
        public string Description { get; set; }
       
        public Author Author { get; set; }
        public WorkCategory Category { get; set; }
        public Agency Agency { get; set; }

        public ICollection<PublishFile> PublishFiles { get; set; }


    }
}
