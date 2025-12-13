using YayinEviApi.Domain.Entities.Common;

namespace YayinEviApi.Domain.Entities.CategoriesE
{
    public class SubCategory_4:BaseEntity
    {
        public string Name { get; set; }
        public string Title { get; set; }

        public Guid ParentId { get; set; }
        public SubCategory_3 Parent { get; set; }
    }
}
