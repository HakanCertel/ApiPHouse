using System.ComponentModel.DataAnnotations.Schema;
using YayinEviApi.Domain.Entities.Common;
using YayinEviApi.Domain.Entities.HelperEntities;

namespace YayinEviApi.Application.DTOs.HelperEntityDtos
{
    [NotMapped]
    public class WorkCategoryS:WorkCategory
    {
        public string WorkTypeName { get; set; }
    }
    public class WorkCategoryL
    {
        public string CategoryId { get; set; }
        public string WorkTypeId { get; set; }
        public string WorkTypeName { get; set; }
        public string CategoryCode { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
    }

}
