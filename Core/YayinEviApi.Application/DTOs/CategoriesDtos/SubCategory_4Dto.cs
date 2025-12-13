using YayinEviApi.Domain.Interfaces;

namespace YayinEviApi.Application.DTOs.CategoriesDtos
{
    public class SubCategory_4Dto : IBaseEntity
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Title { get; set; }
        public string? ParentId { get; set; }
        public string? ParentName { get; set; }
        public string? ParentTitle { get; set; }

    }
}
