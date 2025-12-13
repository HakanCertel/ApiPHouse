using YayinEviApi.Domain.Interfaces;

namespace YayinEviApi.Application.DTOs.CategoriesDtos
{
    public class MainCategoryDto:IBaseEntity
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Title { get; set; }
        public string? EntityType { get; set; }

    }
}
