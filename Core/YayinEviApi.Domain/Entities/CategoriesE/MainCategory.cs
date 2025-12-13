using YayinEviApi.Domain.Entities.Common;
using YayinEviApi.Domain.Enum;

namespace YayinEviApi.Domain.Entities.CategoriesE
{
    public class MainCategory:BaseEntity
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public EntityType EntityType { get; set; }

        public ICollection<SubCategory_1> SubCategory_1List { get; set; }
    }
}
