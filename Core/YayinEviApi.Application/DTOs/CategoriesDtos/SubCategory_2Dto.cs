using YayinEviApi.Domain.Interfaces;

namespace YayinEviApi.Application.DTOs.CategoriesDtos
{
    public class SubCategory_2Dto : IBaseEntity
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Title { get; set; }
        public string? ParentId { get; set; }
        public string? ParentName { get; set; }
        public string? ParentTitle { get; set; }
        public string? Sub_3Id { get; set; }
        public string? Sub_3Name { get; set; }
        public string? Sub_3Title { get; set; }
    }
}
