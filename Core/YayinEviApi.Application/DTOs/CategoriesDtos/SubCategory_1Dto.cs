using YayinEviApi.Domain.Interfaces;

namespace YayinEviApi.Application.DTOs.CategoriesDtos
{
    public class SubCategory_1Dto : IBaseEntity
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Title { get; set; }
        public string? ParentId { get; set; }
        public string? ParentName { get; set; }
        public string? ParentTitle { get; set; }
        public string? Sub_2Id { get; set; }
        public string? Sub_2Name { get; set; }
        public string? Sub_2Title { get; set; }
    }
}
