using System.ComponentModel.DataAnnotations.Schema;
using YayinEviApi.Domain.Entities.Common;

namespace YayinEviApi.Domain.Entities.HubMessagesE
{
    public class HubMessage:BaseEntity
    {
        public string? MessageHead { get; set; }
        public string? MessageBody{ get; set; }
        public string? SendedUserId { get; set; }
        public string? CreatingUserId { get; set; }
        public string? CreatingUserNameSurname { get; set; }
        public bool? Readed { get; set; }
        public bool? IsShowed { get; set; }

        [NotMapped]
        public override string Code { get; set; }
    }
}
