using YayinEviApi.Domain.Interfaces;

namespace YayinEviApi.Domain.Entities.Common
{
    public class BaseEntity:IBaseEntity
    {
        public Guid Id { get; set; }
        public virtual string? Code { get; set; }
        public DateTime CreatedDate { get; set; }
        public virtual DateTime UpdatedDate { get; set; }
        public virtual bool IsActive { get; set; }
    }
}
